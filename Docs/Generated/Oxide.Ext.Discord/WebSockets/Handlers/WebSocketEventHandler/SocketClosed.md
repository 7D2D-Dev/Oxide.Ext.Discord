# WebSocketEventHandler.SocketClosed method

Called when a socket is closed

```csharp
public Task SocketClosed(Snowflake websocketId, WebSocketCloseStatus status, string message)
```

| parameter | description |
| --- | --- |
| websocketId | ID of the web socket |
| status | WebSocketCloseStatus for the web socket |
| message | Close message from the web socket |

## See Also

* struct [Snowflake](../../../Entities/Snowflake.md)
* class [WebSocketEventHandler](../WebSocketEventHandler.md)
* namespace [Oxide.Ext.Discord.WebSockets.Handlers](../WebSocketEventHandler.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->