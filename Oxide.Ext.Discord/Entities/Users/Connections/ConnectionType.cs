using System.ComponentModel;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Json.Converters;

namespace Oxide.Ext.Discord.Entities.Users.Connections
{
    /// <summary>
    /// Represents a <a href="https://discord.com/developers/docs/resources/user#connection-object-connection-structure">Connection Type</a> for a connection
    /// </summary>
    [JsonConverter(typeof(DiscordEnumConverter))]
    public enum ConnectionType
    {
        /// <summary>
        /// Discord Extension doesn't currently support this connection type
        /// </summary>
        Unknown,
        
        /// <summary>
        /// Connection type is Battle.net
        /// </summary>
        [Description("battlenet")] BattleNet,
        
        /// <summary>
        /// Connection type is Epic Games
        /// </summary>
        [Description("ebay")] eBay,
        
        /// <summary>
        /// Connection type is Epic Games
        /// </summary>
        [Description("epicgames")] EpicGames,
        
        /// <summary>
        /// Connection type is Facebook
        /// </summary>
        [Description("facebook")] Facebook,
        
        /// <summary>
        /// Connection type is Youtube
        /// </summary>
        [Description("github")] GitHub,
        
        /// <summary>
        /// Connection type is League of Legends
        /// </summary>
        [Description("paypal")] PayPal,
        
        /// <summary>
        /// Connection type is League of Legends
        /// </summary>
        [Description("leagueoflegends")] LeagueOfLegends,
        
        /// <summary>
        /// Connection type is PlayStation Network
        /// </summary>
        [Description("playstation")] PlayStationNetwork,
        
        /// <summary>
        /// Connection type is Reddit
        /// </summary>
        [Description("reddit")] Reddit,
        
        /// <summary>
        /// Connection type is Reddit
        /// </summary>
        [Description("riotgames")] RiotGames,

        /// <summary>
        /// Connection type is Spotify
        /// </summary>
        [Description("spotify")] Spotify,    
        
        /// <summary>
        /// Connection type is Skype
        /// </summary>
        [Description("skype")] Skype,     
        
        /// <summary>
        /// Connection type is Steam
        /// </summary>
        [Description("steam")] Steam, 
        
        /// <summary>
        /// Connection type is TikTok
        /// </summary>
        [Description("tiktok")] TikTok, 
        
        /// <summary>
        /// Connection type is Twitch
        /// </summary>
        [Description("twitch")] Twitch,      
        
        /// <summary>
        /// Connection type is Twitter
        /// </summary>
        [Description("twitter")] Twitter,   
        
        /// <summary>
        /// Connection type is Xbox
        /// </summary>
        [Description("xbox")] Xbox,        
        
        /// <summary>
        /// Connection type is Youtube
        /// </summary>
        [Description("youtube")] Youtube,
    }
}