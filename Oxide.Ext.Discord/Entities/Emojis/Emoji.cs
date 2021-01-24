﻿using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Users;
using Oxide.Ext.Discord.Helpers.Cdn;

namespace Oxide.Ext.Discord.Entities.Emojis
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]

    public class Emoji : EmojiUpdate
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("user")]
        public DiscordUser User { get; set; }

        [JsonProperty("require_colons")]
        public bool? RequireColons { get; set; }

        [JsonProperty("managed")]
        public bool? Managed { get; set; }

        [JsonProperty("animated")]
        public bool? Animated { get; set; }
        
        [JsonProperty("available")]
        public bool? Available { get; set; }
        
        public string Url => DiscordCdn.GetCustomEmojiUrl(Id, Animated.HasValue && Animated.Value ? ImageFormat.Gif : ImageFormat.Png);
    }
}