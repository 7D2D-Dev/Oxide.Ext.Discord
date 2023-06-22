# DiscordPluginPool.Get&lt;T&gt; method

Returns a pooled object of {T} type Must inherit from [`BasePoolable`](../BasePoolable.md) and have an empty default constructor

```csharp
public T Get<T>()
    where T : BasePoolable, new()
```

| parameter | description |
| --- | --- |
| T | Type to be returned |

## Return Value

Pooled object of type T

## See Also

* class [BasePoolable](../BasePoolable.md)
* class [DiscordPluginPool](../DiscordPluginPool.md)
* namespace [Oxide.Ext.Discord.Pooling](../DiscordPluginPool.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->