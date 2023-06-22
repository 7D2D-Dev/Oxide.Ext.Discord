# DiscordInvite.Get method

Returns an invite object for the given code. See [Get Invite](https://discord.com/developers/docs/resources/invite#get-invite)

```csharp
public static IPromise<DiscordInvite> Get(DiscordClient client, string inviteCode, 
    InviteLookup lookup = null)
```

| parameter | description |
| --- | --- |
| client | Client to use |
| inviteCode | Invite code |
| lookup | Lookup query string parameters for the request |

## See Also

* interface [IPromise&lt;TPromised&gt;](../../../Interfaces/Promises/IPromise-1.md)
* class [DiscordClient](../../../Clients/DiscordClient.md)
* class [InviteLookup](../InviteLookup.md)
* class [DiscordInvite](../DiscordInvite.md)
* namespace [Oxide.Ext.Discord.Entities.Invites](../DiscordInvite.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->