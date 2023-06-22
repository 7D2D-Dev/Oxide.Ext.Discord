# DiscordPool.GetOrCreate method

Returns an existing [`DiscordPluginPool`](../../../Pooling/DiscordPluginPool.md) for the given plugin or returns a new one

```csharp
public DiscordPluginPool GetOrCreate(Plugin plugin)
```

| parameter | description |
| --- | --- |
| plugin | The pool the plugin is for |

## Exceptions

| exception | condition |
| --- | --- |
| ArgumentNullException | Thrown if the plugin is null |

## See Also

* class [DiscordPluginPool](../../../Pooling/DiscordPluginPool.md)
* class [DiscordPool](../DiscordPool.md)
* namespace [Oxide.Ext.Discord.Libraries.Pooling](../DiscordPool.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->