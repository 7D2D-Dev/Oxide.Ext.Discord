﻿using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Channels;
using Oxide.Ext.Discord.Entities.Guilds;
using Oxide.Ext.Discord.Entities.Interactions;
using Oxide.Ext.Discord.Entities.Users;

namespace Oxide.Ext.Discord.Entities.Gatway.Events
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Ready
    {
        [JsonProperty("v")]
        public int Version { get; private set; }
        
        [JsonProperty("user")]
        public DiscordUser User { get; set; }

        [JsonProperty("guilds")]
        public List<Guild> Guilds { get; set; }

        [JsonProperty("session_id")]
        public string SessionId { get; set; }

        [JsonProperty("shard")]
        public List<int> Shard { get; set; }
        
        [JsonProperty("application")]
        public Application Application { get; set; }
    }
}