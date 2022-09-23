﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Core.Libraries;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Builders.Messages;
using Oxide.Ext.Discord.Entities.Api;
using Oxide.Ext.Discord.Entities.Guilds;
using Oxide.Ext.Discord.Entities.Messages;
using Oxide.Ext.Discord.Entities.Users;
using Oxide.Ext.Discord.Exceptions.Entities;
using Oxide.Ext.Discord.Libraries.Langs;
using Oxide.Ext.Discord.Libraries.Placeholders;

namespace Oxide.Ext.Discord.Entities.Webhooks
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/resources/webhook#webhook-object">Webhook Structure</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class DiscordWebhook
    {
        /// <summary>
        /// The id of the webhook
        /// </summary>
        [JsonProperty("id")]
        public Snowflake Id { get; set; }
        
        /// <summary>
        /// The type of the webhook
        /// See <see cref="WebhookType"/>
        /// </summary>
        [JsonProperty("type")]
        public WebhookType Type { get; set; }
        
        /// <summary>
        /// The guild id this webhook is for, if any
        /// </summary>
        [JsonProperty("guild_id")]
        public Snowflake? GuildId { get; set; }
        
        /// <summary>
        /// The channel id this webhook is for, if any
        /// </summary>
        [JsonProperty("channel_id")]
        public Snowflake? ChannelId { get; set; }
        
        /// <summary>
        /// The user this webhook was created by
        /// not returned when getting a webhook with its token
        /// </summary>
        [JsonProperty("user")]
        public DiscordUser User { get; set; }
        
        /// <summary>
        /// The default name of the webhook
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        
        /// <summary>
        /// the default user avatar hash of the webhook
        /// </summary>
        [JsonProperty("avatar")]
        public string Avatar { get; set; }
        
        /// <summary>
        /// The secure token of the webhook
        /// returned for Incoming Webhooks
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; set; }
        
        /// <summary>
        /// The bot/OAuth2 application that created this webhook
        /// </summary>
        [JsonProperty("application_id")]
        public Snowflake ApplicationId { get; set; }
        
        /// <summary>
        /// The guild of the channel that this webhook is following (returned for Channel Follower Webhooks)
        /// </summary>
        [JsonProperty("source_guild")]
        public DiscordGuild SourceGuild { get; set; }
        
        /// <summary>
        /// The channel that this webhook is following (returned for Channel Follower Webhooks)
        /// </summary>
        [JsonProperty("source_channel")]
        public Snowflake SourceChannel { get; set; }

        /// <summary>
        /// Create a new webhook.
        /// Requires the MANAGE_WEBHOOKS permission.
        /// See <a href="https://discord.com/developers/docs/resources/webhook#create-webhook">Create Webhook</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="channelId">Channel ID for the webhook</param>
        /// <param name="name">Name of the webhook (1-80 characters)</param>
        /// <param name="avatar">Image for the default webhook avatar</param>
        /// <param name="callback">Callback with the completed webhook</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        [Obsolete("Replaced with CreateWebhook(DiscordClient client, Snowflake channelId, WebhookCreate create, Action<DiscordWebhook> callback = null, Action<RestError> error = null)")]
        public static void CreateWebhook(DiscordClient client, Snowflake channelId, string name, string avatar = null, Action<DiscordWebhook> callback = null, Action<RequestError> error = null)
        {
            WebhookCreate create = new WebhookCreate
            {
                Name = name,
                Avatar = avatar
            };

            CreateWebhook(client, channelId, create, callback, error);
        }

        /// <summary>
        /// Create a new webhook.
        /// Requires the MANAGE_WEBHOOKS permission.
        /// See <a href="https://discord.com/developers/docs/resources/webhook#create-webhook">Create Webhook</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="channelId">Channel ID for the webhook</param>
        /// <param name="create">Webhook create request</param>
        /// <param name="callback">Callback with the completed webhook</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public static void CreateWebhook(DiscordClient client, Snowflake channelId, WebhookCreate create, Action<DiscordWebhook> callback = null, Action<RequestError> error = null)
        {
            InvalidSnowflakeException.ThrowIfInvalid(channelId, nameof(channelId));
            client.Bot.Rest.CreateRequest(client,$"channels/{channelId}/webhooks", RequestMethod.POST, create, callback, error);
        }

        /// <summary>
        /// Returns a list of channel webhook.
        /// See <a href="https://discord.com/developers/docs/resources/webhook#get-channel-webhooks">Get Channel Webhooks</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="channelId">Channel ID to get webhooks for</param>
        /// <param name="callback">Callback with a list of channel webhooks</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public static void GetChannelWebhooks(DiscordClient client, Snowflake channelId, Action<List<DiscordWebhook>> callback = null, Action<RequestError> error = null)
        {
            InvalidSnowflakeException.ThrowIfInvalid(channelId, nameof(channelId));
            client.Bot.Rest.CreateRequest(client,$"channels/{channelId}/webhooks", RequestMethod.GET, null, callback, error);
        }

        /// <summary>
        /// Returns a list of guild webhooks
        /// See <a href="https://discord.com/developers/docs/resources/webhook#get-guild-webhooks">Get Guild Webhooks</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="guildId">Guild ID to get webhooks for</param>
        /// <param name="callback">Callback with the list of guild webhooks</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public static void GetGuildWebhooks(DiscordClient client, Snowflake guildId, Action<List<DiscordWebhook>> callback = null, Action<RequestError> error = null)
        {
            InvalidSnowflakeException.ThrowIfInvalid(guildId, nameof(guildId));
            client.Bot.Rest.CreateRequest(client,$"guilds/{guildId}/webhooks", RequestMethod.GET, null, callback, error);
        }

        /// <summary>
        /// Returns the webhook with the given webhook ID
        /// See <a href="https://discord.com/developers/docs/resources/webhook#get-webhook">Get Webhook</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="webhookId">Webhook ID to get</param>
        /// <param name="callback">Callback with the webhook</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public static void GetWebhook(DiscordClient client, Snowflake webhookId, Action<DiscordWebhook> callback = null, Action<RequestError> error = null)
        {
            InvalidSnowflakeException.ThrowIfInvalid(webhookId, nameof(webhookId));
            client.Bot.Rest.CreateRequest(client,$"webhooks/{webhookId}", RequestMethod.GET, null, callback, error);
        }

        /// <summary>
        /// Returns the webhook with the given ID &amp; Token
        /// This call does not required authentication
        /// No user is returned in webhook object
        /// See <a href="https://discord.com/developers/docs/resources/webhook#get-webhook-with-token">Get Webhook with Token</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="webhookId">Webhook ID to get</param>
        /// <param name="webhookToken">Webhook Token</param>
        /// <param name="callback">Callback with the webhook</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public static void GetWebhookWithToken(DiscordClient client, Snowflake webhookId, string webhookToken, Action<DiscordWebhook> callback = null, Action<RequestError> error = null)
        {
            InvalidSnowflakeException.ThrowIfInvalid(webhookId, nameof(webhookId));
            client.Bot.Rest.CreateRequest(client,$"webhooks/{webhookId}/{webhookToken}", RequestMethod.GET, null, callback, error);
        }

        /// <summary>
        /// Returns the webhook with the given ID &amp; Token
        /// This call does not required authentication
        /// No user is returned in webhook object
        /// See <a href="https://discord.com/developers/docs/resources/webhook#get-webhook-with-token">Get Webhook with Token</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="webhookUrl">Returns the webhook for the specified URL</param>
        /// <param name="callback">Callback with the webhook</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public static void GetWebhookWithUrl(DiscordClient client, string webhookUrl, Action<DiscordWebhook> callback = null, Action<RequestError> error = null)
        {
            string[] webhookInfo = webhookUrl.Split('/');
            string id = webhookInfo[webhookInfo.Length - 2];
            string token = webhookInfo[webhookInfo.Length - 1];
            
            client.Bot.Rest.CreateRequest(client,$"webhooks/{id}/{token}", RequestMethod.GET, null, callback, error);
        }

        /// <summary>
        /// Modify a webhook.
        /// Requires the MANAGE_WEBHOOKS permission.
        /// See <a href="https://discord.com/developers/docs/resources/webhook#modify-webhook">Modify Webhook</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="name">New webhook name</param>
        /// <param name="avatar">New avatar image</param>
        /// <param name="channelId">Channel to move the webhook to</param>
        /// <param name="callback">Callback with the updated webhook</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void ModifyWebhook(DiscordClient client, string name = null, string avatar = null, Snowflake? channelId = null, Action<DiscordWebhook> callback = null, Action<RequestError> error = null)
        {
            InvalidSnowflakeException.ThrowIfInvalid(channelId, false, nameof(channelId));

            Dictionary<string, object> data = new Dictionary<string, object>
            {
                ["name"] = name ,
                ["avatar"] = avatar,
                ["channel_id"] = channelId
            };

            client.Bot.Rest.CreateRequest(client,$"webhooks/{Id}", RequestMethod.PATCH, data, callback, error);
        }

        /// <summary>
        /// Modify a webhook.
        /// Requires the MANAGE_WEBHOOKS permission.
        /// See <a href="https://discord.com/developers/docs/resources/webhook#modify-webhook-with-token">Modify Webhook with Token</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="name">New webhook name</param>
        /// <param name="avatar">New avatar image</param>
        /// <param name="callback">Callback with the updated webhook</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void ModifyWebhookWithToken(DiscordClient client, string name = null, string avatar = null, Action<DiscordWebhook> callback = null, Action<RequestError> error = null)
        {
            Dictionary<string, object> data = new Dictionary<string, object>
            {
                ["name"] = name,
                ["avatar"] = avatar
            };

            client.Bot.Rest.CreateRequest(client,$"webhooks/{Id}/{Token}", RequestMethod.PATCH, data, callback, error);
        }

        /// <summary>
        /// Delete a webhook permanently.
        /// Requires the MANAGE_WEBHOOKS permission.
        /// See <a href="https://discord.com/developers/docs/resources/webhook#delete-webhook">Delete Webhook</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void DeleteWebhook(DiscordClient client, Action callback = null, Action<RequestError> error = null)
        {
            client.Bot.Rest.CreateRequest(client,$"webhooks/{Id}", RequestMethod.DELETE, null, callback, error);
        }

        /// <summary>
        /// Delete a webhook permanently.
        /// Does not require authentication.
        /// See <a href="https://discord.com/developers/docs/resources/webhook#delete-webhook-with-token">Delete Webhook with Token</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void DeleteWebhookWithToken(DiscordClient client, Action callback = null, Action<RequestError> error = null)
        {
            client.Bot.Rest.CreateRequest(client,$"webhooks/{Id}/{Token}", RequestMethod.DELETE, null, callback, error);
        }

        /// <summary>
        /// Executes a webhook
        /// See <a href="https://discord.com/developers/docs/resources/webhook#execute-webhook">Execute Webhook</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="message">Message data</param>
        /// <param name="executeParams">Webhook execution parameters</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void ExecuteWebhook(DiscordClient client, WebhookCreateMessage message, WebhookExecuteParams executeParams = null, Action callback = null, Action<RequestError> error = null)
        {
            if (executeParams == null)
            {
                executeParams = WebhookExecuteParams.Default;
            }

            client.Bot.Rest.CreateRequest(client,$"webhooks/{Id}/{Token}{executeParams.GetWebhookFormat()}{executeParams.ToQueryString()}", RequestMethod.POST, message, callback, error);
        }
        
        /// <summary>
        /// Executes a webhook
        /// See <a href="https://discord.com/developers/docs/resources/webhook#execute-webhook">Execute Webhook</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="builder">Builder for the message</param>
        /// <param name="executeParams">Webhook execution parameters</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void ExecuteWebhook(DiscordClient client, WebhookMessageBuilder builder, WebhookExecuteParams executeParams = null, Action callback = null, Action<RequestError> error = null)
        {
            ExecuteWebhook(client, builder.Build(), executeParams, callback, error);
        }

        /// <summary>
        /// Executes a webhook
        /// See <a href="https://discord.com/developers/docs/resources/webhook#execute-webhook">Execute Webhook</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="message">Message data</param>
        /// <param name="executeParams">Webhook execution parameters</param>
        /// <param name="callback">Callback with the created message</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void ExecuteWebhook(DiscordClient client, WebhookCreateMessage message, WebhookExecuteParams executeParams = null, Action<DiscordMessage> callback = null, Action<RequestError> error = null)
        {
            if (executeParams == null)
            {
                executeParams = WebhookExecuteParams.DefaultWait;
            }

            client.Bot.Rest.CreateRequest(client,$"webhooks/{Id}/{Token}{executeParams.GetWebhookFormat()}{executeParams.ToQueryString()}", RequestMethod.POST, message, callback, error);
        }
        
        /// <summary>
        /// Executes a webhook
        /// See <a href="https://discord.com/developers/docs/resources/webhook#execute-webhook">Execute Webhook</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="builder">Builder for the message</param>
        /// <param name="executeParams">Webhook execution parameters</param>
        /// <param name="callback">Callback with the created message</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void ExecuteWebhook(DiscordClient client, WebhookMessageBuilder builder, WebhookExecuteParams executeParams = null, Action<DiscordMessage> callback = null, Action<RequestError> error = null)
        {
            ExecuteWebhook(client, builder.Build(), executeParams, callback, error);
        }

        /// <summary>
        /// Send a message to a webhook using a global message template
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="plugin">Plugin for the template</param>
        /// <param name="templateName">Template Name</param>
        /// <param name="message">Message to use (optional)</param>
        /// <param name="placeholders">Placeholders to apply (optional)</param>
        /// <param name="executeParams">Webhook execution parameters</param>
        /// <param name="callback">Callback when the message is created</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void ExecuteWebhookGlobalTemplate(DiscordClient client, Plugin plugin, string templateName, WebhookCreateMessage message = null, PlaceholderData placeholders = null, WebhookExecuteParams executeParams = null, Action<DiscordMessage> callback = null, Action<RequestError> error = null)
        {
            DiscordExtension.DiscordMessageTemplates.GetGlobalTemplateInternalAsync(plugin, templateName).OnSuccess(template =>
            {
                template.ToMessageInternalAsync(placeholders, message).OnSuccess(response =>
                {
                    ExecuteWebhook(client, response, executeParams, callback, error);
                });
            });
        }

        /// <summary>
        /// Send a message to a webhook using a localized message template
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="plugin">Plugin for the template</param>
        /// <param name="templateName">Template Name</param>
        /// <param name="language">Oxide language to use</param>
        /// <param name="message">Message to use (optional)</param>
        /// <param name="placeholders">Placeholders to apply (optional)</param>
        /// <param name="executeParams">Webhook execution parameters</param>
        /// <param name="callback">Callback when the message is created</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void ExecuteWebhookTemplate(DiscordClient client, Plugin plugin, string templateName, string language = DiscordLang.DefaultOxideLanguage, WebhookCreateMessage message = null, PlaceholderData placeholders = null, WebhookExecuteParams executeParams = null, Action<DiscordMessage> callback = null, Action<RequestError> error = null)
        {
            DiscordExtension.DiscordMessageTemplates.GetLocalizedTemplateInternalAsync(plugin, templateName, language).OnSuccess(template =>
            {
                template.ToMessageInternalAsync(placeholders, message).OnSuccess(response =>
                {
                    ExecuteWebhook(client, response, executeParams, callback, error);
                });
            });
        }

        /// <summary>
        /// Gets a previously-sent webhook message from the same token.
        /// See <a href="https://discord.com/developers/docs/resources/webhook#get-webhook-message">Edit Webhook Message</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="messageId">Message ID to get</param>
        /// <param name="messageParams">Message Params</param>
        /// <param name="callback">Callback with the message</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void GetWebhookMessage(DiscordClient client, Snowflake messageId, WebhookMessageParams messageParams = null, Action<DiscordMessage> callback = null, Action<RequestError> error = null)
        {
            InvalidSnowflakeException.ThrowIfInvalid(messageId, nameof(messageId));
            client.Bot.Rest.CreateRequest(client,$"webhooks/{Id}/{Token}/messages/{messageId}{messageParams?.ToQueryString()}", RequestMethod.GET, null, callback, error);
        }
        
        /// <summary>
        /// Edits a previously-sent webhook message from the same token.
        /// See <a href="https://discord.com/developers/docs/resources/webhook#edit-webhook-message">Edit Webhook Message</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="messageId">Message ID to edit</param>
        /// <param name="messageParams">Message Params</param>
        /// <param name="message">The updated message</param>
        /// <param name="callback">Callback with the edited message</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void EditWebhookMessage(DiscordClient client, Snowflake messageId, DiscordMessage message, WebhookMessageParams messageParams = null, Action<DiscordMessage> callback = null, Action<RequestError> error = null)
        {
            client.Bot.Rest.CreateRequest(client,$"webhooks/{Id}/{Token}/messages/{messageId}{messageParams?.ToQueryString()}", RequestMethod.PATCH, message, callback, error);
        }

        /// <summary>
        /// Edit a message from a webhook using a global message template
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="messageId">Message ID of the message to edit</param>
        /// <param name="plugin">Plugin for the template</param>
        /// <param name="templateName">Template Name</param>
        /// <param name="message">Message to use (optional)</param>
        /// <param name="placeholders">Placeholders to apply (optional)</param>
        /// <param name="messageParams">Message Params</param>
        /// <param name="callback">Callback when the message is created</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void EditWebhookMessageGlobalTemplate(DiscordClient client, Snowflake messageId, Plugin plugin, string templateName, DiscordMessage message = null, PlaceholderData placeholders = null, WebhookMessageParams messageParams = null, Action<DiscordMessage> callback = null, Action<RequestError> error = null)
        {
            DiscordExtension.DiscordMessageTemplates.GetGlobalTemplateInternalAsync(plugin, templateName).OnSuccess(template =>
            {
                template.ToMessageInternalAsync(placeholders, message).OnSuccess(response =>
                {
                    EditWebhookMessage(client, messageId, response, messageParams, callback, error);
                });
            });
        }

        /// <summary>
        /// Edit a message from a webhook using a localized message template
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="messageId">Message ID of the message to edit</param>
        /// <param name="plugin">Plugin for the template</param>
        /// <param name="templateName">Template Name</param>
        /// <param name="language">Oxide language to use</param>
        /// <param name="message">Message to use (optional)</param>
        /// <param name="placeholders">Placeholders to apply (optional)</param>
        /// <param name="messageParams">Message Params</param>
        /// <param name="callback">Callback when the message is created</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void EditWebhookMessageTemplate(DiscordClient client, Snowflake messageId, Plugin plugin, string templateName, string language = DiscordLang.DefaultOxideLanguage, DiscordMessage message = null, PlaceholderData placeholders = null, WebhookMessageParams messageParams = null, Action<DiscordMessage> callback = null, Action<RequestError> error = null)
        {
            DiscordExtension.DiscordMessageTemplates.GetLocalizedTemplateInternalAsync(plugin, templateName, language).OnSuccess(template =>
            {
                template.ToMessageInternalAsync(placeholders, message).OnSuccess(response =>
                {
                    EditWebhookMessage(client, messageId, response, messageParams, callback, error);
                });
            });
        }
        
        /// <summary>
        /// Deletes a message that was created by the webhook.
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="messageId">Message ID to delete</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void DeleteWebhookMessage(DiscordClient client, Snowflake messageId, Action callback = null, Action<RequestError> error = null)
        {
            InvalidSnowflakeException.ThrowIfInvalid(messageId, nameof(messageId));
            client.Bot.Rest.CreateRequest(client,$"webhooks/{Id}/{Token}/messages/{messageId}", RequestMethod.DELETE, null, callback, error);
        }
    }
}
