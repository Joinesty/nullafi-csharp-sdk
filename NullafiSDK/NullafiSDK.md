<a name='assembly'></a>
# NullafiSDK

## Contents

- [Client](#T-Nullafi-Client 'Nullafi.Client')
  - [Authenticate(apiKey)](#M-Nullafi-Client-Authenticate-System-String- 'Nullafi.Client.Authenticate(System.String)')
  - [CreateCommunicationVault(name,tags)](#M-Nullafi-Client-CreateCommunicationVault-System-String,System-Collections-Generic-List{System-String}- 'Nullafi.Client.CreateCommunicationVault(System.String,System.Collections.Generic.List{System.String})')
  - [CreateStaticVault(name,tags)](#M-Nullafi-Client-CreateStaticVault-System-String,System-Collections-Generic-List{System-String}- 'Nullafi.Client.CreateStaticVault(System.String,System.Collections.Generic.List{System.String})')
  - [RetrieveCommunicationVault(vaultId,masterKey)](#M-Nullafi-Client-RetrieveCommunicationVault-System-String,System-String- 'Nullafi.Client.RetrieveCommunicationVault(System.String,System.String)')
  - [RetrieveStaticVault(vaultId,masterKey)](#M-Nullafi-Client-RetrieveStaticVault-System-String,System-String- 'Nullafi.Client.RetrieveStaticVault(System.String,System.String)')

<a name='T-Nullafi-Client'></a>
## Client `type`

##### Namespace

Nullafi

##### Summary

Client class

<a name='M-Nullafi-Client-Authenticate-System-String-'></a>
### Authenticate(apiKey) `method`

##### Summary

Authenticate the client API

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| apiKey | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-Nullafi-Client-CreateCommunicationVault-System-String,System-Collections-Generic-List{System-String}-'></a>
### CreateCommunicationVault(name,tags) `method`

##### Summary

Create a new communication vault

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| tags | [System.Collections.Generic.List{System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{System.String}') |  |

<a name='M-Nullafi-Client-CreateStaticVault-System-String,System-Collections-Generic-List{System-String}-'></a>
### CreateStaticVault(name,tags) `method`

##### Summary

Create a new static vault

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| tags | [System.Collections.Generic.List{System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{System.String}') |  |

<a name='M-Nullafi-Client-RetrieveCommunicationVault-System-String,System-String-'></a>
### RetrieveCommunicationVault(vaultId,masterKey) `method`

##### Summary

retrieve an existing communication vault

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| vaultId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| masterKey | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-Nullafi-Client-RetrieveStaticVault-System-String,System-String-'></a>
### RetrieveStaticVault(vaultId,masterKey) `method`

##### Summary

retrieve an existing static vault

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| vaultId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| masterKey | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
