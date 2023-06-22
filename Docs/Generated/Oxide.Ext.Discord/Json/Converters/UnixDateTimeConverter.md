# UnixDateTimeConverter class

Converts a DateTimeOffset to and from a json long

```csharp
public class UnixDateTimeConverter : JsonConverter
```

## Public Members

| name | description |
| --- | --- |
| [UnixDateTimeConverter](UnixDateTimeConverter/UnixDateTimeConverter.md)() | The default constructor. |
| override [CanConvert](UnixDateTimeConverter/CanConvert.md)(…) | Can the type be converted |
| override [ReadJson](UnixDateTimeConverter/ReadJson.md)(…) | Convert to DateTimeOffset from UnixTimeMilliseconds |
| override [WriteJson](UnixDateTimeConverter/WriteJson.md)(…) | Write DateTimeOffset to UnixTimeMilliseconds |

## See Also

* namespace [Oxide.Ext.Discord.Json.Converters](./ConvertersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
* [UnixDateTimeConverter.cs](https://github.com/dassjosh/Oxide.Ext.Discord/blob/develop/Oxide.Ext.Discord/Json/Converters/UnixDateTimeConverter.cs)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->