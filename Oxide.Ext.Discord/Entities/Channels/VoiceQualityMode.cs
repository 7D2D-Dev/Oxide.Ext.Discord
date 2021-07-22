namespace Oxide.Ext.Discord.Entities.Channels
{
    /// <summary>
    /// Represents a <a href="https://discord.com/developers/docs/resources/channel#channel-object-video-quality-modes">Video Quality Mode</a>
    /// </summary>
    public enum VoiceQualityMode
    {
        /// <summary>
        /// Discord chooses the quality for optimal performance
        /// </summary>
        Auto = 1,
        
        /// <summary>
        /// 720p
        /// </summary>
        Full = 2
    }
}