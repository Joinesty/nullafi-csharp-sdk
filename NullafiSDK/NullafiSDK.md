<a name='assembly'></a>
# NullafiSDK

## Contents

- [AddressManager](#T-Nullafi-Domains-StaticVault-Managers-Address-AddressManager 'Nullafi.Domains.StaticVault.Managers.Address.AddressManager')
  - [#ctor(vault)](#M-Nullafi-Domains-StaticVault-Managers-Address-AddressManager-#ctor-Nullafi-Domains-StaticVault-StaticVault- 'Nullafi.Domains.StaticVault.Managers.Address.AddressManager.#ctor(Nullafi.Domains.StaticVault.StaticVault)')
  - [Create(address,tags)](#M-Nullafi-Domains-StaticVault-Managers-Address-AddressManager-Create-System-String,System-Collections-Generic-List{System-String}- 'Nullafi.Domains.StaticVault.Managers.Address.AddressManager.Create(System.String,System.Collections.Generic.List{System.String})')
  - [Create(address,state,tags)](#M-Nullafi-Domains-StaticVault-Managers-Address-AddressManager-Create-System-String,System-String,System-Collections-Generic-List{System-String}- 'Nullafi.Domains.StaticVault.Managers.Address.AddressManager.Create(System.String,System.String,System.Collections.Generic.List{System.String})')
  - [Delete(aliasId)](#M-Nullafi-Domains-StaticVault-Managers-Address-AddressManager-Delete-System-String- 'Nullafi.Domains.StaticVault.Managers.Address.AddressManager.Delete(System.String)')
  - [Retrieve(aliasId)](#M-Nullafi-Domains-StaticVault-Managers-Address-AddressManager-Retrieve-System-String- 'Nullafi.Domains.StaticVault.Managers.Address.AddressManager.Retrieve(System.String)')
- [AddressRequest](#T-Nullafi-Domains-StaticVault-Managers-Address-AddressRequest 'Nullafi.Domains.StaticVault.Managers.Address.AddressRequest')
  - [Address](#P-Nullafi-Domains-StaticVault-Managers-Address-AddressRequest-Address 'Nullafi.Domains.StaticVault.Managers.Address.AddressRequest.Address')
  - [AddressHash](#P-Nullafi-Domains-StaticVault-Managers-Address-AddressRequest-AddressHash 'Nullafi.Domains.StaticVault.Managers.Address.AddressRequest.AddressHash')
  - [AuthTag](#P-Nullafi-Domains-StaticVault-Managers-Address-AddressRequest-AuthTag 'Nullafi.Domains.StaticVault.Managers.Address.AddressRequest.AuthTag')
  - [Iv](#P-Nullafi-Domains-StaticVault-Managers-Address-AddressRequest-Iv 'Nullafi.Domains.StaticVault.Managers.Address.AddressRequest.Iv')
  - [Tags](#P-Nullafi-Domains-StaticVault-Managers-Address-AddressRequest-Tags 'Nullafi.Domains.StaticVault.Managers.Address.AddressRequest.Tags')
- [AddressResponse](#T-Nullafi-Domains-StaticVault-Managers-Address-AddressResponse 'Nullafi.Domains.StaticVault.Managers.Address.AddressResponse')
  - [Address](#P-Nullafi-Domains-StaticVault-Managers-Address-AddressResponse-Address 'Nullafi.Domains.StaticVault.Managers.Address.AddressResponse.Address')
  - [AddressAlias](#P-Nullafi-Domains-StaticVault-Managers-Address-AddressResponse-AddressAlias 'Nullafi.Domains.StaticVault.Managers.Address.AddressResponse.AddressAlias')
  - [AuthTag](#P-Nullafi-Domains-StaticVault-Managers-Address-AddressResponse-AuthTag 'Nullafi.Domains.StaticVault.Managers.Address.AddressResponse.AuthTag')
  - [CreatedAt](#P-Nullafi-Domains-StaticVault-Managers-Address-AddressResponse-CreatedAt 'Nullafi.Domains.StaticVault.Managers.Address.AddressResponse.CreatedAt')
  - [Id](#P-Nullafi-Domains-StaticVault-Managers-Address-AddressResponse-Id 'Nullafi.Domains.StaticVault.Managers.Address.AddressResponse.Id')
  - [Iv](#P-Nullafi-Domains-StaticVault-Managers-Address-AddressResponse-Iv 'Nullafi.Domains.StaticVault.Managers.Address.AddressResponse.Iv')
  - [Tags](#P-Nullafi-Domains-StaticVault-Managers-Address-AddressResponse-Tags 'Nullafi.Domains.StaticVault.Managers.Address.AddressResponse.Tags')
  - [UpdatedAt](#P-Nullafi-Domains-StaticVault-Managers-Address-AddressResponse-UpdatedAt 'Nullafi.Domains.StaticVault.Managers.Address.AddressResponse.UpdatedAt')
- [Client](#T-Nullafi-Client 'Nullafi.Client')
  - [Authenticate(apiKey)](#M-Nullafi-Client-Authenticate-System-String- 'Nullafi.Client.Authenticate(System.String)')
  - [CreateCommunicationVault(name,tags)](#M-Nullafi-Client-CreateCommunicationVault-System-String,System-Collections-Generic-List{System-String}- 'Nullafi.Client.CreateCommunicationVault(System.String,System.Collections.Generic.List{System.String})')
  - [CreateStaticVault(name,tags)](#M-Nullafi-Client-CreateStaticVault-System-String,System-Collections-Generic-List{System-String}- 'Nullafi.Client.CreateStaticVault(System.String,System.Collections.Generic.List{System.String})')
  - [RetrieveCommunicationVault(vaultId,masterKey)](#M-Nullafi-Client-RetrieveCommunicationVault-System-String,System-String- 'Nullafi.Client.RetrieveCommunicationVault(System.String,System.String)')
  - [RetrieveStaticVault(vaultId,masterKey)](#M-Nullafi-Client-RetrieveStaticVault-System-String,System-String- 'Nullafi.Client.RetrieveStaticVault(System.String,System.String)')
- [CommunicationVault](#T-Nullafi-Domains-CommunicationVault-CommunicationVault 'Nullafi.Domains.CommunicationVault.CommunicationVault')
  - [Email](#P-Nullafi-Domains-CommunicationVault-CommunicationVault-Email 'Nullafi.Domains.CommunicationVault.CommunicationVault.Email')
  - [MasterKey](#P-Nullafi-Domains-CommunicationVault-CommunicationVault-MasterKey 'Nullafi.Domains.CommunicationVault.CommunicationVault.MasterKey')
  - [VaultId](#P-Nullafi-Domains-CommunicationVault-CommunicationVault-VaultId 'Nullafi.Domains.CommunicationVault.CommunicationVault.VaultId')
  - [VaultName](#P-Nullafi-Domains-CommunicationVault-CommunicationVault-VaultName 'Nullafi.Domains.CommunicationVault.CommunicationVault.VaultName')
  - [CreateCommunicationVault(client,name,tags)](#M-Nullafi-Domains-CommunicationVault-CommunicationVault-CreateCommunicationVault-Nullafi-Client,System-String,System-Collections-Generic-List{System-String}- 'Nullafi.Domains.CommunicationVault.CommunicationVault.CreateCommunicationVault(Nullafi.Client,System.String,System.Collections.Generic.List{System.String})')
  - [RetrieveCommunicationVault(client,vaultId,masterKey)](#M-Nullafi-Domains-CommunicationVault-CommunicationVault-RetrieveCommunicationVault-Nullafi-Client,System-String,System-String- 'Nullafi.Domains.CommunicationVault.CommunicationVault.RetrieveCommunicationVault(Nullafi.Client,System.String,System.String)')
- [DateOfBirthManager](#T-Nullafi-Domains-StaticVault-Managers-DateOfBirth-DateOfBirthManager 'Nullafi.Domains.StaticVault.Managers.DateOfBirth.DateOfBirthManager')
  - [#ctor(vault)](#M-Nullafi-Domains-StaticVault-Managers-DateOfBirth-DateOfBirthManager-#ctor-Nullafi-Domains-StaticVault-StaticVault- 'Nullafi.Domains.StaticVault.Managers.DateOfBirth.DateOfBirthManager.#ctor(Nullafi.Domains.StaticVault.StaticVault)')
  - [Create(dateOfBirth,tags)](#M-Nullafi-Domains-StaticVault-Managers-DateOfBirth-DateOfBirthManager-Create-System-String,System-Collections-Generic-List{System-String}- 'Nullafi.Domains.StaticVault.Managers.DateOfBirth.DateOfBirthManager.Create(System.String,System.Collections.Generic.List{System.String})')
  - [Create(dateOfBirth,year,month,tags)](#M-Nullafi-Domains-StaticVault-Managers-DateOfBirth-DateOfBirthManager-Create-System-String,System-Nullable{System-Int32},System-Nullable{System-Int32},System-Collections-Generic-List{System-String}- 'Nullafi.Domains.StaticVault.Managers.DateOfBirth.DateOfBirthManager.Create(System.String,System.Nullable{System.Int32},System.Nullable{System.Int32},System.Collections.Generic.List{System.String})')
  - [Delete(aliasId)](#M-Nullafi-Domains-StaticVault-Managers-DateOfBirth-DateOfBirthManager-Delete-System-String- 'Nullafi.Domains.StaticVault.Managers.DateOfBirth.DateOfBirthManager.Delete(System.String)')
  - [Retrieve(aliasId)](#M-Nullafi-Domains-StaticVault-Managers-DateOfBirth-DateOfBirthManager-Retrieve-System-String- 'Nullafi.Domains.StaticVault.Managers.DateOfBirth.DateOfBirthManager.Retrieve(System.String)')
- [DateOfBirthRequest](#T-Nullafi-Domains-StaticVault-Managers-DateOfBirth-DateOfBirthRequest 'Nullafi.Domains.StaticVault.Managers.DateOfBirth.DateOfBirthRequest')
  - [AuthTag](#P-Nullafi-Domains-StaticVault-Managers-DateOfBirth-DateOfBirthRequest-AuthTag 'Nullafi.Domains.StaticVault.Managers.DateOfBirth.DateOfBirthRequest.AuthTag')
  - [DateOfBirth](#P-Nullafi-Domains-StaticVault-Managers-DateOfBirth-DateOfBirthRequest-DateOfBirth 'Nullafi.Domains.StaticVault.Managers.DateOfBirth.DateOfBirthRequest.DateOfBirth')
  - [DateOfBirthHash](#P-Nullafi-Domains-StaticVault-Managers-DateOfBirth-DateOfBirthRequest-DateOfBirthHash 'Nullafi.Domains.StaticVault.Managers.DateOfBirth.DateOfBirthRequest.DateOfBirthHash')
  - [Iv](#P-Nullafi-Domains-StaticVault-Managers-DateOfBirth-DateOfBirthRequest-Iv 'Nullafi.Domains.StaticVault.Managers.DateOfBirth.DateOfBirthRequest.Iv')
  - [Tags](#P-Nullafi-Domains-StaticVault-Managers-DateOfBirth-DateOfBirthRequest-Tags 'Nullafi.Domains.StaticVault.Managers.DateOfBirth.DateOfBirthRequest.Tags')
- [DateOfBirthResponse](#T-Nullafi-Domains-StaticVault-Managers-DateOfBirth-DateOfBirthResponse 'Nullafi.Domains.StaticVault.Managers.DateOfBirth.DateOfBirthResponse')
  - [AuthTag](#P-Nullafi-Domains-StaticVault-Managers-DateOfBirth-DateOfBirthResponse-AuthTag 'Nullafi.Domains.StaticVault.Managers.DateOfBirth.DateOfBirthResponse.AuthTag')
  - [CreatedAt](#P-Nullafi-Domains-StaticVault-Managers-DateOfBirth-DateOfBirthResponse-CreatedAt 'Nullafi.Domains.StaticVault.Managers.DateOfBirth.DateOfBirthResponse.CreatedAt')
  - [DateOfBirth](#P-Nullafi-Domains-StaticVault-Managers-DateOfBirth-DateOfBirthResponse-DateOfBirth 'Nullafi.Domains.StaticVault.Managers.DateOfBirth.DateOfBirthResponse.DateOfBirth')
  - [DateOfBirthAlias](#P-Nullafi-Domains-StaticVault-Managers-DateOfBirth-DateOfBirthResponse-DateOfBirthAlias 'Nullafi.Domains.StaticVault.Managers.DateOfBirth.DateOfBirthResponse.DateOfBirthAlias')
  - [Id](#P-Nullafi-Domains-StaticVault-Managers-DateOfBirth-DateOfBirthResponse-Id 'Nullafi.Domains.StaticVault.Managers.DateOfBirth.DateOfBirthResponse.Id')
  - [Iv](#P-Nullafi-Domains-StaticVault-Managers-DateOfBirth-DateOfBirthResponse-Iv 'Nullafi.Domains.StaticVault.Managers.DateOfBirth.DateOfBirthResponse.Iv')
  - [Tags](#P-Nullafi-Domains-StaticVault-Managers-DateOfBirth-DateOfBirthResponse-Tags 'Nullafi.Domains.StaticVault.Managers.DateOfBirth.DateOfBirthResponse.Tags')
  - [UpdatedAt](#P-Nullafi-Domains-StaticVault-Managers-DateOfBirth-DateOfBirthResponse-UpdatedAt 'Nullafi.Domains.StaticVault.Managers.DateOfBirth.DateOfBirthResponse.UpdatedAt')
- [NullafiSDK](#T-Nullafi-NullafiSDK 'Nullafi.NullafiSDK')
  - [#ctor(apiKey)](#M-Nullafi-NullafiSDK-#ctor-System-String- 'Nullafi.NullafiSDK.#ctor(System.String)')
  - [CreateClient()](#M-Nullafi-NullafiSDK-CreateClient 'Nullafi.NullafiSDK.CreateClient')
- [Security](#T-Nullafi-Security 'Nullafi.Security')
  - [#ctor()](#M-Nullafi-Security-#ctor 'Nullafi.Security.#ctor')
  - [Aes](#P-Nullafi-Security-Aes 'Nullafi.Security.Aes')
  - [Hmac](#P-Nullafi-Security-Hmac 'Nullafi.Security.Hmac')
  - [RSA](#P-Nullafi-Security-RSA 'Nullafi.Security.RSA')
- [StaticVault](#T-Nullafi-Domains-StaticVault-StaticVault 'Nullafi.Domains.StaticVault.StaticVault')
  - [Address](#P-Nullafi-Domains-StaticVault-StaticVault-Address 'Nullafi.Domains.StaticVault.StaticVault.Address')
  - [DateOfBirth](#P-Nullafi-Domains-StaticVault-StaticVault-DateOfBirth 'Nullafi.Domains.StaticVault.StaticVault.DateOfBirth')
  - [DriversLicense](#P-Nullafi-Domains-StaticVault-StaticVault-DriversLicense 'Nullafi.Domains.StaticVault.StaticVault.DriversLicense')
  - [FirstName](#P-Nullafi-Domains-StaticVault-StaticVault-FirstName 'Nullafi.Domains.StaticVault.StaticVault.FirstName')
  - [Gender](#P-Nullafi-Domains-StaticVault-StaticVault-Gender 'Nullafi.Domains.StaticVault.StaticVault.Gender')
  - [Generic](#P-Nullafi-Domains-StaticVault-StaticVault-Generic 'Nullafi.Domains.StaticVault.StaticVault.Generic')
  - [LastName](#P-Nullafi-Domains-StaticVault-StaticVault-LastName 'Nullafi.Domains.StaticVault.StaticVault.LastName')
  - [MasterKey](#P-Nullafi-Domains-StaticVault-StaticVault-MasterKey 'Nullafi.Domains.StaticVault.StaticVault.MasterKey')
  - [Passport](#P-Nullafi-Domains-StaticVault-StaticVault-Passport 'Nullafi.Domains.StaticVault.StaticVault.Passport')
  - [PlaceOfBirth](#P-Nullafi-Domains-StaticVault-StaticVault-PlaceOfBirth 'Nullafi.Domains.StaticVault.StaticVault.PlaceOfBirth')
  - [Race](#P-Nullafi-Domains-StaticVault-StaticVault-Race 'Nullafi.Domains.StaticVault.StaticVault.Race')
  - [Random](#P-Nullafi-Domains-StaticVault-StaticVault-Random 'Nullafi.Domains.StaticVault.StaticVault.Random')
  - [Ssn](#P-Nullafi-Domains-StaticVault-StaticVault-Ssn 'Nullafi.Domains.StaticVault.StaticVault.Ssn')
  - [TaxPayer](#P-Nullafi-Domains-StaticVault-StaticVault-TaxPayer 'Nullafi.Domains.StaticVault.StaticVault.TaxPayer')
  - [VaultId](#P-Nullafi-Domains-StaticVault-StaticVault-VaultId 'Nullafi.Domains.StaticVault.StaticVault.VaultId')
  - [VaultName](#P-Nullafi-Domains-StaticVault-StaticVault-VaultName 'Nullafi.Domains.StaticVault.StaticVault.VaultName')
  - [VehicleRegistration](#P-Nullafi-Domains-StaticVault-StaticVault-VehicleRegistration 'Nullafi.Domains.StaticVault.StaticVault.VehicleRegistration')
  - [CreateStaticVault(client,name,tags)](#M-Nullafi-Domains-StaticVault-StaticVault-CreateStaticVault-Nullafi-Client,System-String,System-Collections-Generic-List{System-String}- 'Nullafi.Domains.StaticVault.StaticVault.CreateStaticVault(Nullafi.Client,System.String,System.Collections.Generic.List{System.String})')
  - [RetrieveStaticVault(client,vaultId,masterKey)](#M-Nullafi-Domains-StaticVault-StaticVault-RetrieveStaticVault-Nullafi-Client,System-String,System-String- 'Nullafi.Domains.StaticVault.StaticVault.RetrieveStaticVault(Nullafi.Client,System.String,System.String)')

<a name='T-Nullafi-Domains-StaticVault-Managers-Address-AddressManager'></a>
## AddressManager `type`

##### Namespace

Nullafi.Domains.StaticVault.Managers.Address

##### Summary



<a name='M-Nullafi-Domains-StaticVault-Managers-Address-AddressManager-#ctor-Nullafi-Domains-StaticVault-StaticVault-'></a>
### #ctor(vault) `constructor`

##### Summary



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| vault | [Nullafi.Domains.StaticVault.StaticVault](#T-Nullafi-Domains-StaticVault-StaticVault 'Nullafi.Domains.StaticVault.StaticVault') |  |

<a name='M-Nullafi-Domains-StaticVault-Managers-Address-AddressManager-Create-System-String,System-Collections-Generic-List{System-String}-'></a>
### Create(address,tags) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| address | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| tags | [System.Collections.Generic.List{System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{System.String}') |  |

<a name='M-Nullafi-Domains-StaticVault-Managers-Address-AddressManager-Create-System-String,System-String,System-Collections-Generic-List{System-String}-'></a>
### Create(address,state,tags) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| address | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| state | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| tags | [System.Collections.Generic.List{System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{System.String}') |  |

<a name='M-Nullafi-Domains-StaticVault-Managers-Address-AddressManager-Delete-System-String-'></a>
### Delete(aliasId) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| aliasId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-Nullafi-Domains-StaticVault-Managers-Address-AddressManager-Retrieve-System-String-'></a>
### Retrieve(aliasId) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| aliasId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='T-Nullafi-Domains-StaticVault-Managers-Address-AddressRequest'></a>
## AddressRequest `type`

##### Namespace

Nullafi.Domains.StaticVault.Managers.Address

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Address-AddressRequest-Address'></a>
### Address `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Address-AddressRequest-AddressHash'></a>
### AddressHash `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Address-AddressRequest-AuthTag'></a>
### AuthTag `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Address-AddressRequest-Iv'></a>
### Iv `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Address-AddressRequest-Tags'></a>
### Tags `property`

##### Summary



<a name='T-Nullafi-Domains-StaticVault-Managers-Address-AddressResponse'></a>
## AddressResponse `type`

##### Namespace

Nullafi.Domains.StaticVault.Managers.Address

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Address-AddressResponse-Address'></a>
### Address `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Address-AddressResponse-AddressAlias'></a>
### AddressAlias `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Address-AddressResponse-AuthTag'></a>
### AuthTag `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Address-AddressResponse-CreatedAt'></a>
### CreatedAt `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Address-AddressResponse-Id'></a>
### Id `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Address-AddressResponse-Iv'></a>
### Iv `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Address-AddressResponse-Tags'></a>
### Tags `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Address-AddressResponse-UpdatedAt'></a>
### UpdatedAt `property`

##### Summary



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

<a name='T-Nullafi-Domains-CommunicationVault-CommunicationVault'></a>
## CommunicationVault `type`

##### Namespace

Nullafi.Domains.CommunicationVault

##### Summary



<a name='P-Nullafi-Domains-CommunicationVault-CommunicationVault-Email'></a>
### Email `property`

##### Summary



<a name='P-Nullafi-Domains-CommunicationVault-CommunicationVault-MasterKey'></a>
### MasterKey `property`

##### Summary



<a name='P-Nullafi-Domains-CommunicationVault-CommunicationVault-VaultId'></a>
### VaultId `property`

##### Summary



<a name='P-Nullafi-Domains-CommunicationVault-CommunicationVault-VaultName'></a>
### VaultName `property`

##### Summary



<a name='M-Nullafi-Domains-CommunicationVault-CommunicationVault-CreateCommunicationVault-Nullafi-Client,System-String,System-Collections-Generic-List{System-String}-'></a>
### CreateCommunicationVault(client,name,tags) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| client | [Nullafi.Client](#T-Nullafi-Client 'Nullafi.Client') |  |
| name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| tags | [System.Collections.Generic.List{System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{System.String}') |  |

<a name='M-Nullafi-Domains-CommunicationVault-CommunicationVault-RetrieveCommunicationVault-Nullafi-Client,System-String,System-String-'></a>
### RetrieveCommunicationVault(client,vaultId,masterKey) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| client | [Nullafi.Client](#T-Nullafi-Client 'Nullafi.Client') |  |
| vaultId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| masterKey | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='T-Nullafi-Domains-StaticVault-Managers-DateOfBirth-DateOfBirthManager'></a>
## DateOfBirthManager `type`

##### Namespace

Nullafi.Domains.StaticVault.Managers.DateOfBirth

##### Summary



<a name='M-Nullafi-Domains-StaticVault-Managers-DateOfBirth-DateOfBirthManager-#ctor-Nullafi-Domains-StaticVault-StaticVault-'></a>
### #ctor(vault) `constructor`

##### Summary



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| vault | [Nullafi.Domains.StaticVault.StaticVault](#T-Nullafi-Domains-StaticVault-StaticVault 'Nullafi.Domains.StaticVault.StaticVault') |  |

<a name='M-Nullafi-Domains-StaticVault-Managers-DateOfBirth-DateOfBirthManager-Create-System-String,System-Collections-Generic-List{System-String}-'></a>
### Create(dateOfBirth,tags) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| dateOfBirth | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| tags | [System.Collections.Generic.List{System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{System.String}') |  |

<a name='M-Nullafi-Domains-StaticVault-Managers-DateOfBirth-DateOfBirthManager-Create-System-String,System-Nullable{System-Int32},System-Nullable{System-Int32},System-Collections-Generic-List{System-String}-'></a>
### Create(dateOfBirth,year,month,tags) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| dateOfBirth | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| year | [System.Nullable{System.Int32}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Int32}') |  |
| month | [System.Nullable{System.Int32}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Int32}') |  |
| tags | [System.Collections.Generic.List{System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{System.String}') |  |

<a name='M-Nullafi-Domains-StaticVault-Managers-DateOfBirth-DateOfBirthManager-Delete-System-String-'></a>
### Delete(aliasId) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| aliasId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-Nullafi-Domains-StaticVault-Managers-DateOfBirth-DateOfBirthManager-Retrieve-System-String-'></a>
### Retrieve(aliasId) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| aliasId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='T-Nullafi-Domains-StaticVault-Managers-DateOfBirth-DateOfBirthRequest'></a>
## DateOfBirthRequest `type`

##### Namespace

Nullafi.Domains.StaticVault.Managers.DateOfBirth

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-DateOfBirth-DateOfBirthRequest-AuthTag'></a>
### AuthTag `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-DateOfBirth-DateOfBirthRequest-DateOfBirth'></a>
### DateOfBirth `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-DateOfBirth-DateOfBirthRequest-DateOfBirthHash'></a>
### DateOfBirthHash `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-DateOfBirth-DateOfBirthRequest-Iv'></a>
### Iv `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-DateOfBirth-DateOfBirthRequest-Tags'></a>
### Tags `property`

##### Summary



<a name='T-Nullafi-Domains-StaticVault-Managers-DateOfBirth-DateOfBirthResponse'></a>
## DateOfBirthResponse `type`

##### Namespace

Nullafi.Domains.StaticVault.Managers.DateOfBirth

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-DateOfBirth-DateOfBirthResponse-AuthTag'></a>
### AuthTag `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-DateOfBirth-DateOfBirthResponse-CreatedAt'></a>
### CreatedAt `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-DateOfBirth-DateOfBirthResponse-DateOfBirth'></a>
### DateOfBirth `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-DateOfBirth-DateOfBirthResponse-DateOfBirthAlias'></a>
### DateOfBirthAlias `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-DateOfBirth-DateOfBirthResponse-Id'></a>
### Id `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-DateOfBirth-DateOfBirthResponse-Iv'></a>
### Iv `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-DateOfBirth-DateOfBirthResponse-Tags'></a>
### Tags `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-DateOfBirth-DateOfBirthResponse-UpdatedAt'></a>
### UpdatedAt `property`

##### Summary



<a name='T-Nullafi-NullafiSDK'></a>
## NullafiSDK `type`

##### Namespace

Nullafi

##### Summary



<a name='M-Nullafi-NullafiSDK-#ctor-System-String-'></a>
### #ctor(apiKey) `constructor`

##### Summary



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| apiKey | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-Nullafi-NullafiSDK-CreateClient'></a>
### CreateClient() `method`

##### Summary



##### Returns



##### Parameters

This method has no parameters.

<a name='T-Nullafi-Security'></a>
## Security `type`

##### Namespace

Nullafi

##### Summary



<a name='M-Nullafi-Security-#ctor'></a>
### #ctor() `constructor`

##### Summary



##### Parameters

This constructor has no parameters.

<a name='P-Nullafi-Security-Aes'></a>
### Aes `property`

##### Summary



<a name='P-Nullafi-Security-Hmac'></a>
### Hmac `property`

##### Summary



<a name='P-Nullafi-Security-RSA'></a>
### RSA `property`

##### Summary



<a name='T-Nullafi-Domains-StaticVault-StaticVault'></a>
## StaticVault `type`

##### Namespace

Nullafi.Domains.StaticVault

##### Summary



<a name='P-Nullafi-Domains-StaticVault-StaticVault-Address'></a>
### Address `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-StaticVault-DateOfBirth'></a>
### DateOfBirth `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-StaticVault-DriversLicense'></a>
### DriversLicense `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-StaticVault-FirstName'></a>
### FirstName `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-StaticVault-Gender'></a>
### Gender `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-StaticVault-Generic'></a>
### Generic `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-StaticVault-LastName'></a>
### LastName `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-StaticVault-MasterKey'></a>
### MasterKey `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-StaticVault-Passport'></a>
### Passport `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-StaticVault-PlaceOfBirth'></a>
### PlaceOfBirth `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-StaticVault-Race'></a>
### Race `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-StaticVault-Random'></a>
### Random `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-StaticVault-Ssn'></a>
### Ssn `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-StaticVault-TaxPayer'></a>
### TaxPayer `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-StaticVault-VaultId'></a>
### VaultId `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-StaticVault-VaultName'></a>
### VaultName `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-StaticVault-VehicleRegistration'></a>
### VehicleRegistration `property`

##### Summary



<a name='M-Nullafi-Domains-StaticVault-StaticVault-CreateStaticVault-Nullafi-Client,System-String,System-Collections-Generic-List{System-String}-'></a>
### CreateStaticVault(client,name,tags) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| client | [Nullafi.Client](#T-Nullafi-Client 'Nullafi.Client') |  |
| name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| tags | [System.Collections.Generic.List{System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{System.String}') |  |

<a name='M-Nullafi-Domains-StaticVault-StaticVault-RetrieveStaticVault-Nullafi-Client,System-String,System-String-'></a>
### RetrieveStaticVault(client,vaultId,masterKey) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| client | [Nullafi.Client](#T-Nullafi-Client 'Nullafi.Client') |  |
| vaultId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| masterKey | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
