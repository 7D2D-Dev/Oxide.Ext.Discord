using System;
using Oxide.Ext.Discord.Entities.Channels;
using Oxide.Ext.Discord.Entities.Emojis;
using Oxide.Ext.Discord.Entities.Interactions.MessageComponents;
using Oxide.Ext.Discord.Entities.Interactions.MessageComponents.SelectMenus;
using Oxide.Ext.Discord.Exceptions.Entities.Interactions.MessageComponents;

namespace Oxide.Ext.Discord.Builders.MessageComponents
{
    /// <summary>
    /// Builder for Select Menus
    /// </summary>
    public class SelectMenuComponentBuilder
    {
        private readonly BaseSelectMenuComponent _menu;
        private readonly MessageComponentBuilder _builder;
        
        internal SelectMenuComponentBuilder(BaseSelectMenuComponent menu, MessageComponentBuilder builder)
        {
            _menu = menu;
            _builder = builder;
        }

        /// <summary>
        /// Adds an option to a select menu;
        /// </summary>
        /// <param name="label">Display text for the select option</param>
        /// <param name="value">Selected value for the select option</param>
        /// <param name="description">Description of the select option</param>
        /// <param name="default">Default selected option</param>
        /// <param name="emoji">Emoji to display with the option</param>
        /// <exception cref="Exception">Throw is more than 25 options are added</exception>
        public SelectMenuComponentBuilder AddOption(string label, string value, string description, bool @default = false, DiscordEmoji emoji = null)
        {
            InvalidSelectMenuComponentException.ThrowIfTypeCantAddOptions(_menu.Type);
            InvalidSelectMenuComponentException.ThrowIfInvalidSelectMenuOptionLabel(label);
            InvalidSelectMenuComponentException.ThrowIfInvalidSelectMenuOptionValue(value);
            InvalidSelectMenuComponentException.ThrowIfInvalidSelectMenuOptionDescription(description);

            StringSelectComponent text = (StringSelectComponent)_menu;
            
            InvalidSelectMenuComponentException.ThrowIfInvalidSelectMenuOptionCount(text.Options.Count);

            text.Options.Add(new SelectMenuOption
            {
                Label = label,
                Value = value,
                Description = description,
                Default = @default,
                Emoji = emoji
            });
            return this;
        }
        
        /// <summary>
        /// Adds an allow channel type for <see cref="MessageComponentType.ChannelSelect"/>
        /// </summary>
        /// <param name="type">Channel Type to add</param>
        /// <returns>This</returns>
        public SelectMenuComponentBuilder AddChannelType(ChannelType type)
        {
            InvalidSelectMenuComponentException.ThrowIfTypeCantAddChannelTypes(_menu.Type);

            ChannelSelectComponent text = (ChannelSelectComponent)_menu;
            text.ChannelTypes.Add(type);
            return this;
        }

        /// <summary>
        /// Returns the root builder
        /// </summary>
        /// <returns><see cref="MessageComponentBuilder"/></returns>
        public MessageComponentBuilder Build()
        {
            return _builder;
        }
    }
}