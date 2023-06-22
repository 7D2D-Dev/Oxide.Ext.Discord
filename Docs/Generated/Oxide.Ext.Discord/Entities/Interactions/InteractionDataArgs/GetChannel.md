# InteractionDataArgs.GetChannel method

Returns the [`DiscordChannel`](../../Channels/DiscordChannel.md) that was resolved from the command.

```csharp
public DiscordChannel GetChannel(string name)
```

| parameter | description |
| --- | --- |
| name | Name of the command option |

## Return Value

[`DiscordChannel`](../../Channels/DiscordChannel.md) resolved for the matching command option name

## Exceptions

| exception | condition |
| --- | --- |
| Exception | Thrown if the option type is not a [`DiscordChannel`](../../Channels/DiscordChannel.md) |

## See Also

* class [DiscordChannel](../../Channels/DiscordChannel.md)
* class [InteractionDataArgs](../InteractionDataArgs.md)
* namespace [Oxide.Ext.Discord.Entities.Interactions](../InteractionDataArgs.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->