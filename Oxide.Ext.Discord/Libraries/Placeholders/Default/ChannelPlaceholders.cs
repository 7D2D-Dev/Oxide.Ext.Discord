using System.Text;
using Oxide.Ext.Discord.Entities.Channels;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Default
{
    internal class ChannelPlaceholders
    {
        private static void Id(StringBuilder builder, DiscordChannel channel, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, channel.Id);
        private static void Name(StringBuilder builder, DiscordChannel channel, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, channel.Name);
        private static void Icon(StringBuilder builder, DiscordChannel channel, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, channel.IconUrl);
        private static void Topic(StringBuilder builder, DiscordChannel channel, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, channel.Topic);
        private static void Mention(StringBuilder builder, DiscordChannel channel, PlaceholderMatch match) => PlaceholderFormatting.Replace(builder, match, channel.Mention);

        public static void RegisterPlaceholders(DiscordPlaceholders placeholders)
        {
            placeholders.RegisterInternalPlaceholder<DiscordChannel>("channel.id", GetDataKey(), Id);
            placeholders.RegisterInternalPlaceholder<DiscordChannel>("channel.name", GetDataKey(), Name);
            placeholders.RegisterInternalPlaceholder<DiscordChannel>("channel.icon", GetDataKey(), Icon);
            placeholders.RegisterInternalPlaceholder<DiscordChannel>("channel.topic", GetDataKey(), Topic);
            placeholders.RegisterInternalPlaceholder<DiscordChannel>("channel.mention", GetDataKey(), Mention);
        }
        
        private static string GetDataKey() => nameof(DiscordChannel);
    }
}