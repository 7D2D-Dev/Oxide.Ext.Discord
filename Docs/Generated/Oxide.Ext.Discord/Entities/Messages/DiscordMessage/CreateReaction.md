# DiscordMessage.CreateReaction method (1 of 2)

Create a reaction for the message. This endpoint requires the 'READ_MESSAGE_HISTORY' permission to be present on the current user. Additionally, if nobody else has reacted to the message using this emoji, this endpoint requires the 'ADD_REACTIONS' permission to be present on the current user. Valid emoji formats are the unicode emoji character '😀' or for custom emoji format must be &lt;emojiName:emojiId&gt; See [Create Reaction](https://discord.com/developers/docs/resources/channel#create-reaction)

```csharp
public IPromise CreateReaction(DiscordClient client, DiscordEmoji emoji)
```

| parameter | description |
| --- | --- |
| client | Client to use |
| emoji | Emoji to react with. |

## See Also

* interface [IPromise](../../../Interfaces/Promises/IPromise.md)
* class [DiscordClient](../../../Clients/DiscordClient.md)
* class [DiscordEmoji](../../Emojis/DiscordEmoji.md)
* class [DiscordMessage](../DiscordMessage.md)
* namespace [Oxide.Ext.Discord.Entities.Messages](../DiscordMessage.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)

---

# DiscordMessage.CreateReaction method (2 of 2)

Create a reaction for the message. This endpoint requires the 'READ_MESSAGE_HISTORY' permission to be present on the current user. Additionally, if nobody else has reacted to the message using this emoji, this endpoint requires the 'ADD_REACTIONS' permission to be present on the current user. Valid emoji formats are the unicode emoji character '😀' or for custom emoji format must be &lt;emojiName:emojiId&gt; See [Create Reaction](https://discord.com/developers/docs/resources/channel#create-reaction)

```csharp
public IPromise CreateReaction(DiscordClient client, string emoji)
```

| parameter | description |
| --- | --- |
| client | Client to use |
| emoji | Emoji to react with. |

## See Also

* interface [IPromise](../../../Interfaces/Promises/IPromise.md)
* class [DiscordClient](../../../Clients/DiscordClient.md)
* class [DiscordMessage](../DiscordMessage.md)
* namespace [Oxide.Ext.Discord.Entities.Messages](../DiscordMessage.md)
* assembly [Oxide.Ext.Discord](../../../../Oxide.Ext.Discord.md)

<!-- DO NOT EDIT: generated by xmldocmd for Oxide.Ext.Discord.dll -->