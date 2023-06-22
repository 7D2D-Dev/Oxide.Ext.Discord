# WebsocketRateLimit class

Represents a WebSocket Rate Limit

```csharp
public class WebsocketRateLimit : BaseRateLimit
```

## Public Members

| name | description |
| --- | --- |
| [WebsocketRateLimit](WebsocketRateLimit/WebsocketRateLimit.md)(…) | Constructor for WebsocketRateLimit |
| [CanFireRequest](WebsocketRateLimit/CanFireRequest.md)(…) | Returns if the client can return the given command. This is used to limit plugins to a certain number of websocket commands per rate limit tick |
| [FiredRequest](WebsocketRateLimit/FiredRequest.md)(…) | Called when an API request is fired |

## Protected Members

| name | description |
| --- | --- |
| override [OnRateLimitReset](WebsocketRateLimit/OnRateLimitReset.md)() |  |

## See Also

* class [BaseRateLimit](./BaseRateLimit.md)
* namespace [Oxide.Ext.Discord.RateLimits](./RateLimitsNamespace.md)
* assembly [Oxide.Ext.Discord](../../Oxide.Ext.Discord.md)
* [WebsocketRateLimit.cs](https://github.com/dassjosh/Oxide.Ext.Discord/blob/develop/Oxide.Ext.Discord/RateLimits/WebsocketRateLimit.cs)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->