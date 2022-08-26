using System;
using System.ComponentModel;

namespace Oxide.Ext.Discord.Entities.Messages
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/resources/channel#message-object-message-flags">Message Flags</a> for a message
    /// </summary>
    [Flags]
    public enum MessageFlags
    {
        /// <summary>
        /// This message has no flags
        /// </summary>
        None = 0,
        
        /// <summary>
        /// This message has been published to subscribed channels (via Channel Following)
        /// </summary>
        [Description("CROSSPOSTED")]
        CrossPosted = 1 << 0,

        /// <summary>
        /// This message originated from a message in another channel (via Channel Following)
        /// </summary>
        [Description("IS_CROSSPOST")]
        IsCrossPost = 1 << 1,

        /// <summary>
        /// Do not include any embeds when serializing this message
        /// </summary>
        [Description("SUPPRESS_EMBEDS")]
        SuppressEmbeds = 1 << 2,

        /// <summary>
        /// The source message for this crosspost has been deleted (via Channel Following)
        /// </summary>
        [Description("SOURCE_MESSAGE_DELETED")]
        SourceMessageDeleted = 1 << 3,

        /// <summary>
        /// This message came from the urgent message system
        /// </summary>
        [Description("URGENT")]
        Urgent = 1 << 4,

        /// <summary>
        /// This message has an associated thread, with the same id as the message
        /// </summary>
        [Description("HAS_THREAD")]
        HasThread = 1 << 5,
        
        /// <summary>
        /// This message is only visible to the user who invoked the Interaction
        /// </summary>
        [Description("EPHEMERAL")]
        Ephemeral = 1 << 6,

        /// <summary>
        /// This message is an Interaction Response and the bot is "thinking"
        /// </summary>
        [Description("LOADING")]
        Loading = 1 << 7
    }
}