# DiscordMessage.StartThread method

Creates a new public thread this message See [https://discord.com/developers/docs/resources/channel#start-thread-from-message](https://discord.com/developers/docs/resources/channel#start-thread-from-message)

```csharp
public IPromise<DiscordChannel> StartThread(DiscordClient client, ThreadCreateFromMessage create)
```

| parameter | description |
| --- | --- |
| client | Client to use |
| create | Data to use when creating the thread |

## See Also

* interface [IPromise&lt;TPromised&gt;](../../../Interfaces/Promises/IPromise-1.md)
* class [DiscordChannel](../../Channels/DiscordChannel.md)
* class [DiscordClient](../../../Clients/DiscordClient.md)
* class [ThreadCreateFromMessage](../../Channels/Threads/ThreadCreateFromMessage.md)
* class [DiscordMessage](../DiscordMessage.md)
* namespace [Oxide.Ext.Discord.Entities.Messages](../DiscordMessage.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->