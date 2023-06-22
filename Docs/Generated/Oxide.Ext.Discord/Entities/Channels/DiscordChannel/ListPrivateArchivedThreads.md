# DiscordChannel.ListPrivateArchivedThreads method

Returns archived threads in the channel that are of type GUILD_PRIVATE_THREAD. Threads are ordered by archive_timestamp, in descending order. Requires both the READ_MESSAGE_HISTORY and MANAGE_THREADS permissions. See [List Private Archived Threads](https://discord.com/developers/docs/resources/channel#list-private-archived-threads)

```csharp
public IPromise<ThreadList> ListPrivateArchivedThreads(DiscordClient client, 
    ThreadArchivedLookup request = null)
```

| parameter | description |
| --- | --- |
| client | Client to use |
| request | The options to use when looking up the archived threads |

## See Also

* interface [IPromise&lt;TPromised&gt;](../../../Interfaces/Promises/IPromise-1.md)
* class [ThreadList](../Threads/ThreadList.md)
* class [DiscordClient](../../../Clients/DiscordClient.md)
* class [ThreadArchivedLookup](../Threads/ThreadArchivedLookup.md)
* class [DiscordChannel](../DiscordChannel.md)
* namespace [Oxide.Ext.Discord.Entities.Channels](../DiscordChannel.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->