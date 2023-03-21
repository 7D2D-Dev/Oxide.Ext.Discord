﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Core.Libraries;
using Oxide.Core.Libraries.Covalence;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Entities.Api;
using Oxide.Ext.Discord.Entities.Channels;
using Oxide.Ext.Discord.Entities.Guilds;
using Oxide.Ext.Discord.Entities.Messages;
using Oxide.Ext.Discord.Entities.Messages.Embeds;
using Oxide.Ext.Discord.Entities.Permissions;
using Oxide.Ext.Discord.Entities.Users.Connections;
using Oxide.Ext.Discord.Exceptions.Entities;
using Oxide.Ext.Discord.Exceptions.Entities.Channels;
using Oxide.Ext.Discord.Helpers;
using Oxide.Ext.Discord.Interfaces;
using Oxide.Ext.Discord.Json.Converters;
using Oxide.Ext.Discord.Libraries.Langs;
using Oxide.Ext.Discord.Libraries.Linking;
using Oxide.Ext.Discord.Libraries.Placeholders;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Entities.Users
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/resources/user#user-object">User Structure</a>
    /// </summary>
    [JsonConverter(typeof(DiscordUserConverter))]
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class DiscordUser : ISnowflakeEntity, IDiscordUser
    {
        #region Discord Fields
        ///<inheritdoc cref="IDiscordUser.Id"/>
        [JsonProperty("id")]
        public Snowflake Id { get; set; }

        ///<inheritdoc cref="IDiscordUser.Username"/>
        [JsonProperty("username")]
        public string Username { get; set; }

        ///<inheritdoc cref="IDiscordUser.Discriminator"/>
        [JsonProperty("discriminator")]
        public string Discriminator { get; set; }

        ///<inheritdoc cref="IDiscordUser.Avatar"/>
        [JsonProperty("avatar")]
        public string Avatar { get; set; }

        ///<inheritdoc cref="IDiscordUser.Bot"/>
        [JsonProperty("bot")]
        public bool? Bot { get; set; }

        ///<inheritdoc cref="IDiscordUser.System"/>
        [JsonProperty("system")]
        public bool? System { get; set; }

        ///<inheritdoc cref="IDiscordUser.MfaEnabled"/>
        [JsonProperty("mfa_enabled")]
        public bool? MfaEnabled { get; set; }
        
        ///<inheritdoc cref="IDiscordUser.Banner"/>
        [JsonProperty("banner")]
        public string Banner { get; set; }
        
        ///<inheritdoc cref="IDiscordUser.AccentColor"/>
        [JsonProperty("accent_color")]
        public DiscordColor? AccentColor { get; set; }
        
        ///<inheritdoc cref="IDiscordUser.Locale"/>
        [JsonProperty("locale")]
        public string Locale { get; set; }

        ///<inheritdoc cref="IDiscordUser.Verified"/>
        [JsonProperty("verified")]
        public bool? Verified { get; set; }

        ///<inheritdoc cref="IDiscordUser.Email"/>
        [JsonProperty("email")]
        public string Email { get; set; }

        ///<inheritdoc cref="IDiscordUser.Flags"/>
        [JsonProperty("flags")]
        public UserFlags? Flags { get; set; }

        ///<inheritdoc cref="IDiscordUser.PremiumType"/>
        [JsonProperty("premium_type")]
        public UserPremiumType? PremiumType { get; set; }

        ///<inheritdoc cref="IDiscordUser.PublicFlags"/>
        [JsonProperty("public_flags")]
        public UserFlags? PublicFlags { get; set; }
        #endregion

        #region Helper Properties
        /// <summary>
        /// Returns a string to mention this users nickname in a message
        /// </summary>
        public string Mention => DiscordFormatting.MentionUser(Id);

        /// <summary>
        /// Default Avatar Url for the User
        /// </summary>
        public string GetDefaultAvatarUrl => DiscordCdn.GetUserDefaultAvatarUrl(Discriminator);

        /// <summary>
        /// Avatar Url for the user
        /// </summary>
        public string GetAvatarUrl => DiscordCdn.GetUserAvatarUrl(Id, Avatar);
        
        /// <summary>
        /// Banner Url for the user
        /// </summary>
        public string GetBannerUrl => DiscordCdn.GetUserBanner(Id, Banner);

        /// <summary>
        /// Returns the username#discriminator for the user
        /// </summary>
        public string FullUserName => $"{Username}#{Discriminator}";

        /// <summary>
        /// Returns if the DiscordUser is a bot
        /// </summary>
        public bool IsBot => Bot.HasValue && Bot.Value;
        
        /// <summary>
        /// Returns if the DiscordUser is a system user
        /// </summary>
        public bool IsSystem => System.HasValue && System.Value;

        /// <summary>
        /// Returns the IPlayer for the discord user if linked; null otherwise
        /// </summary>
        public IPlayer Player => DiscordLink.Instance.GetPlayer(Id);
        #endregion

        #region API Methods
        /// <summary>
        /// Returns the currently logged in user account
        /// See <a href="https://discord.com/developers/docs/resources/user#get-current-user">Get Current User</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="callback">Callback with the logged in user</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public static void GetCurrentUser(DiscordClient client, Action<DiscordUser> callback = null, Action<RequestError> error = null)
        {
            client.Bot.Rest.CreateRequest(client,"users/@me", RequestMethod.GET, null, callback, error);
        }

        /// <summary>
        /// Returns the user for the given user Id
        /// See <a href="https://discord.com/developers/docs/resources/user#get-user">Get User</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="userId">User ID to lookup</param>
        /// <param name="callback">Callback with the looked up user</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public static void GetUser(DiscordClient client, Snowflake userId, Action<DiscordUser> callback = null, Action<RequestError> error = null)
        {
            InvalidSnowflakeException.ThrowIfInvalid(userId, nameof(userId));
            client.Bot.Rest.CreateRequest(client,$"users/{userId}", RequestMethod.GET, null, callback, error);
        }

        /// <summary>
        /// Send a message to a user in a direct message channel
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="message">Message to be created</param>
        /// <param name="callback">Callback with the created message</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void SendDirectMessage(DiscordClient client, MessageCreate message, Action<DiscordMessage> callback = null, Action<RequestError> error = null)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));
            CreateDirectMessageChannel(client, Id, channel => { channel.CreateMessage(client, message, callback, error); });
        }

        /// <summary>
        /// Send a message to a user in a direct message channel
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="message">Content of the message</param>
        /// <param name="callback">Callback with the created message</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void SendDirectMessage(DiscordClient client, string message, Action<DiscordMessage> callback = null, Action<RequestError> error = null)
        {
            CreateDirectMessageChannel(client, Id, channel => { channel.CreateMessage(client, message, callback, error); });
        }

        /// <summary>
        /// Send a message to a user in a direct message channel
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="embeds">Embeds to be send in the message</param>
        /// <param name="callback">Callback with the created message</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void SendDirectMessage(DiscordClient client, List<DiscordEmbed> embeds, Action<DiscordMessage> callback = null, Action<RequestError> error = null)
        {
            if (embeds == null) throw new ArgumentNullException(nameof(embeds));
            CreateDirectMessageChannel(client, Id, channel => { channel.CreateMessage(client, embeds, callback, error); });
        }

        /// <summary>
        /// Send a message in a DM to the user using a localized message template
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="plugin">Plugin for the template</param>
        /// <param name="templateName">Template Name</param>
        /// <param name="language">Oxide language to use</param>
        /// <param name="message">Message to use (optional)</param>
        /// <param name="placeholders">Placeholders to apply (optional)</param>
        /// <param name="callback">Callback when the message is created</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void SendTemplateDirectMessage(DiscordClient client, Plugin plugin, string templateName, string language = DiscordLang.DefaultOxideLanguage, MessageCreate message = null, PlaceholderData placeholders = null, Action<DiscordMessage> callback = null, Action<RequestError> error = null)
        {
            MessageCreate template = DiscordExtension.DiscordMessageTemplates.GetLocalizedEntity(plugin, templateName, language, placeholders, message);
            SendDirectMessage(client, template, callback, error);
        }
        
        /// <summary>
        /// Reply to a message using a global message template
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="plugin">Plugin for the template</param>
        /// <param name="templateName">Template Name</param>
        /// <param name="message">Message to use (optional)</param>
        /// <param name="placeholders">Placeholders to apply (optional)</param>
        /// <param name="callback">Callback when the message is created</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void SendGlobalTemplateDirectMessage(DiscordClient client, Plugin plugin, string templateName, MessageCreate message = null, PlaceholderData placeholders = null, Action<DiscordMessage> callback = null, Action<RequestError> error = null)
        {
            MessageCreate template = DiscordExtension.DiscordMessageTemplates.GetGlobalEntity(plugin, templateName, placeholders, message);
            SendDirectMessage(client, template, callback, error);
        }

        /// <summary>
        /// Modify the currently logged in user with the currently set UserName and Avatar
        /// See <a href="https://discord.com/developers/docs/resources/user#modify-current-user">Modify Current User</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="callback">Callback with the updated user</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        [Obsolete("Replaced with ModifyCurrentUser(DiscordClient client, UserModifyCurrent current, Action<DiscordUser> callback = null, Action<RestError> error = null)")]
        public void ModifyCurrentUser(DiscordClient client, Action<DiscordUser> callback = null, Action<RequestError> error = null) => ModifyCurrentUser(client, Username, Avatar, callback, error);

        /// <summary>
        /// Modify the currently logged in user
        /// See <a href="https://discord.com/developers/docs/resources/user#modify-current-user">Modify Current User</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="username">Username to set</param>
        /// <param name="avatarData">Avatar data to set</param>
        /// <param name="callback">Callback with the updated user</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        [Obsolete("Replaced with ModifyCurrentUser(DiscordClient client, UserModifyCurrent current, Action<DiscordUser> callback = null, Action<RestError> error = null)")]
        public void ModifyCurrentUser(DiscordClient client, string username = "", string avatarData = "", Action<DiscordUser> callback = null, Action<RequestError> error = null)
        {
            UserModifyCurrent data = new UserModifyCurrent
            {
                Username = username,
                Avatar = avatarData
            };

            ModifyCurrentUser(client, data, callback, error);
        }

        /// <summary>
        /// Modify the currently logged in user
        /// See <a href="https://discord.com/developers/docs/resources/user#modify-current-user">Modify Current User</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="current">The updated current user information</param>
        /// <param name="callback">Callback with the updated user</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void ModifyCurrentUser(DiscordClient client, UserModifyCurrent current, Action<DiscordUser> callback = null, Action<RequestError> error = null)
        {
            if (current == null) throw new ArgumentNullException(nameof(current));
            client.Bot.Rest.CreateRequest(client,"users/@me", RequestMethod.PATCH, current, callback, error);
        }

        /// <summary>
        /// Returns the guilds for the currently logged in user
        /// See <a href="https://discord.com/developers/docs/resources/user#get-current-user-guilds">Get Current User Guilds</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="request">Request parameters for filtering guilds</param>
        /// <param name="callback">Callback with the list of guilds</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void GetCurrentUserGuilds(DiscordClient client, UserGuildsRequest request = null, Action<List<DiscordGuild>> callback = null, Action<RequestError> error = null)
        {
            client.Bot.Rest.CreateRequest(client,$"users/@me/guilds{request?.ToQueryString()}", RequestMethod.GET, null, callback, error);
        }

        /// <summary>
        /// Leave the guild that the currently logged in user is in
        /// See <a href="https://discord.com/developers/docs/resources/user#leave-guild">Leave Guild</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="guild">Guild to leave</param>
        /// <param name="callback">callback when the action is completed</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void LeaveGuild(DiscordClient client, DiscordGuild guild, Action callback = null, Action<RequestError> error = null) => LeaveGuild(client, guild.Id, callback, error);

        /// <summary>
        /// Leave the guild that the currently logged in user is in
        /// See <a href="https://discord.com/developers/docs/resources/user#leave-guild">Leave Guild</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="guildId">Guild ID to leave</param>
        /// <param name="callback">callback when the action is completed</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void LeaveGuild(DiscordClient client, Snowflake guildId, Action callback = null, Action<RequestError> error = null)
        {
            InvalidSnowflakeException.ThrowIfInvalid(guildId, nameof(guildId));
            client.Bot.Rest.CreateRequest(client,$"users/@me/guilds/{guildId}", RequestMethod.DELETE, null, callback, error);
        }

        /// <summary>
        /// Create a Direct Message to the current User
        /// See <a href="https://discord.com/developers/docs/resources/user#create-dm">Create DM</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="callback">Callback with the direct message channel</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void CreateDirectMessageChannel(DiscordClient client, Action<DiscordChannel> callback, Action<RequestError> error = null) => CreateDirectMessageChannel(client, Id, callback, error);

        /// <summary>
        /// Create a Direct Message to the current User
        /// See <a href="https://discord.com/developers/docs/resources/user#create-dm">Create DM</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="userId">User ID to send the message to</param>
        /// <param name="callback">Callback with the direct message channel</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public static void CreateDirectMessageChannel(DiscordClient client, Snowflake userId, Action<DiscordChannel> callback, Action<RequestError> error = null)
        {
            InvalidSnowflakeException.ThrowIfInvalid(userId, nameof(userId));
            InvalidChannelException.ThrowIfChannelToSelf(userId, client);

            DiscordChannel channel = client.Bot.DirectMessagesByUserId[userId];
            if (channel != null)
            {
                callback.Invoke(channel);
                return;
            }

            Dictionary<string, object> data = new Dictionary<string, object>
            {
                ["recipient_id"] = userId
            };

            client.Bot.Rest.CreateRequest<DiscordChannel>(client, "users/@me/channels", RequestMethod.POST, data, newChannel =>
            {
                client.Bot.AddDirectChannel(newChannel);
                callback?.Invoke(newChannel);
            }, error);
        }

        /// <summary>
        /// Create a Group Direct Message
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="accessTokens">access tokens of users that have granted your app the gdm.join scope</param>
        /// <param name="nicks">a list of user ids to their respective nicknames</param>
        /// <param name="callback">Callback with the direct message channel</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void CreateGroupDm(DiscordClient client, string[] accessTokens, Hash<Snowflake, string> nicks, Action<DiscordChannel> callback = null, Action<RequestError> error = null)
        {
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                ["access_tokens"] = accessTokens,
                ["nicks"] = nicks
            };

            client.Bot.Rest.CreateRequest(client,"users/@me/channels", RequestMethod.POST, data, callback, error);
        }

        /// <summary>
        /// Returns a list of connection objects.
        /// Requires the connections OAuth2 scope.
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="callback">Callback with the list of connections</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void GetUserConnections(DiscordClient client, Action<List<Connection>> callback = null, Action<RequestError> error = null)
        {
            client.Bot.Rest.CreateRequest(client,"users/@me/connections", RequestMethod.GET, null, callback, error);
        }

        /// <summary>
        /// Adds a recipient to a Group DM using their access token
        /// See <a href="https://discord.com/developers/docs/resources/channel#group-dm-add-recipient">Group DM Add Recipient</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="channel">Channel to add recipient to</param>
        /// <param name="accessToken">Users access token</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void GroupDmAddRecipient(DiscordClient client, DiscordChannel channel, string accessToken, Action callback = null, Action<RequestError> error = null) => GroupDmAddRecipient(client, channel.Id, accessToken, Username, callback, error);

        /// <summary>
        /// Adds a recipient to a Group DM using their access token
        /// See <a href="https://discord.com/developers/docs/resources/channel#group-dm-add-recipient">Group DM Add Recipient</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="channelId">Channel ID to add user to</param>
        /// <param name="accessToken">Users access token</param>
        /// <param name="nick">User nickname</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void GroupDmAddRecipient(DiscordClient client, Snowflake channelId, string accessToken, string nick, Action callback = null, Action<RequestError> error = null)
        {
            InvalidSnowflakeException.ThrowIfInvalid(channelId, nameof(channelId));
            Dictionary<string, string> data = new Dictionary<string, string>
            {
                ["access_token"] = accessToken,
                ["nick"] = nick
            };

            client.Bot.Rest.CreateRequest(client,$"channels/{channelId}/recipients/{Id}", RequestMethod.PUT, data, callback, error);
        }

        /// <summary>
        /// Removes a recipient from a Group DM
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="channel">Channel to remove recipient from</param>
        /// <param name="callback">callback once the action is completed</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void GroupDmRemoveRecipient(DiscordClient client, DiscordChannel channel, Action callback = null, Action<RequestError> error = null) => GroupDmRemoveRecipient(client, channel.Id, callback, error);

        /// <summary>
        /// Removes a recipient from a Group DM
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="channelId">Channel ID to remove recipient from</param>
        /// <param name="callback">callback once the action is completed</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void GroupDmRemoveRecipient(DiscordClient client, Snowflake channelId, Action callback = null, Action<RequestError> error = null)
        {
            InvalidSnowflakeException.ThrowIfInvalid(channelId, nameof(channelId));
            client.Bot.Rest.CreateRequest(client,$"channels/{channelId}/recipients/{Id}", RequestMethod.DELETE, null, callback, error);
        }
        #endregion

        #region Entity Update
        internal static DiscordUser FromInterface(IDiscordUser iUser)
        {
            DiscordUser user = new DiscordUser
            {
                Id = iUser.Id
            };
            
            user.Update(iUser);
            return user;
        }
        
        internal void Update(IDiscordUser update)
        {
            if (update.Username != null)
            {
                Username = update.Username;
            }

            if (update.Discriminator != null)
            {
                Discriminator = update.Discriminator;
            }

            if (update.Avatar != null)
            {
                Avatar = update.Avatar;
            }

            if (update.Bot != null)
            {
                Bot = update.Bot;
            }

            if (update.System != null)
            {
                System = update.System;
            }
            
            if (update.MfaEnabled != null)
            {
                MfaEnabled = update.MfaEnabled;
            }
            
            if (update.Banner != null)
            {
                Banner = update.Banner;
            }
            
            if (update.AccentColor != null)
            {
                AccentColor = update.AccentColor;
            }

            if (update.Locale != null)
            {
                Locale = update.Locale;
            }

            if (update.Verified != null)
            {
                Verified = update.Verified;
            }

            if (update.Email != null)
            {
                Email = update.Email;
            }

            if (update.Flags != null)
            {
                Flags = update.Flags;
            }

            if (update.PremiumType != null)
            {
                PremiumType = update.PremiumType;
            }

            if (update.PublicFlags != null)
            {
                PublicFlags = update.PublicFlags;
            }
        }
        #endregion
    }
}