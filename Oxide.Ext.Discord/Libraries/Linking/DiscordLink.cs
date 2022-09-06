using System;
using System.Collections.Generic;
using Oxide.Core;
using Oxide.Core.Libraries;
using Oxide.Core.Libraries.Covalence;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Cache;
using Oxide.Ext.Discord.Constants;
using Oxide.Ext.Discord.Entities;
using Oxide.Ext.Discord.Entities.Guilds;
using Oxide.Ext.Discord.Entities.Users;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Hooks;
using Oxide.Ext.Discord.Logging;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Libraries.Linking
{
    /// <summary>
    /// Represents a library for discord linking
    /// </summary>
    public class DiscordLink : Library
    {
        private IPlayerManager Players => _players ?? (_players = Interface.Oxide.GetLibrary<Covalence>().Players);
        private IPlayerManager _players;

        private readonly Hash<string, Snowflake> _steamIdToDiscordId = new Hash<string, Snowflake>();
        private readonly Hash<Snowflake, string> _discordIdToSteamId = new Hash<Snowflake, string>();
        private readonly HashSet<string> _steamIds = new HashSet<string>();
        private readonly HashSet<Snowflake> _discordIds = new HashSet<Snowflake>();
        private readonly Hash<string, IDictionary<string, Snowflake>> _pluginLinks = new Hash<string, IDictionary<string, Snowflake>>();

        private readonly List<IDiscordLinkPlugin> _linkPlugins = new List<IDiscordLinkPlugin>();

        private readonly ILogger _logger;

        /// <summary>
        /// DiscordLink Constructor
        /// </summary>
        /// <param name="logger">Logger for Discord Link</param>
        public DiscordLink(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Returns if there is a registered link plugin
        /// </summary>
        /// <returns></returns>
        public bool IsEnabled()
        {
            return _linkPlugins.Count != 0;
        }

        /// <summary>
        /// Adds a link plugin to be the plugin used with the Discord Link library
        /// </summary>
        /// <param name="plugin"></param>
        public void AddLinkPlugin(IDiscordLinkPlugin plugin)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));

            if (!_linkPlugins.Contains(plugin))
            {
                _linkPlugins.Add(plugin);
            }

            IDictionary<string, Snowflake> data = plugin.GetSteamToDiscordIds();
            if (data == null)
            {
                _logger.Error($"{{0}} returned null when {nameof(plugin.GetSteamToDiscordIds)} was called", plugin.Name);
                return;
            }

            _pluginLinks[plugin.Name] = data;

            foreach (KeyValuePair<string,Snowflake> pair in data)
            {
                _steamIdToDiscordId[pair.Key] = pair.Value;
                _discordIdToSteamId[pair.Value] = pair.Key;
                _steamIds.Add(pair.Key);
                _discordIds.Add(pair.Value);
            }
            
            _logger.Debug("{0} has been registered as a DiscordLink plugin", plugin.Name);
        }

        /// <summary>
        /// Removes a link plugin from the Discord Link library
        /// </summary>
        /// <param name="plugin"></param>
        public void RemoveLinkPlugin(IDiscordLinkPlugin plugin)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));

            IDictionary<string, Snowflake> pluginData = _pluginLinks[plugin.Name];
            if (pluginData != null)
            {
                foreach (KeyValuePair<string,Snowflake> linkData in pluginData)
                {
                    _steamIdToDiscordId.Remove(linkData.Key);
                    _discordIdToSteamId.Remove(linkData.Value);
                    _steamIds.Remove(linkData.Key);
                    _discordIds.Remove(linkData.Value);
                }
            }
            
            _linkPlugins.Remove(plugin);
        }

        internal void OnPluginUnloaded(Plugin plugin)
        {
            if (plugin is IDiscordLinkPlugin link)
            {
                RemoveLinkPlugin(link);
            }
        }

        /// <summary>
        /// Returns if the specified ID is linked
        /// </summary>
        /// <param name="steamId">Steam ID of the player</param>
        /// <returns>True if the ID is linked; false otherwise</returns>
        public bool IsLinked(string steamId)
        {
            return GetSteamToDiscordIds()?.ContainsKey(steamId) ?? false;
        }
        
        /// <summary>
        /// Returns if the specified ID is linked
        /// </summary>
        /// <param name="discordId">Discord ID of the player</param>
        /// <returns>True if the ID is linked; false otherwise</returns>
        public bool IsLinked(Snowflake discordId)
        {
            return GetDiscordToSteamIds()?.ContainsKey(discordId) ?? false;
        }
        
        /// <summary>
        /// Returns if the specified ID is linked
        /// </summary>
        /// <param name="player">Player to check if linked</param>
        /// <returns>True if the player is linked; false otherwise</returns>
        public bool IsLinked(IPlayer player)
        {
            return IsLinked(player.Id);
        }
        
        /// <summary>
        /// Returns if the specified ID is linked
        /// </summary>
        /// <param name="user">Discord user to check</param>
        /// <returns>True if the user is linked; false otherwise</returns>
        public bool IsLinked(DiscordUser user)
        {
            return IsLinked(user.Id);
        }

        /// <summary>
        /// Returns the Steam ID of the given Discord ID if there is a link
        /// </summary>
        /// <param name="discordId">Discord ID to get steam ID for</param>
        /// <returns>Steam ID of the given given discord ID if linked; null otherwise</returns>
        public string GetSteamId(Snowflake discordId)
        {
            return GetDiscordToSteamIds()?[discordId];
        }
        
        /// <summary>
        /// Returns the Steam ID of the given Discord ID if there is a link
        /// </summary>
        /// <param name="user"><see cref="DiscordUser"/> to get steam Id for</param>
        /// <returns>Steam ID of the given given discord ID if linked; null otherwise</returns>
        public string GetSteamId(DiscordUser user)
        {
            return GetSteamId(user.Id);
        }

        /// <summary>
        /// Returns the IPlayer for the given Discord ID
        /// </summary>
        /// <param name="discordId">Discord ID to get IPlayer for</param>
        /// <returns>IPlayer for the given Discord ID; null otherwise</returns>
        public IPlayer GetPlayer(Snowflake discordId)
        {
            string id = GetSteamId(discordId);
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            return ServerPlayerCache.GetPlayer(id);
        }

        /// <summary>
        /// Returns the Discord ID for the given Steam ID
        /// </summary>
        /// <param name="steamId">Steam ID to get Discord ID for</param>
        /// <returns>Discord ID for the given Steam ID; null otherwise</returns>
        public Snowflake? GetDiscordId(string steamId)
        {
            return GetSteamToDiscordIds()?[steamId];
        }

        /// <summary>
        /// Returns the Discord ID for the given IPlayer
        /// </summary>
        /// <param name="player">Player to get Discord ID for</param>
        /// <returns>Discord ID for the given Steam ID; null otherwise</returns>
        public Snowflake? GetDiscordId(IPlayer player)
        {
            return GetDiscordId(player.Id);
        }

        /// <summary>
        /// Returns a minimal Discord User
        /// </summary>
        /// <param name="steamId">ID of the in game player</param>
        /// <returns>Discord ID for the given Steam ID; null otherwise</returns>
        public DiscordUser GetDiscordUser(string steamId)
        {
            Snowflake? discordId = GetDiscordId(steamId);
            if (!discordId.HasValue)
            {
                return null;
            }

            return DiscordUserCache.GetOrCreate(discordId.Value);
        }
        
        /// <summary>
        /// Returns a minimal Discord User
        /// </summary>
        /// <param name="player">Player to get the Discord User for</param>
        /// <returns>Discord ID for the given Steam ID; null otherwise</returns>
        public DiscordUser GetDiscordUser(IPlayer player)
        {
            return GetDiscordUser(player.Id);
        }

        /// <summary>
        /// Returns a linked guild member for the matching steam id in the given guild
        /// </summary>
        /// <param name="steamId">ID of the in game player</param>
        /// <param name="guild">Guild the member is in</param>
        /// <returns>Discord ID for the given Steam ID; null otherwise</returns>
        public GuildMember GetLinkedMember(string steamId, DiscordGuild guild)
        {
            Snowflake? discordId = GetDiscordId(steamId);
            if (!discordId.HasValue || !guild.IsAvailable)
            {
                return null;
            }

            return guild.Members[discordId.Value];
        }

        /// <summary>
        /// Returns a linked guild member for the matching <see cref="IPlayer"/> in the given guild
        /// </summary>
        /// <param name="player">Player to get the Discord User for</param>
        /// <param name="guild">Guild the member is in</param>
        /// <returns>Discord ID for the given Steam ID; null otherwise</returns>
        public GuildMember GetLinkedMember(IPlayer player, DiscordGuild guild)
        {
            return GetLinkedMember(player.Id, guild);
        }

        /// <summary>
        /// Returns the number of linked players
        /// </summary>
        /// <returns></returns>
        public int GetLinkedCount()
        {
            return GetSteamToDiscordIds()?.Count ?? 0;
        }

        /// <summary>
        /// Returns Steam ID's for all linked players
        /// </summary>
        /// <returns></returns>
        public HashSet<string> GetSteamIds()
        {
            return _steamIds;
        }

        /// <summary>
        /// Returns Discord ID's for all linked players
        /// </summary>
        /// <returns></returns>
        public HashSet<Snowflake> GetDiscordIds()
        {
            return _discordIds;
        }

        /// <summary>
        /// Returns a Hash with a Steam ID key and Discord ID value
        /// </summary>
        /// <returns></returns>
        public Hash<string, Snowflake> GetSteamToDiscordIds()
        {
            return _steamIdToDiscordId;
        }

        /// <summary>
        /// Returns a Hash with a Discord ID key and Steam ID value
        /// </summary>
        /// <returns></returns>
        public Hash<Snowflake, string> GetDiscordToSteamIds()
        {
            return _discordIdToSteamId;
        }

        /// <summary>
        /// Called by a link plugin when a link occured
        /// </summary>
        /// <param name="plugin">Plugin that initiated the link</param>
        /// <param name="player">Player being linked</param>
        /// <param name="discord">DiscordUser being linked</param>
        public void OnLinked(Plugin plugin, IPlayer player, DiscordUser discord)
        {
            if (player == null) throw new ArgumentNullException(nameof(player));
            if (discord == null) throw new ArgumentNullException(nameof(discord));
            
            IDiscordLinkPlugin link = plugin as IDiscordLinkPlugin;
            if (link == null)
            {
                _logger.Error($"{{0}} tried to link but does not inherit from interface {nameof(IDiscordLinkPlugin)}", plugin.FullName());
                return;
            }
            
            if (!_linkPlugins.Contains(link))
            {
                _logger.Error("{0} has not been added as a link plugin and cannot set a link", plugin.FullName());
                return;
            }
            
            _pluginLinks[plugin.Id()][player.Id] = discord.Id;

            _discordIdToSteamId[discord.Id] = player.Id;
            _steamIdToDiscordId[player.Id] = discord.Id;
            _steamIds.Add(player.Id);
            _discordIds.Add(discord.Id);
            ServerPlayerCache.SetPlayer(player);
            DiscordHook.CallGlobalHook(DiscordExtHooks.OnDiscordPlayerLinked, player, discord);
        }

        /// <summary>
        /// Called by a link plugin when an unlink occured
        /// </summary>
        /// <param name="plugin">Plugin that is unlinking</param>
        /// <param name="player">Player being unlinked</param>
        /// <param name="discord">DiscordUser being unlinked</param>
        public void OnUnlinked(Plugin plugin, IPlayer player, DiscordUser discord)
        {
            if (player == null) throw new ArgumentNullException(nameof(player));
            if (discord == null) throw new ArgumentNullException(nameof(discord));
            
            IDiscordLinkPlugin link = plugin as IDiscordLinkPlugin;
            if (link == null)
            {
                _logger.Error($"{{0}} tried to unlink but does not inherit from interface {nameof(IDiscordLinkPlugin)}", plugin.FullName());
                return;
            }
            
            if (!_linkPlugins.Contains(link))
            {
                _logger.Error("{0} has not been added as a link plugin and cannot unlink", plugin.FullName());
                return;
            }
            
            DiscordHook.CallGlobalHook(DiscordExtHooks.OnDiscordPlayerUnlink, player, discord);

            _pluginLinks[plugin.Id()].Remove(player.Id);
            
            _discordIdToSteamId.Remove(discord.Id);
            _steamIdToDiscordId.Remove(player.Id);
            _steamIds.Remove(player.Id);
            _discordIds.Remove(discord.Id);
            DiscordHook.CallGlobalHook(DiscordExtHooks.OnDiscordPlayerUnlinked, player, discord);
        }
    }
}