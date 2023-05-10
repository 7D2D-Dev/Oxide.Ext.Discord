﻿using System;
using System.Collections.Generic;
using System.Linq;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Cache;
using Oxide.Ext.Discord.Extensions;
using Oxide.Ext.Discord.Interfaces;
using Oxide.Ext.Discord.Libraries;
using Oxide.Ext.Discord.Logging;
using Oxide.Ext.Discord.Singleton;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Factory
{
    /// <summary>
    /// Factory for creating <see cref="DiscordClient"/>
    /// </summary>
    public class DiscordClientFactory : Singleton<DiscordClientFactory>
    {
        private readonly Hash<string, DiscordClient> _clients = new Hash<string, DiscordClient>();
        internal IEnumerable<DiscordClient> Clients => _clients.Values;
        
        private DiscordClientFactory() { }
        
        #region Client Handling
        /// <summary>
        /// Creates the client for the given plugin. If one already exist the existing one is returned
        /// </summary>
        /// <param name="plugin">Plugin the client is for</param>
        /// <returns>DiscordClient for plugin</returns>
        /// <exception cref="ArgumentNullException">Thrown is plugin is null</exception>
        public DiscordClient CreateClient(Plugin plugin)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            DiscordPluginCache.Instance.OnPluginLoaded(plugin);
            OnPluginRemoved(plugin);

            // ReSharper disable once SuspiciousTypeConversion.Global
            if (!(plugin is IDiscordPlugin discordPlugin))
            {
                return null;
            }

            DiscordClient client = _clients[plugin.Id()];
            if (client == null)
            {
                DiscordExtension.GlobalLogger.Debug($"{nameof(DiscordClient)}.{nameof(OnPluginAdded)} Creating DiscordClient for plugin {{0}}", plugin.FullName());
                client = new DiscordClient(plugin);
                _clients[plugin.Id()] = client;
            }

            discordPlugin.Client = client;
            return client;
        }

        /// <summary>
        /// Gets the client for the given plugin
        /// </summary>
        /// <param name="plugin">Plugin to get client for</param>
        /// <returns>Discord client for the plugin</returns>
        public DiscordClient GetClient(Plugin plugin)
        {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            return GetClient(plugin.Id());
        }

        /// <summary>
        /// Gets the client for the given plugin name
        /// </summary>
        /// <param name="pluginName">Plugin Name to get client for</param>
        /// <returns>Discord client for the plugin name</returns>
        public DiscordClient GetClient(string pluginName)
        {
            if (pluginName == null) throw new ArgumentNullException(nameof(pluginName));
            return _clients[pluginName];
        }
        #endregion
        
        #region Plugin Handling
        internal void OnPluginAdded(Plugin plugin)
        {
            if (!plugin.IsCorePlugin)
            {
                CreateClient(plugin);
            }
        }

        internal void OnPluginRemoved(Plugin plugin)
        {
            if (plugin.IsCorePlugin)
            {
                return;
            }

            if (DiscordExtension.IsShuttingDown)
            {
                return;
            }

            DiscordClient client = _clients[plugin.Id()];
            if (client != null)
            {
                client.CloseClient();
                BaseDiscordLibrary.ProcessPluginUnloaded(plugin);
            }

            PluginExt.OnPluginUnloaded(plugin);
            DiscordPluginCache.Instance.OnPluginUnloaded(plugin);
            DiscordLoggerFactory.Instance.OnPluginUnloaded(plugin);
        }

        internal void RemoveClient(DiscordClient client)
        {
            _clients.Remove(client.PluginId);
        }

        internal void OnShutdown()
        {
            foreach (DiscordClient client in Clients.ToList())
            {
                client.CloseClient();
            }
        }
        #endregion
    }
}