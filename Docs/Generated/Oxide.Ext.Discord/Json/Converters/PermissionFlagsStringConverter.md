# PermissionFlagsStringConverter class

Converts Permission Flags to and from a JSON string

```csharp
public class PermissionFlagsStringConverter : JsonConverter
```

## Public Members

| name | description |
| --- | --- |
| [PermissionFlagsStringConverter](PermissionFlagsStringConverter/PermissionFlagsStringConverter.md)() | The default constructor. |
| override [CanConvert](PermissionFlagsStringConverter/CanConvert.md)(…) | Returns if the type equals PermissionFlags |
| override [ReadJson](PermissionFlagsStringConverter/ReadJson.md)(…) | Converts the ulong JSON string to Permission Flags |
| override [WriteJson](PermissionFlagsStringConverter/WriteJson.md)(…) | Writes Permission Flags as a JSON string |

## See Also

* namespace [Oxide.Ext.Discord.Json.Converters](./ConvertersNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
* [PermissionFlagsStringConverter.cs](https://github.com/dassjosh/Oxide.Ext.Discord/blob/develop/Oxide.Ext.Discord/Json/Converters/PermissionFlagsStringConverter.cs)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->