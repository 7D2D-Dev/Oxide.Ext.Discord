# ChannelMention class

Represents a [Channel Mention Structure](https://discord.com/developers/docs/resources/channel#channel-mention-object-channel-mention-structure) in a message

```csharp
public class ChannelMention : ISnowflakeEntity
```

## Public Members

| name | description |
| --- | --- |
| [ChannelMention](ChannelMention/ChannelMention.md)() | The default constructor. |
| [GuildId](ChannelMention/GuildId.md) { get; set; } | ID of the guild containing the channel |
| [Id](ChannelMention/Id.md) { get; set; } | ID of the channel |
| [Name](ChannelMention/Name.md) { get; set; } | The name of the channel |
| [Type](ChannelMention/Type.md) { get; set; } | The type of channel [`ChannelType`](./ChannelType.md) |

## See Also

* interface [ISnowflakeEntity](../../Interfaces/ISnowflakeEntity.md)
* namespace [Oxide.Ext.Discord.Entities.Channels](./ChannelsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
* [ChannelMention.cs](https://github.com/dassjosh/Oxide.Ext.Discord/blob/develop/Oxide.Ext.Discord/Entities/Channels/ChannelMention.cs)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->