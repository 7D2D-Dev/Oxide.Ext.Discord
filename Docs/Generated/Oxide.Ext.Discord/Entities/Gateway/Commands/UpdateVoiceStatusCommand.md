# UpdateVoiceStatusCommand class

Represents [Update Voice State](https://discord.com/developers/docs/topics/gateway#update-voice-state)

```csharp
public class UpdateVoiceStatusCommand
```

## Public Members

| name | description |
| --- | --- |
| [UpdateVoiceStatusCommand](UpdateVoiceStatusCommand/UpdateVoiceStatusCommand.md)() | The default constructor. |
| [ChannelId](UpdateVoiceStatusCommand/ChannelId.md) { get; set; } | ID of the voice channel client wants to join (null if disconnecting) |
| [GuildId](UpdateVoiceStatusCommand/GuildId.md) { get; set; } | ID of the guild |
| [SelfDeaf](UpdateVoiceStatusCommand/SelfDeaf.md) { get; set; } | Is the client deafened |
| [SelfMute](UpdateVoiceStatusCommand/SelfMute.md) { get; set; } | Is the client muted |

## See Also

* namespace [Oxide.Ext.Discord.Entities.Gateway.Commands](./CommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)
* [UpdateVoiceStatusCommand.cs](https://github.com/dassjosh/Oxide.Ext.Discord/blob/develop/Oxide.Ext.Discord/Entities/Gateway/Commands/UpdateVoiceStatusCommand.cs)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->