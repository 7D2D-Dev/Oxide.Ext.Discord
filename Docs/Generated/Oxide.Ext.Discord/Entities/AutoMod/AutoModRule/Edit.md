# AutoModRule.Edit method

Modify an existing rule Requires ManageGuild permissions. See [Modify Auto Moderation Rule](https://discord.com/developers/docs/resources/auto-moderation#modify-auto-moderation-rule)

```csharp
public IPromise<AutoModRule> Edit(DiscordClient client, AutoModRuleModify modify)
```

| parameter | description |
| --- | --- |
| client | Client to use |
| modify | [`AutoModRuleModify`](../AutoModRuleModify.md) |

## See Also

* interface [IPromise&lt;TPromised&gt;](../../../Interfaces/Promises/IPromise-1.md)
* class [DiscordClient](../../../Clients/DiscordClient.md)
* class [AutoModRuleModify](../AutoModRuleModify.md)
* class [AutoModRule](../AutoModRule.md)
* namespace [Oxide.Ext.Discord.Entities.AutoMod](../AutoModRule.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->