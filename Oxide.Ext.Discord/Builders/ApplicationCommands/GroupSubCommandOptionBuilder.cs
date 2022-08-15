using System.Collections.Generic;
using Oxide.Ext.Discord.Builders.ApplicationCommands.BaseBuilders;
using Oxide.Ext.Discord.Entities.Interactions.ApplicationCommands;

namespace Oxide.Ext.Discord.Builders.ApplicationCommands
{
    /// <summary>
    /// Represents a Subcommand Option Builder for SubCommands
    /// </summary>
    public class GroupSubCommandOptionBuilder : BaseCommandOptionBuilder<GroupSubCommandOptionBuilder, GroupSubCommandBuilder>
    {
        internal GroupSubCommandOptionBuilder(List<CommandOption> parent, CommandOptionType type, string name, string description, GroupSubCommandBuilder builder) : base(parent, type, name, description, builder) { }
    }
}