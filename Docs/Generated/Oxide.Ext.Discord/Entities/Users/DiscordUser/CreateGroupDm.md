# DiscordUser.CreateGroupDm method

Create a Group Direct Message

```csharp
public IPromise<DiscordChannel> CreateGroupDm(DiscordClient client, string[] accessTokens, 
    Hash<Snowflake, string> nicks)
```

| parameter | description |
| --- | --- |
| client | Client to use |
| accessTokens | access tokens of users that have granted your app the gdm.join scope |
| nicks | a list of user ids to their respective nicknames |

## See Also

* interface [IPromise&lt;TPromised&gt;](../../../Interfaces/Promises/IPromise-1.md)
* class [DiscordChannel](../../Channels/DiscordChannel.md)
* class [DiscordClient](../../../Clients/DiscordClient.md)
* struct [Snowflake](../../Snowflake.md)
* class [DiscordUser](../DiscordUser.md)
* namespace [Oxide.Ext.Discord.Entities.Users](../DiscordUser.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->