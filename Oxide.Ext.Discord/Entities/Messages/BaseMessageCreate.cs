using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Interactions.MessageComponents;
using Oxide.Ext.Discord.Entities.Messages.AllowedMentions;
using Oxide.Ext.Discord.Entities.Messages.Embeds;
using Oxide.Ext.Discord.Exceptions.Entities.Messages;
using Oxide.Ext.Discord.Interfaces;

namespace Oxide.Ext.Discord.Entities.Messages
{
    /// <summary>
    /// Represents a base message in discord
    /// </summary>
    public abstract class BaseMessageCreate : IFileAttachments, IDiscordValidation
    {
        /// <summary>
        /// Contents of the message
        /// </summary>
        [JsonProperty("content")]
        public string Content { get; set; }

        /// <summary>
        /// Whether this was a TTS message
        /// </summary>
        [JsonProperty("tts")]
        public bool? Tts { get; set; }
        
        /// <summary>
        /// Embeds for the message
        /// </summary>
        [JsonProperty("embeds")]
        public List<DiscordEmbed> Embeds { get; set; }
        
        /// <summary>
        /// Allowed mentions for a message
        /// Allows for more granular control over mentions without various hacks to the message content. 
        /// </summary>
        [JsonProperty("allowed_mentions")]
        public AllowedMention AllowedMention { get; set; }
        
        /// <summary>
        /// Used to create message components on a message
        /// </summary>
        [JsonProperty("components")]
        public List<ActionRowComponent> Components { get; set; }
        
        /// <summary>
        /// Attachments for the message
        /// </summary>
        [JsonProperty("flags")]
        public MessageFlags? Flags { get; set; }
        
        /// <summary>
        /// Attachments for a discord message
        /// </summary>
        public List<MessageFileAttachment> FileAttachments { get; set; }
        
        /// <summary>
        /// Attachments for the message
        /// </summary>
        [JsonProperty("attachments")]
        public List<MessageAttachment> Attachments { get; set; }

        /// <summary>
        /// Adds an attachment to the message
        /// </summary>
        /// <param name="filename">Name of the file</param>
        /// <param name="data">byte[] of the attachment</param>
        /// <param name="contentType">Attachment content type</param>
        /// <param name="description">Description for the attachment</param>
        public void AddAttachment(string filename, byte[] data, string contentType, string description = null)
        {
            InvalidFileNameException.ThrowIfInvalid(filename);

            if (FileAttachments == null)
            {
                FileAttachments = new List<MessageFileAttachment>();
            }

            if (Attachments == null)
            {
                Attachments = new List<MessageAttachment>();
            }

            FileAttachments.Add(new MessageFileAttachment(filename, data, contentType));
            Attachments.Add(new MessageAttachment {Id = new Snowflake((ulong)FileAttachments.Count), Filename = filename, Description = description});
        }

        /// <inheritdoc/>
        public void Validate()
        {
            InvalidMessageException.ThrowIfMissingRequiredField(this);
            InvalidMessageException.ThrowIfInvalidContent(Content);
            ValidateFlags();
        }

        /// <summary>
        /// Validates that the message flags are correct for the message type
        /// </summary>
        /// <exception cref="InvalidMessageException"></exception>
        protected virtual void ValidateFlags()
        {
            InvalidMessageException.ThrowIfInvalidFlags(Flags, MessageFlags.SuppressEmbeds, "Invalid Message Flags Used for Channel Message. Only supported flags are MessageFlags.SuppressEmbeds");
        }
    }
}