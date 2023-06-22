# DiscordGuild.EditChannelPositions method

Modify the positions of a set of channel objects for the guild. Requires MANAGE_CHANNELS permission. Only channels to be modified are required, with the minimum being a swap between at least two channels. See [Modify Guild Channel Positions](https://discord.com/developers/docs/resources/guild#modify-guild-channel-positions)

```csharp
public IPromise<List<GuildChannelPosition>> EditChannelPositions(DiscordClient client, 
    List<GuildChannelPosition> positions)
```

| parameter | description |
| --- | --- |
| client | Client to use |
| positions | List new channel positions for each channel |

## See Also

* interface [IPromise&lt;TPromised&gt;](../../../Interfaces/Promises/IPromise-1.md)
* class [GuildChannelPosition](../GuildChannelPosition.md)
* class [DiscordClient](../../../Clients/DiscordClient.md)
* class [DiscordGuild](../DiscordGuild.md)
* namespace [Oxide.Ext.Discord.Entities.Guilds](../DiscordGuild.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->