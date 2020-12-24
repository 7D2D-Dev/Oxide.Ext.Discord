using Newtonsoft.Json;
using Oxide.Ext.Discord.Entities.Users;

namespace Oxide.Ext.Discord.Entities.Gatway.Events
{
    public class GuildBanEvent
    {
        [JsonProperty("guild_id")]
        public string GuildId { get; set; }
        
        [JsonProperty("user")]
        public DiscordUser User { get; set; }
    }
}
