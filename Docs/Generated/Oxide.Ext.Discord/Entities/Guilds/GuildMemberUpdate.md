# GuildMemberUpdate class

Represents [Guild Member Update Structure](https://discord.com/developers/docs/resources/guild#modify-guild-member-json-params)

```csharp
public class GuildMemberUpdate
```

## Public Members

| name | description |
| --- | --- |
| [GuildMemberUpdate](GuildMemberUpdate/GuildMemberUpdate.md)() | The default constructor. |
| [ChannelId](GuildMemberUpdate/ChannelId.md) { get; set; } | The channel to move the user to Requires MOVE_MEMBERS Permission Setting to null will remove that member from a voice channel |
| [CommunicationDisabledUntil](GuildMemberUpdate/CommunicationDisabledUntil.md) { get; set; } | When the user's timeout will expire and the user will be able to communicate in the guild again (up to 28 days in the future), set to null to remove timeout |
| [Deaf](GuildMemberUpdate/Deaf.md) { get; set; } | Deafen the guild member Requires DEAFEN_MEMBERS Permission |
| [Mute](GuildMemberUpdate/Mute.md) { get; set; } | Mute the guild member Requires MUTE_MEMBERS Permission |
| [Nick](GuildMemberUpdate/Nick.md) { get; set; } | The nickname to give the user Requires MANAGE_NICKNAMES Permission |
| [Roles](GuildMemberUpdate/Roles.md) { get; set; } | New list of guild members roles Will replaces all roles with the ones in this list Requires MANAGE_ROLES Permission |
| [Validate](GuildMemberUpdate/Validate.md)() |  |

## See Also

* namespace [Oxide.Ext.Discord.Entities.Guilds](./GuildsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
* [GuildMemberUpdate.cs](https://github.com/dassjosh/Oxide.Ext.Discord/blob/develop/Oxide.Ext.Discord/Entities/Guilds/GuildMemberUpdate.cs)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->