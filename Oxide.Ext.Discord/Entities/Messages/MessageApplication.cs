using Newtonsoft.Json;

namespace Oxide.Ext.Discord.Entities.Messages
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class MessageApplication
    {
        [JsonProperty("id")]
        public Snowflake Id { get; set; }
        
        [JsonProperty("cover_image")]
        public string CoverImage { get; set; }      
        
        [JsonProperty("description")]
        public string Description { get; set; }
        
        [JsonProperty("icon")]
        public string Icon { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}