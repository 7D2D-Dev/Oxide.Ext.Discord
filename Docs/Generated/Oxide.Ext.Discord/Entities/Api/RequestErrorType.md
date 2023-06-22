# RequestErrorType enumeration

Represents a Discord Request Error Type

```csharp
public enum RequestErrorType : byte
```

## Values

| name | value | description |
| --- | --- | --- |
| GenericWeb | `GenericWeb` | A generic web error occured |
| Internal | `Internal` | An Internal HTTP Error Occured |
| RateLimit | `RateLimit` | A Ratelimit Error Occured |
| ApiError | `ApiError` | An Invalid request was passed to discord |
| Serialization | `Serialization` | An error occured during JSON serialization |
| Generic | `Generic` | A non web error occured |

## See Also

* namespace [Oxide.Ext.Discord.Entities.Api](./ApiNamespace.md)
* assembly [Oxide.Ext.Discord](../../../Oxide.Ext.Discord.md)
* [RequestErrorType.cs](https://github.com/dassjosh/Oxide.Ext.Discord/blob/develop/Oxide.Ext.Discord/Entities/Api/RequestErrorType.cs)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->