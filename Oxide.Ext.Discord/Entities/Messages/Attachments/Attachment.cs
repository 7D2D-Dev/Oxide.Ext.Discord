﻿using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Messages.Attachments
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Attachment
    {
        [JsonProperty("id")]
        public Snowflake Id { get; set; }

        [JsonProperty("filename")]
        public string Filename { get; set; }

        [JsonProperty("size")]
        public int? Size { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("proxy_url")]
        public string ProxyUrl { get; set; }

        [JsonProperty("height")]
        public int? Height { get; set; }

        [JsonProperty("width")]
        public int? Width { get; set; }
    }
}
