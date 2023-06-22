# DiscordJsonReader class

This is a pooled JSON reader that can read as string, deserialize object, or populate a given object async

```csharp
public class DiscordJsonReader : BasePoolable
```

## Public Members

| name | description |
| --- | --- |
| [DiscordJsonReader](DiscordJsonReader/DiscordJsonReader.md)() | Constructor |
| static [Create](DiscordJsonReader/Create.md)(…) | Returns a pooled [`DiscordJsonReader`](./DiscordJsonReader.md) |
| static [CreateFromStream](DiscordJsonReader/CreateFromStream.md)(…) | Returns a pooled [`DiscordJsonReader`](./DiscordJsonReader.md) with stream loaded into it |
| [CopyFrom](DiscordJsonReader/CopyFrom.md)(…) | Copy from the given stream to our internal stream |
| [Deserialize&lt;T&gt;](DiscordJsonReader/Deserialize.md)(…) | Deserializes the stream data to {T} |
| [ReadAsString](DiscordJsonReader/ReadAsString.md)() | Returns the Stream data as a string |
| static [DeserializeFrom&lt;T&gt;](DiscordJsonReader/DeserializeFrom.md)(…) | Deserialize from stream to type {T} |

## Protected Members

| name | description |
| --- | --- |
| override [EnterPool](DiscordJsonReader/EnterPool.md)() |  |

## See Also

* class [BasePoolable](../../Pooling/BasePoolable.md)
* namespace [Oxide.Ext.Discord.Json.Serialization](./SerializationNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
* [DiscordJsonReader.cs](https://github.com/dassjosh/Oxide.Ext.Discord/blob/develop/Oxide.Ext.Discord/Json/Serialization/DiscordJsonReader.cs)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->