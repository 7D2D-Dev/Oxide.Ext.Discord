using Newtonsoft.Json;
using Oxide.Ext.Discord.Exceptions.Entities.Webhooks;
using Oxide.Ext.Discord.Interfaces;

namespace Oxide.Ext.Discord.Entities.Webhooks
{
    /// <summary>
    /// Represents a <a href="https://discord.com/developers/docs/resources/webhook#create-webhook-json-params">Webhook Create Structure</a>
    /// </summary>
    public class WebhookCreate : IDiscordValidation
    {
        /// <summary>
        /// Name of the webhook (1-80 characters)
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        
        /// <summary>
        /// Image for the default webhook avatar
        /// </summary>
        [JsonProperty("avatar")]
        public string Avatar { get; set; }
        
        /// <inheritdoc/>
        public void Validate()
        {
            InvalidWebhookException.ThrowIfInvalidName(Name);
        }
    }
}