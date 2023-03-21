using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Oxide.Core.Libraries;
using Oxide.Ext.Discord.Callbacks.Api;
using Oxide.Ext.Discord.Constants;
using Oxide.Ext.Discord.Entities.Api;
using Oxide.Ext.Discord.Interfaces;
using Oxide.Ext.Discord.Libraries.Pooling;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.RateLimits;
using Oxide.Ext.Discord.Rest.Buckets;
using Oxide.Ext.Discord.Rest.Requests;

namespace Oxide.Ext.Discord.Rest
{
    /// <summary>
    /// Represents a REST handler for a bot
    /// </summary>
    public class RestHandler
    {
        /// <summary>
        /// <see cref="HttpClient"/> for API Requests
        /// </summary>
        public readonly HttpClient Client;

        /// <summary>
        /// Global Rate Limit for the bot
        /// </summary>
        public readonly RestRateLimit RateLimit;
        
        /// <summary>
        /// Buckets with Routes we don't know the Hash of yet
        /// </summary>
        public readonly ConcurrentDictionary<string, Bucket> Buckets = new ConcurrentDictionary<string, Bucket>();

        /// <summary>
        /// Route to Bucket Hash
        /// </summary>
        public readonly ConcurrentDictionary<string, string> RouteToHash = new ConcurrentDictionary<string, string>();

        /// <summary>
        /// The authorization header value
        /// </summary>
        //internal readonly string AuthHeader;
        
        private readonly ILogger _logger;
        
        /// <summary>
        /// Creates a new REST handler for a bot client
        /// </summary>
        /// <param name="client">Client the request is for</param>
        /// <param name="logger">Logger from the client</param>
        public RestHandler(BotClient client, ILogger logger)
        {
            HttpClientHandler handler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                UseCookies = false
            };
            Client = new HttpClient(handler)
            {
                Timeout = TimeSpan.FromSeconds(15),
                BaseAddress = new Uri(DiscordEndpoints.Rest.ApiUrl)
            };
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bot", client.Settings.ApiToken);
            Client.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
            Client.DefaultRequestHeaders.AcceptEncoding.Add( StringWithQualityHeaderValue.Parse("gzip"));
            Client.DefaultRequestHeaders.AcceptEncoding.Add(StringWithQualityHeaderValue.Parse("deflate"));
            Client.DefaultRequestHeaders.Add("user-agent", $"DiscordBot (https://github.com/Kirollos/Oxide.Ext.Discord, v{DiscordExtension.FullExtensionVersion})");
            _logger = logger;
            RateLimit = new RestRateLimit(logger);
        }

        /// <summary>
        /// Creates a new request and queues it to be ran
        /// </summary>
        /// <param name="client">Client making the request</param>
        /// <param name="url">URL of the request</param>
        /// <param name="method">HTTP method of the request</param>
        /// <param name="data">Data to be sent with the request</param>
        /// <param name="success">Callback once the action is completed</param>
        /// <param name="error">Error callback if an error occurs</param>
        /// <param name="callback">Completed callback for the request</param>
        public void CreateRequest(DiscordClient client, string url, RequestMethod method, object data, Action success, Action<RequestError> error, BaseApiCompletedCallback callback = null)
        {
            if (data is IDiscordValidation validate)
            {
                validate.Validate();
            }
            
            Request request = Request.CreateRequest(DiscordPool.Internal, client, Client, method, url, data, success, error, callback);
            StartRequest(request);
        }

        /// <summary>
        /// Creates a new request and queues it to be ran
        /// </summary>
        /// <param name="client">Client making the request</param>
        /// <param name="url">URL of the request</param>
        /// <param name="method">HTTP method of the request</param>
        /// <param name="data">Data to be sent with the request</param>
        /// <param name="success">Callback once the action is completed</param>
        /// <param name="error">Error callback if an error occurs</param>
        /// <param name="callback">Completed callback for the request</param>
        /// <typeparam name="T">The type that is expected to be returned</typeparam>
        public void CreateRequest<T>(DiscordClient client, string url, RequestMethod method, object data, Action<T> success, Action<RequestError> error, BaseApiCompletedCallback callback = null)
        {
            if (data is IDiscordValidation validate)
            {
                validate.Validate();
            }

            Request<T> request = Request<T>.CreateRequest(DiscordPool.Internal, client, Client, method, url, data, success, error, callback);
            StartRequest(request);
        }

        /// <summary>
        /// Starts the request
        /// </summary>
        /// <param name="request">Request to be started</param>
        public void StartRequest(BaseRequest request)
        {
            _logger.Debug($"{nameof(RestHandler)}.{nameof(StartRequest)} Method: {{0}} Route: {{1}}", request.Method, request.Route);
            RequestHandler.StartRequest(this, request);
        }
        
        /// <summary>
        /// Queues the request for the bucket
        /// </summary>
        public Bucket QueueBucket(RequestHandler handler, BaseRequest request)
        {
            string bucketId = BucketIdGenerator.GetBucketId(request.Method, request.Route);
            _logger.Debug("RestHandler Queuing Bucket for {0} bucket {1}",  request.Route, bucketId);
            Bucket bucket = GetBucket(bucketId);
            bucket.QueueRequest(handler);
            return bucket;
        }

        internal void UpgradeToKnownBucket(Bucket bucket, string newBucketId)
        {
            _logger.Debug("RestHandler Upgrading To Known Bucket for Old ID: {0} New ID: {1}", bucket.Id, newBucketId);
            RouteToHash[bucket.Id] = newBucketId;

            if (Buckets.TryGetValue(newBucketId, out Bucket existing))
            {
                existing.Merge(bucket);
                bucket.Dispose();
                return;
            }

            Buckets.TryRemove(bucket.Id, out Bucket _);
            bucket.Id = newBucketId;
            bucket.IsKnownBucket = true;
            Buckets[newBucketId] = bucket;
        }

        internal void RemoveBucket(Bucket bucket)
        {
            Buckets.TryRemove(bucket.Id, out Bucket _);
            bucket.Dispose();
        }

        /// <summary>
        /// Returns the bucket with the given ID
        /// </summary>
        /// <param name="bucketId"></param>
        /// <returns></returns>
        public Bucket GetBucket(string bucketId)
        {
            if (RouteToHash.ContainsKey(bucketId))
            {
                bucketId = RouteToHash[bucketId];
            }

            if (!Buckets.TryGetValue(bucketId, out Bucket bucket))
            {
                bucket = DiscordPool.Internal.Get<Bucket>();
                bucket.Init(bucketId, this, _logger);
                Buckets[bucketId] = bucket;
            }

            return bucket;
        }

        internal void OnClientClosed(DiscordClient client)
        {
            foreach (KeyValuePair<string, Bucket> bucket in Buckets)
            {
                bucket.Value.AbortClientRequests(client);
            }
        }

        /// <summary>
        /// Shutdown the REST handler
        /// </summary>
        public void Shutdown()
        {
            foreach (KeyValuePair<string, Bucket> bucket in Buckets)
            {
                bucket.Value.Dispose();
            }
            
            RouteToHash.Clear();
            Buckets.Clear();
            RateLimit.Shutdown();
        }
    }
}
