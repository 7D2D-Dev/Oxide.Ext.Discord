using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Ext.Discord.Exceptions.Entities.Interactions.ApplicationCommands;
using Oxide.Ext.Discord.Interfaces;
using Oxide.Plugins;

namespace Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands
{
    /// <summary>
    /// Represents <a href="https://discord.com/developers/docs/interactions/application-commands#create-guild-application-command-json-params">Application Command Create</a>
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class CommandCreate : IDiscordValidation
    {
        /// <summary>
        /// 1-32 lowercase character name matching ^[-_\p{L}\p{N}\p{sc=Deva}\p{sc=Thai}]{1,32}$
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        
        /// <summary>
        /// Localization dictionary for the name field. Values follow the same restrictions as name
        /// </summary>
        [JsonProperty("name_localizations")]
        public Hash<string, string> NameLocalizations { get; set; }
        
        /// <summary>
        /// Description of the command (1-100 characters)
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
        
        /// <summary>
        /// Localization dictionary for the description field. Values follow the same restrictions as description
        /// </summary>
        [JsonProperty("description_localizations")]
        public Hash<string, string> DescriptionLocalizations { get; set; }
        
        /// <summary>
        /// The parameters for the command
        /// See <see cref="CommandOption"/>
        /// </summary>
        [JsonProperty("options")]
        public List<CommandOption> Options { get; set; }
        
        /// <summary>
        /// Whether the command is enabled by default when the app is added to a guild
        /// </summary>
        [JsonProperty("default_permission")]
        public bool? DefaultPermissions { get; set; }
        
        /// <summary>
        /// The type of command, defaults Slash Command if not set
        /// </summary>
        [JsonProperty("type")]
        public ApplicationCommandType? Type { get; set; }

        /// <inheritdoc/>
        public void Validate()
        {
            InvalidApplicationCommandException.ThrowIfInvalidName(Name, false);
            InvalidApplicationCommandException.ThrowIfInvalidDescription(Name, false);
        }
    }
}