using System;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Api;
using Oxide.Ext.Discord.Entities.Guilds;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;
using Oxide.Ext.Discord.Entities.Messages;
using Oxide.Ext.Discord.Entities.Webhooks;

namespace Oxide.Ext.Discord.Entities.Interactions
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/interactions/slash-commands#interaction">Interaction Structure</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class DiscordInteraction
    {
        /// <summary>
        /// Id of the interaction
        /// </summary>
        [JsonProperty("id")]
        public Snowflake Id { get; set; }
        
        /// <summary>
        /// ID of the application this interaction is for
        /// </summary>
        [JsonProperty("application_id")]
        public Snowflake ApplicationId { get; set; }
        
        /// <summary>
        /// The type of interaction
        /// See <see cref="InteractionType"/>
        /// </summary>
        [JsonProperty("type")]
        public InteractionType Type { get; set; }
        
        /// <summary>
        /// The command data payload
        /// See <see cref="CommandInteractionData"/>
        /// </summary>
        [JsonProperty("data")]
        public CommandInteractionData Data { get; set; }
        
        /// <summary>
        /// The guild it was sent from
        /// </summary>
        [JsonProperty("guild_id")]
        public Snowflake? GuildId { get; set; }    
        
        /// <summary>
        /// The channel it was sent from
        /// </summary>
        [JsonProperty("channel_id")]
        public Snowflake? ChannelId { get; set; }
        
        /// <summary>
        /// Guild member data for the invoking user
        /// </summary>
        [JsonProperty("member")]
        public GuildMember Member { get; set; }
        
        /// <summary>
        /// A continuation token for responding to the interaction
        /// Interaction tokens are valid for 15 minutes and can be used to send followup messages but you must send an initial response within 3 seconds of receiving the event.
        /// If the 3 second deadline is exceeded, the token will be invalidated.
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; set; } 
        
        /// <summary>
        /// Read-only property, always 1
        /// </summary>
        [JsonProperty("version")]
        public int Version { get; set; } 
        
        /// <summary>
        ///  For components, the message they were attached to
        /// </summary>
        [JsonProperty("message")]
        public DiscordMessage Message { get; set; } 
        
        /// <summary>
        /// Create a response to an Interaction from the gateway.
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="response">Response to respond with</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void CreateResponse(DiscordClient client, InteractionResponse response, Action callback = null, Action<RestError> error = null)
        {
            client.Bot.Rest.DoRequest($"/interactions/{Id}/{Token}/callback", RequestMethod.POST, response, callback, error);
        }
        
                /// <summary>
        /// Edits the initial Interaction response
        /// See <a href="https://discord.com/developers/docs/interactions/slash-commands#edit-original-interaction-response">Edit Original Interaction Response</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// /// <param name="message">Updated message</param>
        /// <param name="callback">Callback with the created message</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void EditOriginalInteractionResponse(DiscordClient client, DiscordMessage message, Action<DiscordMessage> callback = null, Action<RestError> error = null)
        {
            client.Bot.Rest.DoRequest($"/webhooks/{ApplicationId}/{Token}/messages/@original", RequestMethod.PATCH, message, callback, error);
        }

        /// <summary>
        /// Deletes the initial Interaction response
        /// See <a href="https://discord.com/developers/docs/interactions/slash-commands#delete-original-interaction-response">Delete Original Interaction Response</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="callback">Callback once the action is completed</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void DeleteOriginalInteractionResponse(DiscordClient client, Action callback = null, Action<RestError> error = null)
        {
            client.Bot.Rest.DoRequest($"/webhooks/{ApplicationId}/{Token}/messages/@original", RequestMethod.DELETE, null, callback, error);
        }

        /// <summary>
        /// Create a followup message for an Interaction
        /// See <a href="https://discord.com/developers/docs/interactions/slash-commands#create-followup-message">Create Followup Message</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="message">Message to follow up with</param>
        /// <param name="callback">Callback with the message</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void CreateFollowUpMessage(DiscordClient client, CommandFollowupCreate message, Action<DiscordMessage> callback = null, Action<RestError> error = null)
        {
            client.Bot.Rest.DoRequest($"/webhooks/{ApplicationId}/{Token}", RequestMethod.POST, message, callback, error);
        }

        /// <summary>
        /// Edits a followup message for an Interaction
        /// See <a href="https://discord.com/developers/docs/interactions/slash-commands#edit-followup-message">Edit Followup Message</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="messageId">Message ID of the follow up message</param>
        /// <param name="edit">Updated message</param>
        /// <param name="callback">Callback with the updated message</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void EditFollowUpMessage(DiscordClient client, Snowflake messageId, CommandFollowupUpdate edit, Action<DiscordMessage> callback = null, Action<RestError> error = null)
        {
            client.Bot.Rest.DoRequest($"/webhooks/{ApplicationId}/{Token}/messages/{messageId}", RequestMethod.PATCH, edit, callback, error);
        }

        /// <summary>
        /// Deletes a followup message for an Interaction
        /// See <a href="https://discord.com/developers/docs/interactions/slash-commands#delete-followup-message">Delete Followup Message</a>
        /// </summary>
        /// <param name="client">Client to use</param>
        /// <param name="messageId">Message ID to delete</param>
        /// <param name="callback">Callback with the updated message</param>
        /// <param name="error">Callback when an error occurs with error information</param>
        public void DeleteFollowUpMessage(DiscordClient client, Snowflake messageId, Action callback = null, Action<RestError> error = null)
        {
            client.Bot.Rest.DoRequest($"/webhooks/{ApplicationId}/{Token}/messages/{messageId}", RequestMethod.DELETE, null, callback, error);
        }
    }
}