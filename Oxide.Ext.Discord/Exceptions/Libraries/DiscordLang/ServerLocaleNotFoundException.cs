﻿using Oxide.Ext.Discord.Libraries.Locale;

namespace Oxide.Ext.Discord.Exceptions.Libraries.DiscordLang
{
    public class ServerLocaleNotFoundException : BaseDiscordException
    {
        private ServerLocaleNotFoundException(string message) : base(message) { }

        internal static ServerLocaleNotFoundException NotFound(DiscordLocale id) => throw new ServerLocaleNotFoundException($"Failed to find discord locale for server locale '{id.Id}'");
        internal static ServerLocaleNotFoundException NotFound(ServerLocale id) => throw new ServerLocaleNotFoundException($"Failed to find server local '{id.Id}'");

        internal static void ThrowNotFound(DiscordLocale id) => throw NotFound(id);
        internal static void ThrowNotFound(ServerLocale id) => throw NotFound(id);
    }
}