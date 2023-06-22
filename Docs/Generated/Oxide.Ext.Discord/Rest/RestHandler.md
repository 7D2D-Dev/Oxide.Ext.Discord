# RestHandler class

Represents a REST handler for a bot

```csharp
public class RestHandler : IDebugLoggable
```

## Public Members

| name | description |
| --- | --- |
| [RestHandler](RestHandler/RestHandler.md)(…) | Creates a new REST handler for a bot client |
| readonly [Buckets](RestHandler/Buckets.md) | Buckets with Routes we don't know the Hash of yet |
| readonly [Client](RestHandler/Client.md) | HttpClient for API Requests |
| readonly [RateLimit](RestHandler/RateLimit.md) | Global Rate Limit for the bot |
| readonly [RouteToBucketId](RestHandler/RouteToBucketId.md) | Route to Bucket ID |
| [Delete](RestHandler/Delete.md)(…) | Performs a HTTP Delete Request |
| [Delete&lt;TResult&gt;](RestHandler/Delete.md)(…) | Performs a HTTP Delete Request with TResult response |
| [Get&lt;TResult&gt;](RestHandler/Get.md)(…) | Performs a HTTP Get Request with TResult response |
| [GetBucket](RestHandler/GetBucket.md)(…) | Returns the bucket with the given ID |
| [LogDebug](RestHandler/LogDebug.md)(…) |  |
| [Patch](RestHandler/Patch.md)(…) | Performs a HTTP Patch Request |
| [Patch&lt;TResult&gt;](RestHandler/Patch.md)(…) | Performs a HTTP Patch Request with TResult response |
| [Post](RestHandler/Post.md)(…) | Performs a HTTP Post Request |
| [Post&lt;TResult&gt;](RestHandler/Post.md)(…) | Performs a HTTP Post Request with TResult response |
| [Put](RestHandler/Put.md)(…) | Performs a HTTP Put Request |
| [Put&lt;TResult&gt;](RestHandler/Put.md)(…) | Performs a HTTP Put Request with TResult response |
| [QueueBucket](RestHandler/QueueBucket.md)(…) | Queues the request for the bucket |
| [Shutdown](RestHandler/Shutdown.md)() | Shutdown the REST handler |
| [StartRequest](RestHandler/StartRequest.md)(…) | Starts the request |

## See Also

* interface [IDebugLoggable](../Interfaces/Logging/IDebugLoggable.md)
* namespace [Oxide.Ext.Discord.Rest](./RestNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
* [RestHandler.cs](https://github.com/dassjosh/Oxide.Ext.Discord/blob/develop/Oxide.Ext.Discord/Rest/RestHandler.cs)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->