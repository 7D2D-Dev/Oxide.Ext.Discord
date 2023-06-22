# DiscordModalSubmitAttribute class

Discord Message Component Command Attribute for ModalSubmit Callback Hook Format:

```csharp
[DiscordModalSubmitAttribute("CustomId")]
private void ModalSubmitCommand(DiscordInteraction interaction)
{
    Puts("ModalSubmitCommand Works!");
}
```

```csharp
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public sealed class DiscordModalSubmitAttribute : BaseApplicationCommandAttribute
```

## Public Members

| name | description |
| --- | --- |
| [DiscordModalSubmitAttribute](DiscordModalSubmitAttribute/DiscordModalSubmitAttribute.md)(…) | Constructor |

## See Also

* class [BaseApplicationCommandAttribute](./BaseApplicationCommandAttribute.md)
* namespace [Oxide.Ext.Discord.Attributes.ApplicationCommands](./ApplicationCommandsNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
* [DiscordModalSubmitAttribute.cs](https://github.com/dassjosh/Oxide.Ext.Discord/blob/develop/Oxide.Ext.Discord/Attributes/ApplicationCommands/DiscordModalSubmitAttribute.cs)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->