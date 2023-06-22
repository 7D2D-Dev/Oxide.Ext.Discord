# GuildScheduledEvent.Delete method

Delete a guild scheduled event See [Delete Guild Scheduled Event](https://discord.com/developers/docs/resources/guild-scheduled-event#delete-guild-scheduled-event)

```csharp
public IPromise Delete(DiscordClient client, Snowflake guildId, Snowflake eventId)
```

| parameter | description |
| --- | --- |
| client | Client to use |
| guildId | Guild to modify event in |
| eventId | Id of the event to delete |

## See Also

* interface [IPromise](../../../../Interfaces/Promises/IPromise.md)
* class [DiscordClient](../../../../Clients/DiscordClient.md)
* struct [Snowflake](../../../Snowflake.md)
* class [GuildScheduledEvent](../GuildScheduledEvent.md)
* namespace [Oxide.Ext.Discord.Entities.Guilds.ScheduledEvents](../GuildScheduledEvent.md)
* assembly [Oxide.Ext.Discord](../../../../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->