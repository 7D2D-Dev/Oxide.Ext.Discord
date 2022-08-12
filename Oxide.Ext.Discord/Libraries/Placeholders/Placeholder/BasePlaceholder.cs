using System.Text;
using Oxide.Core.Plugins;
using Oxide.Ext.Discord.Extensions;

namespace Oxide.Ext.Discord.Libraries.Placeholders.Placeholder
{
    internal abstract class BasePlaceholder
    {
        protected readonly string DataKey;
        internal readonly Plugin Plugin;

        protected BasePlaceholder(string dataKey, Plugin plugin)
        {
            DataKey = dataKey;
            Plugin = plugin;
        }

        public abstract void Invoke(StringBuilder builder, PlaceholderMatch match, PlaceholderData data);

        public bool IsExtensionPlaceholder()
        {
            return Plugin == null;
        }
        
        public bool IsForPlugin(Plugin plugin)
        {
            return !IsExtensionPlaceholder() && plugin.Id() == Plugin.Id();
        }
    }
}