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
- [Aesgcm](#T-Nullafi-Services-Crypto-Aesgcm 'Nullafi.Services.Crypto.Aesgcm')
  - [Decrypt(masterKey,iv,authTag,cipherText,returnBase64)](#M-Nullafi-Services-Crypto-Aesgcm-Decrypt-System-String,System-String,System-String,System-String- 'Nullafi.Services.Crypto.Aesgcm.Decrypt(System.String,System.String,System.String,System.String)')
  - [Encrypt(masterKey,iv,plainText)](#M-Nullafi-Services-Crypto-Aesgcm-Encrypt-System-String,System-String,System-String- 'Nullafi.Services.Crypto.Aesgcm.Encrypt(System.String,System.String,System.String)')
  - [GenerateStringIv()](#M-Nullafi-Services-Crypto-Aesgcm-GenerateStringIv 'Nullafi.Services.Crypto.Aesgcm.GenerateStringIv')
  - [GenerateStringMasterKey()](#M-Nullafi-Services-Crypto-Aesgcm-GenerateStringMasterKey 'Nullafi.Services.Crypto.Aesgcm.GenerateStringMasterKey')
- [Api](#T-Nullafi-Services-Api 'Nullafi.Services.Api')
  - [#ctor()](#M-Nullafi-Services-Api-#ctor 'Nullafi.Services.Api.#ctor')
- [Client](#T-Nullafi-Client 'Nullafi.Client')
  - [HashKey](#P-Nullafi-Client-HashKey 'Nullafi.Client.HashKey')
  - [Authenticate(apiKey)](#M-Nullafi-Client-Authenticate-System-String- 'Nullafi.Client.Authenticate(System.String)')
  - [CreateCommunicationVault(name,tags)](#M-Nullafi-Client-CreateCommunicationVault-System-String,System-Collections-Generic-List{System-String}- 'Nullafi.Client.CreateCommunicationVault(System.String,System.Collections.Generic.List{System.String})')
  - [CreateStaticVault(name,tags)](#M-Nullafi-Client-CreateStaticVault-System-String,System-Collections-Generic-List{System-String}- 'Nullafi.Client.CreateStaticVault(System.String,System.Collections.Generic.List{System.String})')
  - [RetrieveCommunicationVault(vaultId,masterKey)](#M-Nullafi-Client-RetrieveCommunicationVault-System-String,System-String- 'Nullafi.Client.RetrieveCommunicationVault(System.String,System.String)')
  - [RetrieveStaticVault(vaultId,masterKey)](#M-Nullafi-Client-RetrieveStaticVault-System-String,System-String- 'Nullafi.Client.RetrieveStaticVault(System.String,System.String)')
- [CommunicationVault](#T-Nullafi-Domains-CommunicationVault-CommunicationVault 'Nullafi.Domains.CommunicationVault.CommunicationVault')
  - [#ctor(client,vaultId,vaultName,masterKey)](#M-Nullafi-Domains-CommunicationVault-CommunicationVault-#ctor-Nullafi-Client,System-String,System-String,System-String- 'Nullafi.Domains.CommunicationVault.CommunicationVault.#ctor(Nullafi.Client,System.String,System.String,System.String)')
  - [Email](#P-Nullafi-Domains-CommunicationVault-CommunicationVault-Email 'Nullafi.Domains.CommunicationVault.CommunicationVault.Email')
  - [MasterKey](#P-Nullafi-Domains-CommunicationVault-CommunicationVault-MasterKey 'Nullafi.Domains.CommunicationVault.CommunicationVault.MasterKey')
  - [VaultId](#P-Nullafi-Domains-CommunicationVault-CommunicationVault-VaultId 'Nullafi.Domains.CommunicationVault.CommunicationVault.VaultId')
  - [VaultName](#P-Nullafi-Domains-CommunicationVault-CommunicationVault-VaultName 'Nullafi.Domains.CommunicationVault.CommunicationVault.VaultName')
  - [CreateCommunicationVault(client,name,tags)](#M-Nullafi-Domains-CommunicationVault-CommunicationVault-CreateCommunicationVault-Nullafi-Client,System-String,System-Collections-Generic-List{System-String}- 'Nullafi.Domains.CommunicationVault.CommunicationVault.CreateCommunicationVault(Nullafi.Client,System.String,System.Collections.Generic.List{System.String})')
  - [Hash(value)](#M-Nullafi-Domains-CommunicationVault-CommunicationVault-Hash-System-String- 'Nullafi.Domains.CommunicationVault.CommunicationVault.Hash(System.String)')
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
- [DriversLicenseManager](#T-Nullafi-Domains-StaticVault-Managers-DriversLicense-DriversLicenseManager 'Nullafi.Domains.StaticVault.Managers.DriversLicense.DriversLicenseManager')
  - [#ctor(vault)](#M-Nullafi-Domains-StaticVault-Managers-DriversLicense-DriversLicenseManager-#ctor-Nullafi-Domains-StaticVault-StaticVault- 'Nullafi.Domains.StaticVault.Managers.DriversLicense.DriversLicenseManager.#ctor(Nullafi.Domains.StaticVault.StaticVault)')
  - [Create(driversLicense,tags)](#M-Nullafi-Domains-StaticVault-Managers-DriversLicense-DriversLicenseManager-Create-System-String,System-Collections-Generic-List{System-String}- 'Nullafi.Domains.StaticVault.Managers.DriversLicense.DriversLicenseManager.Create(System.String,System.Collections.Generic.List{System.String})')
  - [Create(driversLicense,state,tags)](#M-Nullafi-Domains-StaticVault-Managers-DriversLicense-DriversLicenseManager-Create-System-String,System-String,System-Collections-Generic-List{System-String}- 'Nullafi.Domains.StaticVault.Managers.DriversLicense.DriversLicenseManager.Create(System.String,System.String,System.Collections.Generic.List{System.String})')
  - [Delete(aliasId)](#M-Nullafi-Domains-StaticVault-Managers-DriversLicense-DriversLicenseManager-Delete-System-String- 'Nullafi.Domains.StaticVault.Managers.DriversLicense.DriversLicenseManager.Delete(System.String)')
  - [Retrieve(aliasId)](#M-Nullafi-Domains-StaticVault-Managers-DriversLicense-DriversLicenseManager-Retrieve-System-String- 'Nullafi.Domains.StaticVault.Managers.DriversLicense.DriversLicenseManager.Retrieve(System.String)')
- [DriversLicenseRequest](#T-Nullafi-Domains-StaticVault-Managers-DriversLicense-DriversLicenseRequest 'Nullafi.Domains.StaticVault.Managers.DriversLicense.DriversLicenseRequest')
  - [AuthTag](#P-Nullafi-Domains-StaticVault-Managers-DriversLicense-DriversLicenseRequest-AuthTag 'Nullafi.Domains.StaticVault.Managers.DriversLicense.DriversLicenseRequest.AuthTag')
  - [DriversLicense](#P-Nullafi-Domains-StaticVault-Managers-DriversLicense-DriversLicenseRequest-DriversLicense 'Nullafi.Domains.StaticVault.Managers.DriversLicense.DriversLicenseRequest.DriversLicense')
  - [DriversLicenseHash](#P-Nullafi-Domains-StaticVault-Managers-DriversLicense-DriversLicenseRequest-DriversLicenseHash 'Nullafi.Domains.StaticVault.Managers.DriversLicense.DriversLicenseRequest.DriversLicenseHash')
  - [Iv](#P-Nullafi-Domains-StaticVault-Managers-DriversLicense-DriversLicenseRequest-Iv 'Nullafi.Domains.StaticVault.Managers.DriversLicense.DriversLicenseRequest.Iv')
  - [Tags](#P-Nullafi-Domains-StaticVault-Managers-DriversLicense-DriversLicenseRequest-Tags 'Nullafi.Domains.StaticVault.Managers.DriversLicense.DriversLicenseRequest.Tags')
- [DriversLicenseResponse](#T-Nullafi-Domains-StaticVault-Managers-DriversLicense-DriversLicenseResponse 'Nullafi.Domains.StaticVault.Managers.DriversLicense.DriversLicenseResponse')
  - [AuthTag](#P-Nullafi-Domains-StaticVault-Managers-DriversLicense-DriversLicenseResponse-AuthTag 'Nullafi.Domains.StaticVault.Managers.DriversLicense.DriversLicenseResponse.AuthTag')
  - [CreatedAt](#P-Nullafi-Domains-StaticVault-Managers-DriversLicense-DriversLicenseResponse-CreatedAt 'Nullafi.Domains.StaticVault.Managers.DriversLicense.DriversLicenseResponse.CreatedAt')
  - [DriversLicense](#P-Nullafi-Domains-StaticVault-Managers-DriversLicense-DriversLicenseResponse-DriversLicense 'Nullafi.Domains.StaticVault.Managers.DriversLicense.DriversLicenseResponse.DriversLicense')
  - [DriversLicenseAlias](#P-Nullafi-Domains-StaticVault-Managers-DriversLicense-DriversLicenseResponse-DriversLicenseAlias 'Nullafi.Domains.StaticVault.Managers.DriversLicense.DriversLicenseResponse.DriversLicenseAlias')
  - [Id](#P-Nullafi-Domains-StaticVault-Managers-DriversLicense-DriversLicenseResponse-Id 'Nullafi.Domains.StaticVault.Managers.DriversLicense.DriversLicenseResponse.Id')
  - [Iv](#P-Nullafi-Domains-StaticVault-Managers-DriversLicense-DriversLicenseResponse-Iv 'Nullafi.Domains.StaticVault.Managers.DriversLicense.DriversLicenseResponse.Iv')
  - [Tags](#P-Nullafi-Domains-StaticVault-Managers-DriversLicense-DriversLicenseResponse-Tags 'Nullafi.Domains.StaticVault.Managers.DriversLicense.DriversLicenseResponse.Tags')
  - [UpdatedAt](#P-Nullafi-Domains-StaticVault-Managers-DriversLicense-DriversLicenseResponse-UpdatedAt 'Nullafi.Domains.StaticVault.Managers.DriversLicense.DriversLicenseResponse.UpdatedAt')
- [EmailManager](#T-Nullafi-Domains-CommunicationVault-Managers-Email-EmailManager 'Nullafi.Domains.CommunicationVault.Managers.Email.EmailManager')
  - [#ctor(vault)](#M-Nullafi-Domains-CommunicationVault-Managers-Email-EmailManager-#ctor-Nullafi-Domains-CommunicationVault-CommunicationVault- 'Nullafi.Domains.CommunicationVault.Managers.Email.EmailManager.#ctor(Nullafi.Domains.CommunicationVault.CommunicationVault)')
  - [Create(email,tags)](#M-Nullafi-Domains-CommunicationVault-Managers-Email-EmailManager-Create-System-String,System-Collections-Generic-List{System-String}- 'Nullafi.Domains.CommunicationVault.Managers.Email.EmailManager.Create(System.String,System.Collections.Generic.List{System.String})')
  - [Delete(aliasId)](#M-Nullafi-Domains-CommunicationVault-Managers-Email-EmailManager-Delete-System-String- 'Nullafi.Domains.CommunicationVault.Managers.Email.EmailManager.Delete(System.String)')
  - [Retrieve(aliasId)](#M-Nullafi-Domains-CommunicationVault-Managers-Email-EmailManager-Retrieve-System-String- 'Nullafi.Domains.CommunicationVault.Managers.Email.EmailManager.Retrieve(System.String)')
- [EmailRequest](#T-Nullafi-Domains-CommunicationVault-Managers-Email-EmailRequest 'Nullafi.Domains.CommunicationVault.Managers.Email.EmailRequest')
  - [AuthTag](#P-Nullafi-Domains-CommunicationVault-Managers-Email-EmailRequest-AuthTag 'Nullafi.Domains.CommunicationVault.Managers.Email.EmailRequest.AuthTag')
  - [Email](#P-Nullafi-Domains-CommunicationVault-Managers-Email-EmailRequest-Email 'Nullafi.Domains.CommunicationVault.Managers.Email.EmailRequest.Email')
  - [EmailHash](#P-Nullafi-Domains-CommunicationVault-Managers-Email-EmailRequest-EmailHash 'Nullafi.Domains.CommunicationVault.Managers.Email.EmailRequest.EmailHash')
  - [Iv](#P-Nullafi-Domains-CommunicationVault-Managers-Email-EmailRequest-Iv 'Nullafi.Domains.CommunicationVault.Managers.Email.EmailRequest.Iv')
  - [Tags](#P-Nullafi-Domains-CommunicationVault-Managers-Email-EmailRequest-Tags 'Nullafi.Domains.CommunicationVault.Managers.Email.EmailRequest.Tags')
- [EmailResponse](#T-Nullafi-Domains-CommunicationVault-Managers-Email-EmailResponse 'Nullafi.Domains.CommunicationVault.Managers.Email.EmailResponse')
  - [AuthTag](#P-Nullafi-Domains-CommunicationVault-Managers-Email-EmailResponse-AuthTag 'Nullafi.Domains.CommunicationVault.Managers.Email.EmailResponse.AuthTag')
  - [CreatedAt](#P-Nullafi-Domains-CommunicationVault-Managers-Email-EmailResponse-CreatedAt 'Nullafi.Domains.CommunicationVault.Managers.Email.EmailResponse.CreatedAt')
  - [Email](#P-Nullafi-Domains-CommunicationVault-Managers-Email-EmailResponse-Email 'Nullafi.Domains.CommunicationVault.Managers.Email.EmailResponse.Email')
  - [EmailAlias](#P-Nullafi-Domains-CommunicationVault-Managers-Email-EmailResponse-EmailAlias 'Nullafi.Domains.CommunicationVault.Managers.Email.EmailResponse.EmailAlias')
  - [Id](#P-Nullafi-Domains-CommunicationVault-Managers-Email-EmailResponse-Id 'Nullafi.Domains.CommunicationVault.Managers.Email.EmailResponse.Id')
  - [Iv](#P-Nullafi-Domains-CommunicationVault-Managers-Email-EmailResponse-Iv 'Nullafi.Domains.CommunicationVault.Managers.Email.EmailResponse.Iv')
  - [Tags](#P-Nullafi-Domains-CommunicationVault-Managers-Email-EmailResponse-Tags 'Nullafi.Domains.CommunicationVault.Managers.Email.EmailResponse.Tags')
  - [UpdatedAt](#P-Nullafi-Domains-CommunicationVault-Managers-Email-EmailResponse-UpdatedAt 'Nullafi.Domains.CommunicationVault.Managers.Email.EmailResponse.UpdatedAt')
- [FirstNameManager](#T-Nullafi-Domains-StaticVault-Managers-FirstName-FirstNameManager 'Nullafi.Domains.StaticVault.Managers.FirstName.FirstNameManager')
  - [#ctor(vault)](#M-Nullafi-Domains-StaticVault-Managers-FirstName-FirstNameManager-#ctor-Nullafi-Domains-StaticVault-StaticVault- 'Nullafi.Domains.StaticVault.Managers.FirstName.FirstNameManager.#ctor(Nullafi.Domains.StaticVault.StaticVault)')
  - [Create(firstname,tags)](#M-Nullafi-Domains-StaticVault-Managers-FirstName-FirstNameManager-Create-System-String,System-Collections-Generic-List{System-String}- 'Nullafi.Domains.StaticVault.Managers.FirstName.FirstNameManager.Create(System.String,System.Collections.Generic.List{System.String})')
  - [Create(firstname,gender,tags)](#M-Nullafi-Domains-StaticVault-Managers-FirstName-FirstNameManager-Create-System-String,System-String,System-Collections-Generic-List{System-String}- 'Nullafi.Domains.StaticVault.Managers.FirstName.FirstNameManager.Create(System.String,System.String,System.Collections.Generic.List{System.String})')
  - [Delete(aliasId)](#M-Nullafi-Domains-StaticVault-Managers-FirstName-FirstNameManager-Delete-System-String- 'Nullafi.Domains.StaticVault.Managers.FirstName.FirstNameManager.Delete(System.String)')
  - [Retrieve(aliasId)](#M-Nullafi-Domains-StaticVault-Managers-FirstName-FirstNameManager-Retrieve-System-String- 'Nullafi.Domains.StaticVault.Managers.FirstName.FirstNameManager.Retrieve(System.String)')
- [FirstNameRequest](#T-Nullafi-Domains-StaticVault-Managers-FirstName-FirstNameRequest 'Nullafi.Domains.StaticVault.Managers.FirstName.FirstNameRequest')
  - [AuthTag](#P-Nullafi-Domains-StaticVault-Managers-FirstName-FirstNameRequest-AuthTag 'Nullafi.Domains.StaticVault.Managers.FirstName.FirstNameRequest.AuthTag')
  - [FirstName](#P-Nullafi-Domains-StaticVault-Managers-FirstName-FirstNameRequest-FirstName 'Nullafi.Domains.StaticVault.Managers.FirstName.FirstNameRequest.FirstName')
  - [FirstNameHash](#P-Nullafi-Domains-StaticVault-Managers-FirstName-FirstNameRequest-FirstNameHash 'Nullafi.Domains.StaticVault.Managers.FirstName.FirstNameRequest.FirstNameHash')
  - [Iv](#P-Nullafi-Domains-StaticVault-Managers-FirstName-FirstNameRequest-Iv 'Nullafi.Domains.StaticVault.Managers.FirstName.FirstNameRequest.Iv')
  - [Tags](#P-Nullafi-Domains-StaticVault-Managers-FirstName-FirstNameRequest-Tags 'Nullafi.Domains.StaticVault.Managers.FirstName.FirstNameRequest.Tags')
- [FirstNameResponse](#T-Nullafi-Domains-StaticVault-Managers-FirstName-FirstNameResponse 'Nullafi.Domains.StaticVault.Managers.FirstName.FirstNameResponse')
  - [AuthTag](#P-Nullafi-Domains-StaticVault-Managers-FirstName-FirstNameResponse-AuthTag 'Nullafi.Domains.StaticVault.Managers.FirstName.FirstNameResponse.AuthTag')
  - [CreatedAt](#P-Nullafi-Domains-StaticVault-Managers-FirstName-FirstNameResponse-CreatedAt 'Nullafi.Domains.StaticVault.Managers.FirstName.FirstNameResponse.CreatedAt')
  - [FirstName](#P-Nullafi-Domains-StaticVault-Managers-FirstName-FirstNameResponse-FirstName 'Nullafi.Domains.StaticVault.Managers.FirstName.FirstNameResponse.FirstName')
  - [FirstNameAlias](#P-Nullafi-Domains-StaticVault-Managers-FirstName-FirstNameResponse-FirstNameAlias 'Nullafi.Domains.StaticVault.Managers.FirstName.FirstNameResponse.FirstNameAlias')
  - [Id](#P-Nullafi-Domains-StaticVault-Managers-FirstName-FirstNameResponse-Id 'Nullafi.Domains.StaticVault.Managers.FirstName.FirstNameResponse.Id')
  - [Iv](#P-Nullafi-Domains-StaticVault-Managers-FirstName-FirstNameResponse-Iv 'Nullafi.Domains.StaticVault.Managers.FirstName.FirstNameResponse.Iv')
  - [Tags](#P-Nullafi-Domains-StaticVault-Managers-FirstName-FirstNameResponse-Tags 'Nullafi.Domains.StaticVault.Managers.FirstName.FirstNameResponse.Tags')
  - [UpdatedAt](#P-Nullafi-Domains-StaticVault-Managers-FirstName-FirstNameResponse-UpdatedAt 'Nullafi.Domains.StaticVault.Managers.FirstName.FirstNameResponse.UpdatedAt')
- [GenderManager](#T-Nullafi-Domains-StaticVault-Managers-Gender-GenderManager 'Nullafi.Domains.StaticVault.Managers.Gender.GenderManager')
  - [#ctor(vault)](#M-Nullafi-Domains-StaticVault-Managers-Gender-GenderManager-#ctor-Nullafi-Domains-StaticVault-StaticVault- 'Nullafi.Domains.StaticVault.Managers.Gender.GenderManager.#ctor(Nullafi.Domains.StaticVault.StaticVault)')
  - [Create(gender,tags)](#M-Nullafi-Domains-StaticVault-Managers-Gender-GenderManager-Create-System-String,System-Collections-Generic-List{System-String}- 'Nullafi.Domains.StaticVault.Managers.Gender.GenderManager.Create(System.String,System.Collections.Generic.List{System.String})')
  - [Delete(aliasId)](#M-Nullafi-Domains-StaticVault-Managers-Gender-GenderManager-Delete-System-String- 'Nullafi.Domains.StaticVault.Managers.Gender.GenderManager.Delete(System.String)')
  - [Retrieve(aliasId)](#M-Nullafi-Domains-StaticVault-Managers-Gender-GenderManager-Retrieve-System-String- 'Nullafi.Domains.StaticVault.Managers.Gender.GenderManager.Retrieve(System.String)')
- [GenderRequest](#T-Nullafi-Domains-StaticVault-Managers-Gender-GenderRequest 'Nullafi.Domains.StaticVault.Managers.Gender.GenderRequest')
  - [AuthTag](#P-Nullafi-Domains-StaticVault-Managers-Gender-GenderRequest-AuthTag 'Nullafi.Domains.StaticVault.Managers.Gender.GenderRequest.AuthTag')
  - [Gender](#P-Nullafi-Domains-StaticVault-Managers-Gender-GenderRequest-Gender 'Nullafi.Domains.StaticVault.Managers.Gender.GenderRequest.Gender')
  - [GenderHash](#P-Nullafi-Domains-StaticVault-Managers-Gender-GenderRequest-GenderHash 'Nullafi.Domains.StaticVault.Managers.Gender.GenderRequest.GenderHash')
  - [Iv](#P-Nullafi-Domains-StaticVault-Managers-Gender-GenderRequest-Iv 'Nullafi.Domains.StaticVault.Managers.Gender.GenderRequest.Iv')
  - [Tags](#P-Nullafi-Domains-StaticVault-Managers-Gender-GenderRequest-Tags 'Nullafi.Domains.StaticVault.Managers.Gender.GenderRequest.Tags')
- [GenderResponse](#T-Nullafi-Domains-StaticVault-Managers-Gender-GenderResponse 'Nullafi.Domains.StaticVault.Managers.Gender.GenderResponse')
  - [AuthTag](#P-Nullafi-Domains-StaticVault-Managers-Gender-GenderResponse-AuthTag 'Nullafi.Domains.StaticVault.Managers.Gender.GenderResponse.AuthTag')
  - [CreatedAt](#P-Nullafi-Domains-StaticVault-Managers-Gender-GenderResponse-CreatedAt 'Nullafi.Domains.StaticVault.Managers.Gender.GenderResponse.CreatedAt')
  - [Gender](#P-Nullafi-Domains-StaticVault-Managers-Gender-GenderResponse-Gender 'Nullafi.Domains.StaticVault.Managers.Gender.GenderResponse.Gender')
  - [GenderAlias](#P-Nullafi-Domains-StaticVault-Managers-Gender-GenderResponse-GenderAlias 'Nullafi.Domains.StaticVault.Managers.Gender.GenderResponse.GenderAlias')
  - [Id](#P-Nullafi-Domains-StaticVault-Managers-Gender-GenderResponse-Id 'Nullafi.Domains.StaticVault.Managers.Gender.GenderResponse.Id')
  - [Iv](#P-Nullafi-Domains-StaticVault-Managers-Gender-GenderResponse-Iv 'Nullafi.Domains.StaticVault.Managers.Gender.GenderResponse.Iv')
  - [Tags](#P-Nullafi-Domains-StaticVault-Managers-Gender-GenderResponse-Tags 'Nullafi.Domains.StaticVault.Managers.Gender.GenderResponse.Tags')
  - [UpdatedAt](#P-Nullafi-Domains-StaticVault-Managers-Gender-GenderResponse-UpdatedAt 'Nullafi.Domains.StaticVault.Managers.Gender.GenderResponse.UpdatedAt')
- [GenericManager](#T-Nullafi-Domains-StaticVault-Managers-Generic-GenericManager 'Nullafi.Domains.StaticVault.Managers.Generic.GenericManager')
  - [#ctor(vault)](#M-Nullafi-Domains-StaticVault-Managers-Generic-GenericManager-#ctor-Nullafi-Domains-StaticVault-StaticVault- 'Nullafi.Domains.StaticVault.Managers.Generic.GenericManager.#ctor(Nullafi.Domains.StaticVault.StaticVault)')
  - [Create(data,tags)](#M-Nullafi-Domains-StaticVault-Managers-Generic-GenericManager-Create-System-String,System-Collections-Generic-List{System-String}- 'Nullafi.Domains.StaticVault.Managers.Generic.GenericManager.Create(System.String,System.Collections.Generic.List{System.String})')
  - [Delete(aliasId)](#M-Nullafi-Domains-StaticVault-Managers-Generic-GenericManager-Delete-System-String- 'Nullafi.Domains.StaticVault.Managers.Generic.GenericManager.Delete(System.String)')
  - [Retrieve(aliasId)](#M-Nullafi-Domains-StaticVault-Managers-Generic-GenericManager-Retrieve-System-String- 'Nullafi.Domains.StaticVault.Managers.Generic.GenericManager.Retrieve(System.String)')
- [GenericRequest](#T-Nullafi-Domains-StaticVault-Managers-Generic-GenericRequest 'Nullafi.Domains.StaticVault.Managers.Generic.GenericRequest')
  - [AuthTag](#P-Nullafi-Domains-StaticVault-Managers-Generic-GenericRequest-AuthTag 'Nullafi.Domains.StaticVault.Managers.Generic.GenericRequest.AuthTag')
  - [Data](#P-Nullafi-Domains-StaticVault-Managers-Generic-GenericRequest-Data 'Nullafi.Domains.StaticVault.Managers.Generic.GenericRequest.Data')
  - [DataHash](#P-Nullafi-Domains-StaticVault-Managers-Generic-GenericRequest-DataHash 'Nullafi.Domains.StaticVault.Managers.Generic.GenericRequest.DataHash')
  - [Iv](#P-Nullafi-Domains-StaticVault-Managers-Generic-GenericRequest-Iv 'Nullafi.Domains.StaticVault.Managers.Generic.GenericRequest.Iv')
  - [Tags](#P-Nullafi-Domains-StaticVault-Managers-Generic-GenericRequest-Tags 'Nullafi.Domains.StaticVault.Managers.Generic.GenericRequest.Tags')
- [GenericResponse](#T-Nullafi-Domains-StaticVault-Managers-Generic-GenericResponse 'Nullafi.Domains.StaticVault.Managers.Generic.GenericResponse')
  - [Alias](#P-Nullafi-Domains-StaticVault-Managers-Generic-GenericResponse-Alias 'Nullafi.Domains.StaticVault.Managers.Generic.GenericResponse.Alias')
  - [AuthTag](#P-Nullafi-Domains-StaticVault-Managers-Generic-GenericResponse-AuthTag 'Nullafi.Domains.StaticVault.Managers.Generic.GenericResponse.AuthTag')
  - [CreatedAt](#P-Nullafi-Domains-StaticVault-Managers-Generic-GenericResponse-CreatedAt 'Nullafi.Domains.StaticVault.Managers.Generic.GenericResponse.CreatedAt')
  - [Data](#P-Nullafi-Domains-StaticVault-Managers-Generic-GenericResponse-Data 'Nullafi.Domains.StaticVault.Managers.Generic.GenericResponse.Data')
  - [Id](#P-Nullafi-Domains-StaticVault-Managers-Generic-GenericResponse-Id 'Nullafi.Domains.StaticVault.Managers.Generic.GenericResponse.Id')
  - [Iv](#P-Nullafi-Domains-StaticVault-Managers-Generic-GenericResponse-Iv 'Nullafi.Domains.StaticVault.Managers.Generic.GenericResponse.Iv')
  - [Tags](#P-Nullafi-Domains-StaticVault-Managers-Generic-GenericResponse-Tags 'Nullafi.Domains.StaticVault.Managers.Generic.GenericResponse.Tags')
  - [UpdatedAt](#P-Nullafi-Domains-StaticVault-Managers-Generic-GenericResponse-UpdatedAt 'Nullafi.Domains.StaticVault.Managers.Generic.GenericResponse.UpdatedAt')
- [Hmac](#T-Nullafi-Services-Crypto-Hmac 'Nullafi.Services.Crypto.Hmac')
  - [Hash(value,secret)](#M-Nullafi-Services-Crypto-Hmac-Hash-System-String,System-String- 'Nullafi.Services.Crypto.Hmac.Hash(System.String,System.String)')
- [LastNameManager](#T-Nullafi-Domains-StaticVault-Managers-LastName-LastNameManager 'Nullafi.Domains.StaticVault.Managers.LastName.LastNameManager')
  - [#ctor(vault)](#M-Nullafi-Domains-StaticVault-Managers-LastName-LastNameManager-#ctor-Nullafi-Domains-StaticVault-StaticVault- 'Nullafi.Domains.StaticVault.Managers.LastName.LastNameManager.#ctor(Nullafi.Domains.StaticVault.StaticVault)')
  - [Create(lastname,tags)](#M-Nullafi-Domains-StaticVault-Managers-LastName-LastNameManager-Create-System-String,System-Collections-Generic-List{System-String}- 'Nullafi.Domains.StaticVault.Managers.LastName.LastNameManager.Create(System.String,System.Collections.Generic.List{System.String})')
  - [Create(lastname,gender,tags)](#M-Nullafi-Domains-StaticVault-Managers-LastName-LastNameManager-Create-System-String,System-String,System-Collections-Generic-List{System-String}- 'Nullafi.Domains.StaticVault.Managers.LastName.LastNameManager.Create(System.String,System.String,System.Collections.Generic.List{System.String})')
  - [Delete(aliasId)](#M-Nullafi-Domains-StaticVault-Managers-LastName-LastNameManager-Delete-System-String- 'Nullafi.Domains.StaticVault.Managers.LastName.LastNameManager.Delete(System.String)')
  - [Retrieve(aliasId)](#M-Nullafi-Domains-StaticVault-Managers-LastName-LastNameManager-Retrieve-System-String- 'Nullafi.Domains.StaticVault.Managers.LastName.LastNameManager.Retrieve(System.String)')
- [LastNameRequest](#T-Nullafi-Domains-StaticVault-Managers-LastName-LastNameRequest 'Nullafi.Domains.StaticVault.Managers.LastName.LastNameRequest')
  - [AuthTag](#P-Nullafi-Domains-StaticVault-Managers-LastName-LastNameRequest-AuthTag 'Nullafi.Domains.StaticVault.Managers.LastName.LastNameRequest.AuthTag')
  - [Iv](#P-Nullafi-Domains-StaticVault-Managers-LastName-LastNameRequest-Iv 'Nullafi.Domains.StaticVault.Managers.LastName.LastNameRequest.Iv')
  - [LastName](#P-Nullafi-Domains-StaticVault-Managers-LastName-LastNameRequest-LastName 'Nullafi.Domains.StaticVault.Managers.LastName.LastNameRequest.LastName')
  - [LastNameHash](#P-Nullafi-Domains-StaticVault-Managers-LastName-LastNameRequest-LastNameHash 'Nullafi.Domains.StaticVault.Managers.LastName.LastNameRequest.LastNameHash')
  - [Tags](#P-Nullafi-Domains-StaticVault-Managers-LastName-LastNameRequest-Tags 'Nullafi.Domains.StaticVault.Managers.LastName.LastNameRequest.Tags')
- [LastNameResponse](#T-Nullafi-Domains-StaticVault-Managers-LastName-LastNameResponse 'Nullafi.Domains.StaticVault.Managers.LastName.LastNameResponse')
  - [AuthTag](#P-Nullafi-Domains-StaticVault-Managers-LastName-LastNameResponse-AuthTag 'Nullafi.Domains.StaticVault.Managers.LastName.LastNameResponse.AuthTag')
  - [CreatedAt](#P-Nullafi-Domains-StaticVault-Managers-LastName-LastNameResponse-CreatedAt 'Nullafi.Domains.StaticVault.Managers.LastName.LastNameResponse.CreatedAt')
  - [Id](#P-Nullafi-Domains-StaticVault-Managers-LastName-LastNameResponse-Id 'Nullafi.Domains.StaticVault.Managers.LastName.LastNameResponse.Id')
  - [Iv](#P-Nullafi-Domains-StaticVault-Managers-LastName-LastNameResponse-Iv 'Nullafi.Domains.StaticVault.Managers.LastName.LastNameResponse.Iv')
  - [LastName](#P-Nullafi-Domains-StaticVault-Managers-LastName-LastNameResponse-LastName 'Nullafi.Domains.StaticVault.Managers.LastName.LastNameResponse.LastName')
  - [LastNameAlias](#P-Nullafi-Domains-StaticVault-Managers-LastName-LastNameResponse-LastNameAlias 'Nullafi.Domains.StaticVault.Managers.LastName.LastNameResponse.LastNameAlias')
  - [Tags](#P-Nullafi-Domains-StaticVault-Managers-LastName-LastNameResponse-Tags 'Nullafi.Domains.StaticVault.Managers.LastName.LastNameResponse.Tags')
  - [UpdatedAt](#P-Nullafi-Domains-StaticVault-Managers-LastName-LastNameResponse-UpdatedAt 'Nullafi.Domains.StaticVault.Managers.LastName.LastNameResponse.UpdatedAt')
- [NullafiSDK](#T-Nullafi-NullafiSDK 'Nullafi.NullafiSDK')
  - [#ctor(apiKey)](#M-Nullafi-NullafiSDK-#ctor-System-String- 'Nullafi.NullafiSDK.#ctor(System.String)')
  - [CreateClient()](#M-Nullafi-NullafiSDK-CreateClient 'Nullafi.NullafiSDK.CreateClient')
- [PassportManager](#T-Nullafi-Domains-StaticVault-Managers-Passport-PassportManager 'Nullafi.Domains.StaticVault.Managers.Passport.PassportManager')
  - [#ctor(vault)](#M-Nullafi-Domains-StaticVault-Managers-Passport-PassportManager-#ctor-Nullafi-Domains-StaticVault-StaticVault- 'Nullafi.Domains.StaticVault.Managers.Passport.PassportManager.#ctor(Nullafi.Domains.StaticVault.StaticVault)')
  - [Create(passport,tags)](#M-Nullafi-Domains-StaticVault-Managers-Passport-PassportManager-Create-System-String,System-Collections-Generic-List{System-String}- 'Nullafi.Domains.StaticVault.Managers.Passport.PassportManager.Create(System.String,System.Collections.Generic.List{System.String})')
  - [Delete(aliasId)](#M-Nullafi-Domains-StaticVault-Managers-Passport-PassportManager-Delete-System-String- 'Nullafi.Domains.StaticVault.Managers.Passport.PassportManager.Delete(System.String)')
  - [Retrieve(aliasId)](#M-Nullafi-Domains-StaticVault-Managers-Passport-PassportManager-Retrieve-System-String- 'Nullafi.Domains.StaticVault.Managers.Passport.PassportManager.Retrieve(System.String)')
- [PassportRequest](#T-Nullafi-Domains-StaticVault-Managers-Passport-PassportRequest 'Nullafi.Domains.StaticVault.Managers.Passport.PassportRequest')
  - [AuthTag](#P-Nullafi-Domains-StaticVault-Managers-Passport-PassportRequest-AuthTag 'Nullafi.Domains.StaticVault.Managers.Passport.PassportRequest.AuthTag')
  - [Iv](#P-Nullafi-Domains-StaticVault-Managers-Passport-PassportRequest-Iv 'Nullafi.Domains.StaticVault.Managers.Passport.PassportRequest.Iv')
  - [Passport](#P-Nullafi-Domains-StaticVault-Managers-Passport-PassportRequest-Passport 'Nullafi.Domains.StaticVault.Managers.Passport.PassportRequest.Passport')
  - [PassportHash](#P-Nullafi-Domains-StaticVault-Managers-Passport-PassportRequest-PassportHash 'Nullafi.Domains.StaticVault.Managers.Passport.PassportRequest.PassportHash')
  - [Tags](#P-Nullafi-Domains-StaticVault-Managers-Passport-PassportRequest-Tags 'Nullafi.Domains.StaticVault.Managers.Passport.PassportRequest.Tags')
- [PassportResponse](#T-Nullafi-Domains-StaticVault-Managers-Passport-PassportResponse 'Nullafi.Domains.StaticVault.Managers.Passport.PassportResponse')
  - [AuthTag](#P-Nullafi-Domains-StaticVault-Managers-Passport-PassportResponse-AuthTag 'Nullafi.Domains.StaticVault.Managers.Passport.PassportResponse.AuthTag')
  - [CreatedAt](#P-Nullafi-Domains-StaticVault-Managers-Passport-PassportResponse-CreatedAt 'Nullafi.Domains.StaticVault.Managers.Passport.PassportResponse.CreatedAt')
  - [Id](#P-Nullafi-Domains-StaticVault-Managers-Passport-PassportResponse-Id 'Nullafi.Domains.StaticVault.Managers.Passport.PassportResponse.Id')
  - [Iv](#P-Nullafi-Domains-StaticVault-Managers-Passport-PassportResponse-Iv 'Nullafi.Domains.StaticVault.Managers.Passport.PassportResponse.Iv')
  - [Passport](#P-Nullafi-Domains-StaticVault-Managers-Passport-PassportResponse-Passport 'Nullafi.Domains.StaticVault.Managers.Passport.PassportResponse.Passport')
  - [PassportAlias](#P-Nullafi-Domains-StaticVault-Managers-Passport-PassportResponse-PassportAlias 'Nullafi.Domains.StaticVault.Managers.Passport.PassportResponse.PassportAlias')
  - [Tags](#P-Nullafi-Domains-StaticVault-Managers-Passport-PassportResponse-Tags 'Nullafi.Domains.StaticVault.Managers.Passport.PassportResponse.Tags')
  - [UpdatedAt](#P-Nullafi-Domains-StaticVault-Managers-Passport-PassportResponse-UpdatedAt 'Nullafi.Domains.StaticVault.Managers.Passport.PassportResponse.UpdatedAt')
- [PlaceOfBirthManager](#T-Nullafi-Domains-StaticVault-Managers-PlaceOfBirth-PlaceOfBirthManager 'Nullafi.Domains.StaticVault.Managers.PlaceOfBirth.PlaceOfBirthManager')
  - [#ctor(vault)](#M-Nullafi-Domains-StaticVault-Managers-PlaceOfBirth-PlaceOfBirthManager-#ctor-Nullafi-Domains-StaticVault-StaticVault- 'Nullafi.Domains.StaticVault.Managers.PlaceOfBirth.PlaceOfBirthManager.#ctor(Nullafi.Domains.StaticVault.StaticVault)')
  - [Create(placeofbirth,tags)](#M-Nullafi-Domains-StaticVault-Managers-PlaceOfBirth-PlaceOfBirthManager-Create-System-String,System-Collections-Generic-List{System-String}- 'Nullafi.Domains.StaticVault.Managers.PlaceOfBirth.PlaceOfBirthManager.Create(System.String,System.Collections.Generic.List{System.String})')
  - [Create(placeofbirth,state,tags)](#M-Nullafi-Domains-StaticVault-Managers-PlaceOfBirth-PlaceOfBirthManager-Create-System-String,System-String,System-Collections-Generic-List{System-String}- 'Nullafi.Domains.StaticVault.Managers.PlaceOfBirth.PlaceOfBirthManager.Create(System.String,System.String,System.Collections.Generic.List{System.String})')
  - [Delete(aliasId)](#M-Nullafi-Domains-StaticVault-Managers-PlaceOfBirth-PlaceOfBirthManager-Delete-System-String- 'Nullafi.Domains.StaticVault.Managers.PlaceOfBirth.PlaceOfBirthManager.Delete(System.String)')
  - [Retrieve(aliasId)](#M-Nullafi-Domains-StaticVault-Managers-PlaceOfBirth-PlaceOfBirthManager-Retrieve-System-String- 'Nullafi.Domains.StaticVault.Managers.PlaceOfBirth.PlaceOfBirthManager.Retrieve(System.String)')
- [PlaceOfBirthRequest](#T-Nullafi-Domains-StaticVault-Managers-PlaceOfBirth-PlaceOfBirthRequest 'Nullafi.Domains.StaticVault.Managers.PlaceOfBirth.PlaceOfBirthRequest')
  - [AuthTag](#P-Nullafi-Domains-StaticVault-Managers-PlaceOfBirth-PlaceOfBirthRequest-AuthTag 'Nullafi.Domains.StaticVault.Managers.PlaceOfBirth.PlaceOfBirthRequest.AuthTag')
  - [Iv](#P-Nullafi-Domains-StaticVault-Managers-PlaceOfBirth-PlaceOfBirthRequest-Iv 'Nullafi.Domains.StaticVault.Managers.PlaceOfBirth.PlaceOfBirthRequest.Iv')
  - [PlaceOfBirth](#P-Nullafi-Domains-StaticVault-Managers-PlaceOfBirth-PlaceOfBirthRequest-PlaceOfBirth 'Nullafi.Domains.StaticVault.Managers.PlaceOfBirth.PlaceOfBirthRequest.PlaceOfBirth')
  - [PlaceOfBirthHash](#P-Nullafi-Domains-StaticVault-Managers-PlaceOfBirth-PlaceOfBirthRequest-PlaceOfBirthHash 'Nullafi.Domains.StaticVault.Managers.PlaceOfBirth.PlaceOfBirthRequest.PlaceOfBirthHash')
  - [Tags](#P-Nullafi-Domains-StaticVault-Managers-PlaceOfBirth-PlaceOfBirthRequest-Tags 'Nullafi.Domains.StaticVault.Managers.PlaceOfBirth.PlaceOfBirthRequest.Tags')
- [PlaceOfBirthResponse](#T-Nullafi-Domains-StaticVault-Managers-PlaceOfBirth-PlaceOfBirthResponse 'Nullafi.Domains.StaticVault.Managers.PlaceOfBirth.PlaceOfBirthResponse')
  - [AuthTag](#P-Nullafi-Domains-StaticVault-Managers-PlaceOfBirth-PlaceOfBirthResponse-AuthTag 'Nullafi.Domains.StaticVault.Managers.PlaceOfBirth.PlaceOfBirthResponse.AuthTag')
  - [CreatedAt](#P-Nullafi-Domains-StaticVault-Managers-PlaceOfBirth-PlaceOfBirthResponse-CreatedAt 'Nullafi.Domains.StaticVault.Managers.PlaceOfBirth.PlaceOfBirthResponse.CreatedAt')
  - [Id](#P-Nullafi-Domains-StaticVault-Managers-PlaceOfBirth-PlaceOfBirthResponse-Id 'Nullafi.Domains.StaticVault.Managers.PlaceOfBirth.PlaceOfBirthResponse.Id')
  - [Iv](#P-Nullafi-Domains-StaticVault-Managers-PlaceOfBirth-PlaceOfBirthResponse-Iv 'Nullafi.Domains.StaticVault.Managers.PlaceOfBirth.PlaceOfBirthResponse.Iv')
  - [PlaceOfBirth](#P-Nullafi-Domains-StaticVault-Managers-PlaceOfBirth-PlaceOfBirthResponse-PlaceOfBirth 'Nullafi.Domains.StaticVault.Managers.PlaceOfBirth.PlaceOfBirthResponse.PlaceOfBirth')
  - [PlaceOfBirthAlias](#P-Nullafi-Domains-StaticVault-Managers-PlaceOfBirth-PlaceOfBirthResponse-PlaceOfBirthAlias 'Nullafi.Domains.StaticVault.Managers.PlaceOfBirth.PlaceOfBirthResponse.PlaceOfBirthAlias')
  - [Tags](#P-Nullafi-Domains-StaticVault-Managers-PlaceOfBirth-PlaceOfBirthResponse-Tags 'Nullafi.Domains.StaticVault.Managers.PlaceOfBirth.PlaceOfBirthResponse.Tags')
  - [UpdatedAt](#P-Nullafi-Domains-StaticVault-Managers-PlaceOfBirth-PlaceOfBirthResponse-UpdatedAt 'Nullafi.Domains.StaticVault.Managers.PlaceOfBirth.PlaceOfBirthResponse.UpdatedAt')
- [RSA](#T-Nullafi-Services-Crypto-RSA 'Nullafi.Services.Crypto.RSA')
  - [RSAGenerateManager()](#M-Nullafi-Services-Crypto-RSA-RSAGenerateManager 'Nullafi.Services.Crypto.RSA.RSAGenerateManager')
- [RSAManager](#T-Nullafi-Services-Crypto-RSAManager 'Nullafi.Services.Crypto.RSAManager')
  - [Decrypt](#P-Nullafi-Services-Crypto-RSAManager-Decrypt 'Nullafi.Services.Crypto.RSAManager.Decrypt')
  - [PublicKey](#P-Nullafi-Services-Crypto-RSAManager-PublicKey 'Nullafi.Services.Crypto.RSAManager.PublicKey')
- [RaceManager](#T-Nullafi-Domains-StaticVault-Managers-Race-RaceManager 'Nullafi.Domains.StaticVault.Managers.Race.RaceManager')
  - [#ctor(vault)](#M-Nullafi-Domains-StaticVault-Managers-Race-RaceManager-#ctor-Nullafi-Domains-StaticVault-StaticVault- 'Nullafi.Domains.StaticVault.Managers.Race.RaceManager.#ctor(Nullafi.Domains.StaticVault.StaticVault)')
  - [Create(race,tags)](#M-Nullafi-Domains-StaticVault-Managers-Race-RaceManager-Create-System-String,System-Collections-Generic-List{System-String}- 'Nullafi.Domains.StaticVault.Managers.Race.RaceManager.Create(System.String,System.Collections.Generic.List{System.String})')
  - [Delete(aliasId)](#M-Nullafi-Domains-StaticVault-Managers-Race-RaceManager-Delete-System-String- 'Nullafi.Domains.StaticVault.Managers.Race.RaceManager.Delete(System.String)')
  - [Retrieve(aliasId)](#M-Nullafi-Domains-StaticVault-Managers-Race-RaceManager-Retrieve-System-String- 'Nullafi.Domains.StaticVault.Managers.Race.RaceManager.Retrieve(System.String)')
- [RaceRequest](#T-Nullafi-Domains-StaticVault-Managers-Race-RaceRequest 'Nullafi.Domains.StaticVault.Managers.Race.RaceRequest')
  - [AuthTag](#P-Nullafi-Domains-StaticVault-Managers-Race-RaceRequest-AuthTag 'Nullafi.Domains.StaticVault.Managers.Race.RaceRequest.AuthTag')
  - [Iv](#P-Nullafi-Domains-StaticVault-Managers-Race-RaceRequest-Iv 'Nullafi.Domains.StaticVault.Managers.Race.RaceRequest.Iv')
  - [Race](#P-Nullafi-Domains-StaticVault-Managers-Race-RaceRequest-Race 'Nullafi.Domains.StaticVault.Managers.Race.RaceRequest.Race')
  - [RaceHash](#P-Nullafi-Domains-StaticVault-Managers-Race-RaceRequest-RaceHash 'Nullafi.Domains.StaticVault.Managers.Race.RaceRequest.RaceHash')
  - [Tags](#P-Nullafi-Domains-StaticVault-Managers-Race-RaceRequest-Tags 'Nullafi.Domains.StaticVault.Managers.Race.RaceRequest.Tags')
- [RaceResponse](#T-Nullafi-Domains-StaticVault-Managers-Race-RaceResponse 'Nullafi.Domains.StaticVault.Managers.Race.RaceResponse')
  - [AuthTag](#P-Nullafi-Domains-StaticVault-Managers-Race-RaceResponse-AuthTag 'Nullafi.Domains.StaticVault.Managers.Race.RaceResponse.AuthTag')
  - [CreatedAt](#P-Nullafi-Domains-StaticVault-Managers-Race-RaceResponse-CreatedAt 'Nullafi.Domains.StaticVault.Managers.Race.RaceResponse.CreatedAt')
  - [Id](#P-Nullafi-Domains-StaticVault-Managers-Race-RaceResponse-Id 'Nullafi.Domains.StaticVault.Managers.Race.RaceResponse.Id')
  - [Iv](#P-Nullafi-Domains-StaticVault-Managers-Race-RaceResponse-Iv 'Nullafi.Domains.StaticVault.Managers.Race.RaceResponse.Iv')
  - [Race](#P-Nullafi-Domains-StaticVault-Managers-Race-RaceResponse-Race 'Nullafi.Domains.StaticVault.Managers.Race.RaceResponse.Race')
  - [RaceAlias](#P-Nullafi-Domains-StaticVault-Managers-Race-RaceResponse-RaceAlias 'Nullafi.Domains.StaticVault.Managers.Race.RaceResponse.RaceAlias')
  - [Tags](#P-Nullafi-Domains-StaticVault-Managers-Race-RaceResponse-Tags 'Nullafi.Domains.StaticVault.Managers.Race.RaceResponse.Tags')
  - [UpdatedAt](#P-Nullafi-Domains-StaticVault-Managers-Race-RaceResponse-UpdatedAt 'Nullafi.Domains.StaticVault.Managers.Race.RaceResponse.UpdatedAt')
- [RandomManager](#T-Nullafi-Domains-StaticVault-Managers-Random-RandomManager 'Nullafi.Domains.StaticVault.Managers.Random.RandomManager')
  - [#ctor(vault)](#M-Nullafi-Domains-StaticVault-Managers-Random-RandomManager-#ctor-Nullafi-Domains-StaticVault-StaticVault- 'Nullafi.Domains.StaticVault.Managers.Random.RandomManager.#ctor(Nullafi.Domains.StaticVault.StaticVault)')
  - [Create(data,tags)](#M-Nullafi-Domains-StaticVault-Managers-Random-RandomManager-Create-System-String,System-Collections-Generic-List{System-String}- 'Nullafi.Domains.StaticVault.Managers.Random.RandomManager.Create(System.String,System.Collections.Generic.List{System.String})')
  - [Delete(aliasId)](#M-Nullafi-Domains-StaticVault-Managers-Random-RandomManager-Delete-System-String- 'Nullafi.Domains.StaticVault.Managers.Random.RandomManager.Delete(System.String)')
  - [Retrieve(aliasId)](#M-Nullafi-Domains-StaticVault-Managers-Random-RandomManager-Retrieve-System-String- 'Nullafi.Domains.StaticVault.Managers.Random.RandomManager.Retrieve(System.String)')
- [RandomRequest](#T-Nullafi-Domains-StaticVault-Managers-Random-RandomRequest 'Nullafi.Domains.StaticVault.Managers.Random.RandomRequest')
  - [AuthTag](#P-Nullafi-Domains-StaticVault-Managers-Random-RandomRequest-AuthTag 'Nullafi.Domains.StaticVault.Managers.Random.RandomRequest.AuthTag')
  - [Data](#P-Nullafi-Domains-StaticVault-Managers-Random-RandomRequest-Data 'Nullafi.Domains.StaticVault.Managers.Random.RandomRequest.Data')
  - [DataHash](#P-Nullafi-Domains-StaticVault-Managers-Random-RandomRequest-DataHash 'Nullafi.Domains.StaticVault.Managers.Random.RandomRequest.DataHash')
  - [Iv](#P-Nullafi-Domains-StaticVault-Managers-Random-RandomRequest-Iv 'Nullafi.Domains.StaticVault.Managers.Random.RandomRequest.Iv')
  - [Tags](#P-Nullafi-Domains-StaticVault-Managers-Random-RandomRequest-Tags 'Nullafi.Domains.StaticVault.Managers.Random.RandomRequest.Tags')
- [RandomResponse](#T-Nullafi-Domains-StaticVault-Managers-Random-RandomResponse 'Nullafi.Domains.StaticVault.Managers.Random.RandomResponse')
  - [Alias](#P-Nullafi-Domains-StaticVault-Managers-Random-RandomResponse-Alias 'Nullafi.Domains.StaticVault.Managers.Random.RandomResponse.Alias')
  - [AuthTag](#P-Nullafi-Domains-StaticVault-Managers-Random-RandomResponse-AuthTag 'Nullafi.Domains.StaticVault.Managers.Random.RandomResponse.AuthTag')
  - [CreatedAt](#P-Nullafi-Domains-StaticVault-Managers-Random-RandomResponse-CreatedAt 'Nullafi.Domains.StaticVault.Managers.Random.RandomResponse.CreatedAt')
  - [Data](#P-Nullafi-Domains-StaticVault-Managers-Random-RandomResponse-Data 'Nullafi.Domains.StaticVault.Managers.Random.RandomResponse.Data')
  - [Id](#P-Nullafi-Domains-StaticVault-Managers-Random-RandomResponse-Id 'Nullafi.Domains.StaticVault.Managers.Random.RandomResponse.Id')
  - [Iv](#P-Nullafi-Domains-StaticVault-Managers-Random-RandomResponse-Iv 'Nullafi.Domains.StaticVault.Managers.Random.RandomResponse.Iv')
  - [Tags](#P-Nullafi-Domains-StaticVault-Managers-Random-RandomResponse-Tags 'Nullafi.Domains.StaticVault.Managers.Random.RandomResponse.Tags')
  - [UpdatedAt](#P-Nullafi-Domains-StaticVault-Managers-Random-RandomResponse-UpdatedAt 'Nullafi.Domains.StaticVault.Managers.Random.RandomResponse.UpdatedAt')
- [Security](#T-Nullafi-Security 'Nullafi.Security')
  - [#ctor()](#M-Nullafi-Security-#ctor 'Nullafi.Security.#ctor')
  - [Aes](#P-Nullafi-Security-Aes 'Nullafi.Security.Aes')
  - [Hmac](#P-Nullafi-Security-Hmac 'Nullafi.Security.Hmac')
  - [RSA](#P-Nullafi-Security-RSA 'Nullafi.Security.RSA')
- [SsnManager](#T-Nullafi-Domains-StaticVault-Managers-Ssn-SsnManager 'Nullafi.Domains.StaticVault.Managers.Ssn.SsnManager')
  - [#ctor(vault)](#M-Nullafi-Domains-StaticVault-Managers-Ssn-SsnManager-#ctor-Nullafi-Domains-StaticVault-StaticVault- 'Nullafi.Domains.StaticVault.Managers.Ssn.SsnManager.#ctor(Nullafi.Domains.StaticVault.StaticVault)')
  - [Create(ssn,tags)](#M-Nullafi-Domains-StaticVault-Managers-Ssn-SsnManager-Create-System-String,System-Collections-Generic-List{System-String}- 'Nullafi.Domains.StaticVault.Managers.Ssn.SsnManager.Create(System.String,System.Collections.Generic.List{System.String})')
  - [Create(ssn,state,tags)](#M-Nullafi-Domains-StaticVault-Managers-Ssn-SsnManager-Create-System-String,System-String,System-Collections-Generic-List{System-String}- 'Nullafi.Domains.StaticVault.Managers.Ssn.SsnManager.Create(System.String,System.String,System.Collections.Generic.List{System.String})')
  - [Delete(aliasId)](#M-Nullafi-Domains-StaticVault-Managers-Ssn-SsnManager-Delete-System-String- 'Nullafi.Domains.StaticVault.Managers.Ssn.SsnManager.Delete(System.String)')
  - [Retrieve(aliasId)](#M-Nullafi-Domains-StaticVault-Managers-Ssn-SsnManager-Retrieve-System-String- 'Nullafi.Domains.StaticVault.Managers.Ssn.SsnManager.Retrieve(System.String)')
- [SsnRequest](#T-Nullafi-Domains-StaticVault-Managers-Ssn-SsnRequest 'Nullafi.Domains.StaticVault.Managers.Ssn.SsnRequest')
  - [AuthTag](#P-Nullafi-Domains-StaticVault-Managers-Ssn-SsnRequest-AuthTag 'Nullafi.Domains.StaticVault.Managers.Ssn.SsnRequest.AuthTag')
  - [Iv](#P-Nullafi-Domains-StaticVault-Managers-Ssn-SsnRequest-Iv 'Nullafi.Domains.StaticVault.Managers.Ssn.SsnRequest.Iv')
  - [Ssn](#P-Nullafi-Domains-StaticVault-Managers-Ssn-SsnRequest-Ssn 'Nullafi.Domains.StaticVault.Managers.Ssn.SsnRequest.Ssn')
  - [SsnHash](#P-Nullafi-Domains-StaticVault-Managers-Ssn-SsnRequest-SsnHash 'Nullafi.Domains.StaticVault.Managers.Ssn.SsnRequest.SsnHash')
  - [Tags](#P-Nullafi-Domains-StaticVault-Managers-Ssn-SsnRequest-Tags 'Nullafi.Domains.StaticVault.Managers.Ssn.SsnRequest.Tags')
- [SsnResponse](#T-Nullafi-Domains-StaticVault-Managers-Ssn-SsnResponse 'Nullafi.Domains.StaticVault.Managers.Ssn.SsnResponse')
  - [AuthTag](#P-Nullafi-Domains-StaticVault-Managers-Ssn-SsnResponse-AuthTag 'Nullafi.Domains.StaticVault.Managers.Ssn.SsnResponse.AuthTag')
  - [CreatedAt](#P-Nullafi-Domains-StaticVault-Managers-Ssn-SsnResponse-CreatedAt 'Nullafi.Domains.StaticVault.Managers.Ssn.SsnResponse.CreatedAt')
  - [Id](#P-Nullafi-Domains-StaticVault-Managers-Ssn-SsnResponse-Id 'Nullafi.Domains.StaticVault.Managers.Ssn.SsnResponse.Id')
  - [Iv](#P-Nullafi-Domains-StaticVault-Managers-Ssn-SsnResponse-Iv 'Nullafi.Domains.StaticVault.Managers.Ssn.SsnResponse.Iv')
  - [Ssn](#P-Nullafi-Domains-StaticVault-Managers-Ssn-SsnResponse-Ssn 'Nullafi.Domains.StaticVault.Managers.Ssn.SsnResponse.Ssn')
  - [SsnAlias](#P-Nullafi-Domains-StaticVault-Managers-Ssn-SsnResponse-SsnAlias 'Nullafi.Domains.StaticVault.Managers.Ssn.SsnResponse.SsnAlias')
  - [Tags](#P-Nullafi-Domains-StaticVault-Managers-Ssn-SsnResponse-Tags 'Nullafi.Domains.StaticVault.Managers.Ssn.SsnResponse.Tags')
  - [UpdatedAt](#P-Nullafi-Domains-StaticVault-Managers-Ssn-SsnResponse-UpdatedAt 'Nullafi.Domains.StaticVault.Managers.Ssn.SsnResponse.UpdatedAt')
- [StaticVault](#T-Nullafi-Domains-StaticVault-StaticVault 'Nullafi.Domains.StaticVault.StaticVault')
  - [#ctor(client,vaultId,vaultName,masterKey)](#M-Nullafi-Domains-StaticVault-StaticVault-#ctor-Nullafi-Client,System-String,System-String,System-String- 'Nullafi.Domains.StaticVault.StaticVault.#ctor(Nullafi.Client,System.String,System.String,System.String)')
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
  - [Hash(value)](#M-Nullafi-Domains-StaticVault-StaticVault-Hash-System-String- 'Nullafi.Domains.StaticVault.StaticVault.Hash(System.String)')
  - [RetrieveStaticVault(client,vaultId,masterKey)](#M-Nullafi-Domains-StaticVault-StaticVault-RetrieveStaticVault-Nullafi-Client,System-String,System-String- 'Nullafi.Domains.StaticVault.StaticVault.RetrieveStaticVault(Nullafi.Client,System.String,System.String)')
- [TaxPayerManager](#T-Nullafi-Domains-StaticVault-Managers-TaxPayer-TaxPayerManager 'Nullafi.Domains.StaticVault.Managers.TaxPayer.TaxPayerManager')
  - [#ctor(vault)](#M-Nullafi-Domains-StaticVault-Managers-TaxPayer-TaxPayerManager-#ctor-Nullafi-Domains-StaticVault-StaticVault- 'Nullafi.Domains.StaticVault.Managers.TaxPayer.TaxPayerManager.#ctor(Nullafi.Domains.StaticVault.StaticVault)')
  - [Create(taxpayer,tags)](#M-Nullafi-Domains-StaticVault-Managers-TaxPayer-TaxPayerManager-Create-System-String,System-Collections-Generic-List{System-String}- 'Nullafi.Domains.StaticVault.Managers.TaxPayer.TaxPayerManager.Create(System.String,System.Collections.Generic.List{System.String})')
  - [Delete(aliasId)](#M-Nullafi-Domains-StaticVault-Managers-TaxPayer-TaxPayerManager-Delete-System-String- 'Nullafi.Domains.StaticVault.Managers.TaxPayer.TaxPayerManager.Delete(System.String)')
  - [Retrieve(aliasId)](#M-Nullafi-Domains-StaticVault-Managers-TaxPayer-TaxPayerManager-Retrieve-System-String- 'Nullafi.Domains.StaticVault.Managers.TaxPayer.TaxPayerManager.Retrieve(System.String)')
- [TaxPayerRequest](#T-Nullafi-Domains-StaticVault-Managers-TaxPayer-TaxPayerRequest 'Nullafi.Domains.StaticVault.Managers.TaxPayer.TaxPayerRequest')
  - [AuthTag](#P-Nullafi-Domains-StaticVault-Managers-TaxPayer-TaxPayerRequest-AuthTag 'Nullafi.Domains.StaticVault.Managers.TaxPayer.TaxPayerRequest.AuthTag')
  - [Iv](#P-Nullafi-Domains-StaticVault-Managers-TaxPayer-TaxPayerRequest-Iv 'Nullafi.Domains.StaticVault.Managers.TaxPayer.TaxPayerRequest.Iv')
  - [Tags](#P-Nullafi-Domains-StaticVault-Managers-TaxPayer-TaxPayerRequest-Tags 'Nullafi.Domains.StaticVault.Managers.TaxPayer.TaxPayerRequest.Tags')
  - [TaxPayer](#P-Nullafi-Domains-StaticVault-Managers-TaxPayer-TaxPayerRequest-TaxPayer 'Nullafi.Domains.StaticVault.Managers.TaxPayer.TaxPayerRequest.TaxPayer')
  - [TaxPayerHash](#P-Nullafi-Domains-StaticVault-Managers-TaxPayer-TaxPayerRequest-TaxPayerHash 'Nullafi.Domains.StaticVault.Managers.TaxPayer.TaxPayerRequest.TaxPayerHash')
- [TaxPayerResponse](#T-Nullafi-Domains-StaticVault-Managers-TaxPayer-TaxPayerResponse 'Nullafi.Domains.StaticVault.Managers.TaxPayer.TaxPayerResponse')
  - [AuthTag](#P-Nullafi-Domains-StaticVault-Managers-TaxPayer-TaxPayerResponse-AuthTag 'Nullafi.Domains.StaticVault.Managers.TaxPayer.TaxPayerResponse.AuthTag')
  - [CreatedAt](#P-Nullafi-Domains-StaticVault-Managers-TaxPayer-TaxPayerResponse-CreatedAt 'Nullafi.Domains.StaticVault.Managers.TaxPayer.TaxPayerResponse.CreatedAt')
  - [Id](#P-Nullafi-Domains-StaticVault-Managers-TaxPayer-TaxPayerResponse-Id 'Nullafi.Domains.StaticVault.Managers.TaxPayer.TaxPayerResponse.Id')
  - [Iv](#P-Nullafi-Domains-StaticVault-Managers-TaxPayer-TaxPayerResponse-Iv 'Nullafi.Domains.StaticVault.Managers.TaxPayer.TaxPayerResponse.Iv')
  - [Tags](#P-Nullafi-Domains-StaticVault-Managers-TaxPayer-TaxPayerResponse-Tags 'Nullafi.Domains.StaticVault.Managers.TaxPayer.TaxPayerResponse.Tags')
  - [TaxPayer](#P-Nullafi-Domains-StaticVault-Managers-TaxPayer-TaxPayerResponse-TaxPayer 'Nullafi.Domains.StaticVault.Managers.TaxPayer.TaxPayerResponse.TaxPayer')
  - [TaxPayerAlias](#P-Nullafi-Domains-StaticVault-Managers-TaxPayer-TaxPayerResponse-TaxPayerAlias 'Nullafi.Domains.StaticVault.Managers.TaxPayer.TaxPayerResponse.TaxPayerAlias')
  - [UpdatedAt](#P-Nullafi-Domains-StaticVault-Managers-TaxPayer-TaxPayerResponse-UpdatedAt 'Nullafi.Domains.StaticVault.Managers.TaxPayer.TaxPayerResponse.UpdatedAt')
- [VehicleRegistrationManager](#T-Nullafi-Domains-StaticVault-Managers-VehicleRegistration-VehicleRegistrationManager 'Nullafi.Domains.StaticVault.Managers.VehicleRegistration.VehicleRegistrationManager')
  - [#ctor(vault)](#M-Nullafi-Domains-StaticVault-Managers-VehicleRegistration-VehicleRegistrationManager-#ctor-Nullafi-Domains-StaticVault-StaticVault- 'Nullafi.Domains.StaticVault.Managers.VehicleRegistration.VehicleRegistrationManager.#ctor(Nullafi.Domains.StaticVault.StaticVault)')
  - [Create(vehicleregistration,tags)](#M-Nullafi-Domains-StaticVault-Managers-VehicleRegistration-VehicleRegistrationManager-Create-System-String,System-Collections-Generic-List{System-String}- 'Nullafi.Domains.StaticVault.Managers.VehicleRegistration.VehicleRegistrationManager.Create(System.String,System.Collections.Generic.List{System.String})')
  - [Delete(aliasId)](#M-Nullafi-Domains-StaticVault-Managers-VehicleRegistration-VehicleRegistrationManager-Delete-System-String- 'Nullafi.Domains.StaticVault.Managers.VehicleRegistration.VehicleRegistrationManager.Delete(System.String)')
  - [Retrieve(aliasId)](#M-Nullafi-Domains-StaticVault-Managers-VehicleRegistration-VehicleRegistrationManager-Retrieve-System-String- 'Nullafi.Domains.StaticVault.Managers.VehicleRegistration.VehicleRegistrationManager.Retrieve(System.String)')
- [VehicleRegistrationRequest](#T-Nullafi-Domains-StaticVault-Managers-VehicleRegistration-VehicleRegistrationRequest 'Nullafi.Domains.StaticVault.Managers.VehicleRegistration.VehicleRegistrationRequest')
  - [AuthTag](#P-Nullafi-Domains-StaticVault-Managers-VehicleRegistration-VehicleRegistrationRequest-AuthTag 'Nullafi.Domains.StaticVault.Managers.VehicleRegistration.VehicleRegistrationRequest.AuthTag')
  - [Iv](#P-Nullafi-Domains-StaticVault-Managers-VehicleRegistration-VehicleRegistrationRequest-Iv 'Nullafi.Domains.StaticVault.Managers.VehicleRegistration.VehicleRegistrationRequest.Iv')
  - [Tags](#P-Nullafi-Domains-StaticVault-Managers-VehicleRegistration-VehicleRegistrationRequest-Tags 'Nullafi.Domains.StaticVault.Managers.VehicleRegistration.VehicleRegistrationRequest.Tags')
  - [VehicleRegistration](#P-Nullafi-Domains-StaticVault-Managers-VehicleRegistration-VehicleRegistrationRequest-VehicleRegistration 'Nullafi.Domains.StaticVault.Managers.VehicleRegistration.VehicleRegistrationRequest.VehicleRegistration')
  - [VehicleRegistrationHash](#P-Nullafi-Domains-StaticVault-Managers-VehicleRegistration-VehicleRegistrationRequest-VehicleRegistrationHash 'Nullafi.Domains.StaticVault.Managers.VehicleRegistration.VehicleRegistrationRequest.VehicleRegistrationHash')
- [VehicleRegistrationResponse](#T-Nullafi-Domains-StaticVault-Managers-VehicleRegistration-VehicleRegistrationResponse 'Nullafi.Domains.StaticVault.Managers.VehicleRegistration.VehicleRegistrationResponse')
  - [AuthTag](#P-Nullafi-Domains-StaticVault-Managers-VehicleRegistration-VehicleRegistrationResponse-AuthTag 'Nullafi.Domains.StaticVault.Managers.VehicleRegistration.VehicleRegistrationResponse.AuthTag')
  - [CreatedAt](#P-Nullafi-Domains-StaticVault-Managers-VehicleRegistration-VehicleRegistrationResponse-CreatedAt 'Nullafi.Domains.StaticVault.Managers.VehicleRegistration.VehicleRegistrationResponse.CreatedAt')
  - [Id](#P-Nullafi-Domains-StaticVault-Managers-VehicleRegistration-VehicleRegistrationResponse-Id 'Nullafi.Domains.StaticVault.Managers.VehicleRegistration.VehicleRegistrationResponse.Id')
  - [Iv](#P-Nullafi-Domains-StaticVault-Managers-VehicleRegistration-VehicleRegistrationResponse-Iv 'Nullafi.Domains.StaticVault.Managers.VehicleRegistration.VehicleRegistrationResponse.Iv')
  - [Tags](#P-Nullafi-Domains-StaticVault-Managers-VehicleRegistration-VehicleRegistrationResponse-Tags 'Nullafi.Domains.StaticVault.Managers.VehicleRegistration.VehicleRegistrationResponse.Tags')
  - [UpdatedAt](#P-Nullafi-Domains-StaticVault-Managers-VehicleRegistration-VehicleRegistrationResponse-UpdatedAt 'Nullafi.Domains.StaticVault.Managers.VehicleRegistration.VehicleRegistrationResponse.UpdatedAt')
  - [VehicleRegistration](#P-Nullafi-Domains-StaticVault-Managers-VehicleRegistration-VehicleRegistrationResponse-VehicleRegistration 'Nullafi.Domains.StaticVault.Managers.VehicleRegistration.VehicleRegistrationResponse.VehicleRegistration')
  - [VehicleRegistrationAlias](#P-Nullafi-Domains-StaticVault-Managers-VehicleRegistration-VehicleRegistrationResponse-VehicleRegistrationAlias 'Nullafi.Domains.StaticVault.Managers.VehicleRegistration.VehicleRegistrationResponse.VehicleRegistrationAlias')

<a name='T-Nullafi-Domains-StaticVault-Managers-Address-AddressManager'></a>
## AddressManager `type`

##### Namespace

Nullafi.Domains.StaticVault.Managers.Address

##### Summary

AddressManager

<a name='M-Nullafi-Domains-StaticVault-Managers-Address-AddressManager-#ctor-Nullafi-Domains-StaticVault-StaticVault-'></a>
### #ctor(vault) `constructor`

##### Summary

Create an instance of AddressManager

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| vault | [Nullafi.Domains.StaticVault.StaticVault](#T-Nullafi-Domains-StaticVault-StaticVault 'Nullafi.Domains.StaticVault.StaticVault') |  |

<a name='M-Nullafi-Domains-StaticVault-Managers-Address-AddressManager-Create-System-String,System-Collections-Generic-List{System-String}-'></a>
### Create(address,tags) `method`

##### Summary

Create a new Address string to be aliased for static vault

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| address | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| tags | [System.Collections.Generic.List{System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{System.String}') |  |

<a name='M-Nullafi-Domains-StaticVault-Managers-Address-AddressManager-Create-System-String,System-String,System-Collections-Generic-List{System-String}-'></a>
### Create(address,state,tags) `method`

##### Summary

Create a new Address string to be aliased for static vault

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

Delete the Address alias from static vault

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| aliasId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-Nullafi-Domains-StaticVault-Managers-Address-AddressManager-Retrieve-System-String-'></a>
### Retrieve(aliasId) `method`

##### Summary

Retrieve the Address string alias from a static vault. Returns an array of matching values. Array will be sorted by date created.

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



<a name='T-Nullafi-Services-Crypto-Aesgcm'></a>
## Aesgcm `type`

##### Namespace

Nullafi.Services.Crypto

##### Summary

Aesgcm

<a name='M-Nullafi-Services-Crypto-Aesgcm-Decrypt-System-String,System-String,System-String,System-String-'></a>
### Decrypt(masterKey,iv,authTag,cipherText,returnBase64) `method`

##### Summary

Decrypt the data using AES GCM 256bit

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| masterKey | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| iv | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| authTag | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| cipherText | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-Nullafi-Services-Crypto-Aesgcm-Encrypt-System-String,System-String,System-String-'></a>
### Encrypt(masterKey,iv,plainText) `method`

##### Summary

Encrypt the data using AES GCM 256bit

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| masterKey | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| iv | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| plainText | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-Nullafi-Services-Crypto-Aesgcm-GenerateStringIv'></a>
### GenerateStringIv() `method`

##### Summary

Generate initialization vector to be used on AES encrypt/decrypt

##### Returns



##### Parameters

This method has no parameters.

<a name='M-Nullafi-Services-Crypto-Aesgcm-GenerateStringMasterKey'></a>
### GenerateStringMasterKey() `method`

##### Summary

Generate masterkey to be used on AES encrypt/decrypt

##### Returns



##### Parameters

This method has no parameters.

<a name='T-Nullafi-Services-Api'></a>
## Api `type`

##### Namespace

Nullafi.Services

##### Summary

Api

<a name='M-Nullafi-Services-Api-#ctor'></a>
### #ctor() `constructor`

##### Summary

Create an instance of Api

##### Returns



##### Parameters

This constructor has no parameters.

<a name='T-Nullafi-Client'></a>
## Client `type`

##### Namespace

Nullafi

##### Summary

Client class

<a name='P-Nullafi-Client-HashKey'></a>
### HashKey `property`

##### Summary



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

Communication Vault

<a name='M-Nullafi-Domains-CommunicationVault-CommunicationVault-#ctor-Nullafi-Client,System-String,System-String,System-String-'></a>
### #ctor(client,vaultId,vaultName,masterKey) `constructor`

##### Summary

Create an instance of CommunicationVault

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| client | [Nullafi.Client](#T-Nullafi-Client 'Nullafi.Client') |  |
| vaultId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| vaultName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| masterKey | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

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

Create the API to create a new communication vault

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| client | [Nullafi.Client](#T-Nullafi-Client 'Nullafi.Client') |  |
| name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| tags | [System.Collections.Generic.List{System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{System.String}') |  |

<a name='M-Nullafi-Domains-CommunicationVault-CommunicationVault-Hash-System-String-'></a>
### Hash(value) `method`

##### Summary

Generate a hash for the real data

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-Nullafi-Domains-CommunicationVault-CommunicationVault-RetrieveCommunicationVault-Nullafi-Client,System-String,System-String-'></a>
### RetrieveCommunicationVault(client,vaultId,masterKey) `method`

##### Summary

Retrieve the communication vault from id

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

AddressManager

<a name='M-Nullafi-Domains-StaticVault-Managers-DateOfBirth-DateOfBirthManager-#ctor-Nullafi-Domains-StaticVault-StaticVault-'></a>
### #ctor(vault) `constructor`

##### Summary

Create an instance of DateOfBirthManager

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| vault | [Nullafi.Domains.StaticVault.StaticVault](#T-Nullafi-Domains-StaticVault-StaticVault 'Nullafi.Domains.StaticVault.StaticVault') |  |

<a name='M-Nullafi-Domains-StaticVault-Managers-DateOfBirth-DateOfBirthManager-Create-System-String,System-Collections-Generic-List{System-String}-'></a>
### Create(dateOfBirth,tags) `method`

##### Summary

Create a new DateOfBirth string to be aliased within static vault

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| dateOfBirth | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| tags | [System.Collections.Generic.List{System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{System.String}') |  |

<a name='M-Nullafi-Domains-StaticVault-Managers-DateOfBirth-DateOfBirthManager-Create-System-String,System-Nullable{System-Int32},System-Nullable{System-Int32},System-Collections-Generic-List{System-String}-'></a>
### Create(dateOfBirth,year,month,tags) `method`

##### Summary

Create a new DateOfBirth string to be aliased within static vault

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

Delete the Address alias from static vault

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| aliasId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-Nullafi-Domains-StaticVault-Managers-DateOfBirth-DateOfBirthManager-Retrieve-System-String-'></a>
### Retrieve(aliasId) `method`

##### Summary

Retrieve the Address string alias from a static vault. Returns an array of matching values. Array will be sorted by date created.

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



<a name='T-Nullafi-Domains-StaticVault-Managers-DriversLicense-DriversLicenseManager'></a>
## DriversLicenseManager `type`

##### Namespace

Nullafi.Domains.StaticVault.Managers.DriversLicense

##### Summary

DateOfBirthManager

<a name='M-Nullafi-Domains-StaticVault-Managers-DriversLicense-DriversLicenseManager-#ctor-Nullafi-Domains-StaticVault-StaticVault-'></a>
### #ctor(vault) `constructor`

##### Summary

Create an instance of DriversLicenseManager

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| vault | [Nullafi.Domains.StaticVault.StaticVault](#T-Nullafi-Domains-StaticVault-StaticVault 'Nullafi.Domains.StaticVault.StaticVault') |  |

<a name='M-Nullafi-Domains-StaticVault-Managers-DriversLicense-DriversLicenseManager-Create-System-String,System-Collections-Generic-List{System-String}-'></a>
### Create(driversLicense,tags) `method`

##### Summary

Create a new DriversLicense string to be aliased within static vault

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| driversLicense | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| tags | [System.Collections.Generic.List{System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{System.String}') |  |

<a name='M-Nullafi-Domains-StaticVault-Managers-DriversLicense-DriversLicenseManager-Create-System-String,System-String,System-Collections-Generic-List{System-String}-'></a>
### Create(driversLicense,state,tags) `method`

##### Summary

Create a new DriversLicense string to be aliased within static vault

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| driversLicense | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| state | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| tags | [System.Collections.Generic.List{System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{System.String}') |  |

<a name='M-Nullafi-Domains-StaticVault-Managers-DriversLicense-DriversLicenseManager-Delete-System-String-'></a>
### Delete(aliasId) `method`

##### Summary

Delete the DriversLicense alias from static vault

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| aliasId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-Nullafi-Domains-StaticVault-Managers-DriversLicense-DriversLicenseManager-Retrieve-System-String-'></a>
### Retrieve(aliasId) `method`

##### Summary

Retrieve the DriversLicense string alias from a static vault. Returns an array of matching values. Array will be sorted by date created.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| aliasId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='T-Nullafi-Domains-StaticVault-Managers-DriversLicense-DriversLicenseRequest'></a>
## DriversLicenseRequest `type`

##### Namespace

Nullafi.Domains.StaticVault.Managers.DriversLicense

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-DriversLicense-DriversLicenseRequest-AuthTag'></a>
### AuthTag `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-DriversLicense-DriversLicenseRequest-DriversLicense'></a>
### DriversLicense `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-DriversLicense-DriversLicenseRequest-DriversLicenseHash'></a>
### DriversLicenseHash `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-DriversLicense-DriversLicenseRequest-Iv'></a>
### Iv `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-DriversLicense-DriversLicenseRequest-Tags'></a>
### Tags `property`

##### Summary



<a name='T-Nullafi-Domains-StaticVault-Managers-DriversLicense-DriversLicenseResponse'></a>
## DriversLicenseResponse `type`

##### Namespace

Nullafi.Domains.StaticVault.Managers.DriversLicense

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-DriversLicense-DriversLicenseResponse-AuthTag'></a>
### AuthTag `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-DriversLicense-DriversLicenseResponse-CreatedAt'></a>
### CreatedAt `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-DriversLicense-DriversLicenseResponse-DriversLicense'></a>
### DriversLicense `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-DriversLicense-DriversLicenseResponse-DriversLicenseAlias'></a>
### DriversLicenseAlias `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-DriversLicense-DriversLicenseResponse-Id'></a>
### Id `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-DriversLicense-DriversLicenseResponse-Iv'></a>
### Iv `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-DriversLicense-DriversLicenseResponse-Tags'></a>
### Tags `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-DriversLicense-DriversLicenseResponse-UpdatedAt'></a>
### UpdatedAt `property`

##### Summary



<a name='T-Nullafi-Domains-CommunicationVault-Managers-Email-EmailManager'></a>
## EmailManager `type`

##### Namespace

Nullafi.Domains.CommunicationVault.Managers.Email

##### Summary

EmailManager

<a name='M-Nullafi-Domains-CommunicationVault-Managers-Email-EmailManager-#ctor-Nullafi-Domains-CommunicationVault-CommunicationVault-'></a>
### #ctor(vault) `constructor`

##### Summary

Create an instance of EmailManager

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| vault | [Nullafi.Domains.CommunicationVault.CommunicationVault](#T-Nullafi-Domains-CommunicationVault-CommunicationVault 'Nullafi.Domains.CommunicationVault.CommunicationVault') |  |

<a name='M-Nullafi-Domains-CommunicationVault-Managers-Email-EmailManager-Create-System-String,System-Collections-Generic-List{System-String}-'></a>
### Create(email,tags) `method`

##### Summary

Create a new Email to be aliased within communication vault

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| email | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| tags | [System.Collections.Generic.List{System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{System.String}') |  |

<a name='M-Nullafi-Domains-CommunicationVault-Managers-Email-EmailManager-Delete-System-String-'></a>
### Delete(aliasId) `method`

##### Summary

Delete the Email alias from communication vault

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| aliasId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-Nullafi-Domains-CommunicationVault-Managers-Email-EmailManager-Retrieve-System-String-'></a>
### Retrieve(aliasId) `method`

##### Summary

Retrieve the Email alias from a communication vault. Returns an array of matching values. Array will be sorted by date created.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| aliasId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='T-Nullafi-Domains-CommunicationVault-Managers-Email-EmailRequest'></a>
## EmailRequest `type`

##### Namespace

Nullafi.Domains.CommunicationVault.Managers.Email

##### Summary



<a name='P-Nullafi-Domains-CommunicationVault-Managers-Email-EmailRequest-AuthTag'></a>
### AuthTag `property`

##### Summary



<a name='P-Nullafi-Domains-CommunicationVault-Managers-Email-EmailRequest-Email'></a>
### Email `property`

##### Summary



<a name='P-Nullafi-Domains-CommunicationVault-Managers-Email-EmailRequest-EmailHash'></a>
### EmailHash `property`

##### Summary



<a name='P-Nullafi-Domains-CommunicationVault-Managers-Email-EmailRequest-Iv'></a>
### Iv `property`

##### Summary



<a name='P-Nullafi-Domains-CommunicationVault-Managers-Email-EmailRequest-Tags'></a>
### Tags `property`

##### Summary



<a name='T-Nullafi-Domains-CommunicationVault-Managers-Email-EmailResponse'></a>
## EmailResponse `type`

##### Namespace

Nullafi.Domains.CommunicationVault.Managers.Email

##### Summary



<a name='P-Nullafi-Domains-CommunicationVault-Managers-Email-EmailResponse-AuthTag'></a>
### AuthTag `property`

##### Summary



<a name='P-Nullafi-Domains-CommunicationVault-Managers-Email-EmailResponse-CreatedAt'></a>
### CreatedAt `property`

##### Summary



<a name='P-Nullafi-Domains-CommunicationVault-Managers-Email-EmailResponse-Email'></a>
### Email `property`

##### Summary



<a name='P-Nullafi-Domains-CommunicationVault-Managers-Email-EmailResponse-EmailAlias'></a>
### EmailAlias `property`

##### Summary



<a name='P-Nullafi-Domains-CommunicationVault-Managers-Email-EmailResponse-Id'></a>
### Id `property`

##### Summary



<a name='P-Nullafi-Domains-CommunicationVault-Managers-Email-EmailResponse-Iv'></a>
### Iv `property`

##### Summary



<a name='P-Nullafi-Domains-CommunicationVault-Managers-Email-EmailResponse-Tags'></a>
### Tags `property`

##### Summary



<a name='P-Nullafi-Domains-CommunicationVault-Managers-Email-EmailResponse-UpdatedAt'></a>
### UpdatedAt `property`

##### Summary



<a name='T-Nullafi-Domains-StaticVault-Managers-FirstName-FirstNameManager'></a>
## FirstNameManager `type`

##### Namespace

Nullafi.Domains.StaticVault.Managers.FirstName

##### Summary

Create an instance of FirstNameManager

<a name='M-Nullafi-Domains-StaticVault-Managers-FirstName-FirstNameManager-#ctor-Nullafi-Domains-StaticVault-StaticVault-'></a>
### #ctor(vault) `constructor`

##### Summary

Create an instance of FirstNameManager

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| vault | [Nullafi.Domains.StaticVault.StaticVault](#T-Nullafi-Domains-StaticVault-StaticVault 'Nullafi.Domains.StaticVault.StaticVault') |  |

<a name='M-Nullafi-Domains-StaticVault-Managers-FirstName-FirstNameManager-Create-System-String,System-Collections-Generic-List{System-String}-'></a>
### Create(firstname,tags) `method`

##### Summary

Create a new FirstName string to be aliased within static vault

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| firstname | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| tags | [System.Collections.Generic.List{System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{System.String}') |  |

<a name='M-Nullafi-Domains-StaticVault-Managers-FirstName-FirstNameManager-Create-System-String,System-String,System-Collections-Generic-List{System-String}-'></a>
### Create(firstname,gender,tags) `method`

##### Summary

Create a new FirstName string to be aliased within static vault

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| firstname | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| gender | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| tags | [System.Collections.Generic.List{System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{System.String}') |  |

<a name='M-Nullafi-Domains-StaticVault-Managers-FirstName-FirstNameManager-Delete-System-String-'></a>
### Delete(aliasId) `method`

##### Summary

Delete the FirstName alias from static vault

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| aliasId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-Nullafi-Domains-StaticVault-Managers-FirstName-FirstNameManager-Retrieve-System-String-'></a>
### Retrieve(aliasId) `method`

##### Summary

Retrieve the FirstName string alias from a static vault. Returns an array of matching values. Array will be sorted by date created.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| aliasId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='T-Nullafi-Domains-StaticVault-Managers-FirstName-FirstNameRequest'></a>
## FirstNameRequest `type`

##### Namespace

Nullafi.Domains.StaticVault.Managers.FirstName

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-FirstName-FirstNameRequest-AuthTag'></a>
### AuthTag `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-FirstName-FirstNameRequest-FirstName'></a>
### FirstName `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-FirstName-FirstNameRequest-FirstNameHash'></a>
### FirstNameHash `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-FirstName-FirstNameRequest-Iv'></a>
### Iv `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-FirstName-FirstNameRequest-Tags'></a>
### Tags `property`

##### Summary



<a name='T-Nullafi-Domains-StaticVault-Managers-FirstName-FirstNameResponse'></a>
## FirstNameResponse `type`

##### Namespace

Nullafi.Domains.StaticVault.Managers.FirstName

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-FirstName-FirstNameResponse-AuthTag'></a>
### AuthTag `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-FirstName-FirstNameResponse-CreatedAt'></a>
### CreatedAt `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-FirstName-FirstNameResponse-FirstName'></a>
### FirstName `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-FirstName-FirstNameResponse-FirstNameAlias'></a>
### FirstNameAlias `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-FirstName-FirstNameResponse-Id'></a>
### Id `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-FirstName-FirstNameResponse-Iv'></a>
### Iv `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-FirstName-FirstNameResponse-Tags'></a>
### Tags `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-FirstName-FirstNameResponse-UpdatedAt'></a>
### UpdatedAt `property`

##### Summary



<a name='T-Nullafi-Domains-StaticVault-Managers-Gender-GenderManager'></a>
## GenderManager `type`

##### Namespace

Nullafi.Domains.StaticVault.Managers.Gender

##### Summary

FirstNameManager

<a name='M-Nullafi-Domains-StaticVault-Managers-Gender-GenderManager-#ctor-Nullafi-Domains-StaticVault-StaticVault-'></a>
### #ctor(vault) `constructor`

##### Summary

Create an instance of GenderManager

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| vault | [Nullafi.Domains.StaticVault.StaticVault](#T-Nullafi-Domains-StaticVault-StaticVault 'Nullafi.Domains.StaticVault.StaticVault') |  |

<a name='M-Nullafi-Domains-StaticVault-Managers-Gender-GenderManager-Create-System-String,System-Collections-Generic-List{System-String}-'></a>
### Create(gender,tags) `method`

##### Summary

Create a new Gender string to be aliased within static vault

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| gender | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| tags | [System.Collections.Generic.List{System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{System.String}') |  |

<a name='M-Nullafi-Domains-StaticVault-Managers-Gender-GenderManager-Delete-System-String-'></a>
### Delete(aliasId) `method`

##### Summary

Delete the Gender alias from static vault

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| aliasId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-Nullafi-Domains-StaticVault-Managers-Gender-GenderManager-Retrieve-System-String-'></a>
### Retrieve(aliasId) `method`

##### Summary

Retrieve the Gender string alias from a static vault. Returns an array of matching values. Array will be sorted by date created.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| aliasId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='T-Nullafi-Domains-StaticVault-Managers-Gender-GenderRequest'></a>
## GenderRequest `type`

##### Namespace

Nullafi.Domains.StaticVault.Managers.Gender

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Gender-GenderRequest-AuthTag'></a>
### AuthTag `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Gender-GenderRequest-Gender'></a>
### Gender `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Gender-GenderRequest-GenderHash'></a>
### GenderHash `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Gender-GenderRequest-Iv'></a>
### Iv `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Gender-GenderRequest-Tags'></a>
### Tags `property`

##### Summary



<a name='T-Nullafi-Domains-StaticVault-Managers-Gender-GenderResponse'></a>
## GenderResponse `type`

##### Namespace

Nullafi.Domains.StaticVault.Managers.Gender

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Gender-GenderResponse-AuthTag'></a>
### AuthTag `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Gender-GenderResponse-CreatedAt'></a>
### CreatedAt `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Gender-GenderResponse-Gender'></a>
### Gender `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Gender-GenderResponse-GenderAlias'></a>
### GenderAlias `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Gender-GenderResponse-Id'></a>
### Id `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Gender-GenderResponse-Iv'></a>
### Iv `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Gender-GenderResponse-Tags'></a>
### Tags `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Gender-GenderResponse-UpdatedAt'></a>
### UpdatedAt `property`

##### Summary



<a name='T-Nullafi-Domains-StaticVault-Managers-Generic-GenericManager'></a>
## GenericManager `type`

##### Namespace

Nullafi.Domains.StaticVault.Managers.Generic

##### Summary

GenericManager

<a name='M-Nullafi-Domains-StaticVault-Managers-Generic-GenericManager-#ctor-Nullafi-Domains-StaticVault-StaticVault-'></a>
### #ctor(vault) `constructor`

##### Summary

Create an instance of GenericManager

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| vault | [Nullafi.Domains.StaticVault.StaticVault](#T-Nullafi-Domains-StaticVault-StaticVault 'Nullafi.Domains.StaticVault.StaticVault') |  |

<a name='M-Nullafi-Domains-StaticVault-Managers-Generic-GenericManager-Create-System-String,System-Collections-Generic-List{System-String}-'></a>
### Create(data,tags) `method`

##### Summary

Create a new Gender string to be aliased within static vault

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| data | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| tags | [System.Collections.Generic.List{System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{System.String}') |  |

<a name='M-Nullafi-Domains-StaticVault-Managers-Generic-GenericManager-Delete-System-String-'></a>
### Delete(aliasId) `method`

##### Summary

Delete the Generic alias from static vault

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| aliasId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-Nullafi-Domains-StaticVault-Managers-Generic-GenericManager-Retrieve-System-String-'></a>
### Retrieve(aliasId) `method`

##### Summary

Retrieve the Generic string alias from a static vault. Returns an array of matching values. Array will be sorted by date created.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| aliasId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='T-Nullafi-Domains-StaticVault-Managers-Generic-GenericRequest'></a>
## GenericRequest `type`

##### Namespace

Nullafi.Domains.StaticVault.Managers.Generic

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Generic-GenericRequest-AuthTag'></a>
### AuthTag `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Generic-GenericRequest-Data'></a>
### Data `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Generic-GenericRequest-DataHash'></a>
### DataHash `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Generic-GenericRequest-Iv'></a>
### Iv `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Generic-GenericRequest-Tags'></a>
### Tags `property`

##### Summary



<a name='T-Nullafi-Domains-StaticVault-Managers-Generic-GenericResponse'></a>
## GenericResponse `type`

##### Namespace

Nullafi.Domains.StaticVault.Managers.Generic

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Generic-GenericResponse-Alias'></a>
### Alias `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Generic-GenericResponse-AuthTag'></a>
### AuthTag `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Generic-GenericResponse-CreatedAt'></a>
### CreatedAt `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Generic-GenericResponse-Data'></a>
### Data `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Generic-GenericResponse-Id'></a>
### Id `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Generic-GenericResponse-Iv'></a>
### Iv `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Generic-GenericResponse-Tags'></a>
### Tags `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Generic-GenericResponse-UpdatedAt'></a>
### UpdatedAt `property`

##### Summary



<a name='T-Nullafi-Services-Crypto-Hmac'></a>
## Hmac `type`

##### Namespace

Nullafi.Services.Crypto

##### Summary

Hmac

<a name='M-Nullafi-Services-Crypto-Hmac-Hash-System-String,System-String-'></a>
### Hash(value,secret) `method`

##### Summary

Generate a hash for the real data

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| secret | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='T-Nullafi-Domains-StaticVault-Managers-LastName-LastNameManager'></a>
## LastNameManager `type`

##### Namespace

Nullafi.Domains.StaticVault.Managers.LastName

##### Summary

LastNameManager

<a name='M-Nullafi-Domains-StaticVault-Managers-LastName-LastNameManager-#ctor-Nullafi-Domains-StaticVault-StaticVault-'></a>
### #ctor(vault) `constructor`

##### Summary

Create an instance of LastNameManager

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| vault | [Nullafi.Domains.StaticVault.StaticVault](#T-Nullafi-Domains-StaticVault-StaticVault 'Nullafi.Domains.StaticVault.StaticVault') |  |

<a name='M-Nullafi-Domains-StaticVault-Managers-LastName-LastNameManager-Create-System-String,System-Collections-Generic-List{System-String}-'></a>
### Create(lastname,tags) `method`

##### Summary

Create a new LastName string to be aliased within static vault

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| lastname | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| tags | [System.Collections.Generic.List{System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{System.String}') |  |

<a name='M-Nullafi-Domains-StaticVault-Managers-LastName-LastNameManager-Create-System-String,System-String,System-Collections-Generic-List{System-String}-'></a>
### Create(lastname,gender,tags) `method`

##### Summary

Create a new LastName string to be aliased within static vault

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| lastname | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| gender | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| tags | [System.Collections.Generic.List{System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{System.String}') |  |

<a name='M-Nullafi-Domains-StaticVault-Managers-LastName-LastNameManager-Delete-System-String-'></a>
### Delete(aliasId) `method`

##### Summary

Delete the LastName alias from static vault

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| aliasId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-Nullafi-Domains-StaticVault-Managers-LastName-LastNameManager-Retrieve-System-String-'></a>
### Retrieve(aliasId) `method`

##### Summary

Retrieve the LastName string alias from a static vault. Returns an array of matching values. Array will be sorted by date created.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| aliasId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='T-Nullafi-Domains-StaticVault-Managers-LastName-LastNameRequest'></a>
## LastNameRequest `type`

##### Namespace

Nullafi.Domains.StaticVault.Managers.LastName

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-LastName-LastNameRequest-AuthTag'></a>
### AuthTag `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-LastName-LastNameRequest-Iv'></a>
### Iv `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-LastName-LastNameRequest-LastName'></a>
### LastName `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-LastName-LastNameRequest-LastNameHash'></a>
### LastNameHash `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-LastName-LastNameRequest-Tags'></a>
### Tags `property`

##### Summary



<a name='T-Nullafi-Domains-StaticVault-Managers-LastName-LastNameResponse'></a>
## LastNameResponse `type`

##### Namespace

Nullafi.Domains.StaticVault.Managers.LastName

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-LastName-LastNameResponse-AuthTag'></a>
### AuthTag `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-LastName-LastNameResponse-CreatedAt'></a>
### CreatedAt `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-LastName-LastNameResponse-Id'></a>
### Id `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-LastName-LastNameResponse-Iv'></a>
### Iv `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-LastName-LastNameResponse-LastName'></a>
### LastName `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-LastName-LastNameResponse-LastNameAlias'></a>
### LastNameAlias `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-LastName-LastNameResponse-Tags'></a>
### Tags `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-LastName-LastNameResponse-UpdatedAt'></a>
### UpdatedAt `property`

##### Summary



<a name='T-Nullafi-NullafiSDK'></a>
## NullafiSDK `type`

##### Namespace

Nullafi

##### Summary

NullafiSDK class

<a name='M-Nullafi-NullafiSDK-#ctor-System-String-'></a>
### #ctor(apiKey) `constructor`

##### Summary

Create an instance of NullafiSDK

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| apiKey | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-Nullafi-NullafiSDK-CreateClient'></a>
### CreateClient() `method`

##### Summary

Create a new instance of client

##### Returns



##### Parameters

This method has no parameters.

<a name='T-Nullafi-Domains-StaticVault-Managers-Passport-PassportManager'></a>
## PassportManager `type`

##### Namespace

Nullafi.Domains.StaticVault.Managers.Passport

##### Summary

Passport Manager

<a name='M-Nullafi-Domains-StaticVault-Managers-Passport-PassportManager-#ctor-Nullafi-Domains-StaticVault-StaticVault-'></a>
### #ctor(vault) `constructor`

##### Summary

Create an instance of Passport Manager

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| vault | [Nullafi.Domains.StaticVault.StaticVault](#T-Nullafi-Domains-StaticVault-StaticVault 'Nullafi.Domains.StaticVault.StaticVault') |  |

<a name='M-Nullafi-Domains-StaticVault-Managers-Passport-PassportManager-Create-System-String,System-Collections-Generic-List{System-String}-'></a>
### Create(passport,tags) `method`

##### Summary

Create a new LastName string to be aliased within static vault

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| passport | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| tags | [System.Collections.Generic.List{System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{System.String}') |  |

<a name='M-Nullafi-Domains-StaticVault-Managers-Passport-PassportManager-Delete-System-String-'></a>
### Delete(aliasId) `method`

##### Summary

Delete the Passport alias from static vault

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| aliasId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-Nullafi-Domains-StaticVault-Managers-Passport-PassportManager-Retrieve-System-String-'></a>
### Retrieve(aliasId) `method`

##### Summary

Retrieve the Passport string alias from a static vault. Returns an array of matching values. Array will be sorted by date created.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| aliasId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='T-Nullafi-Domains-StaticVault-Managers-Passport-PassportRequest'></a>
## PassportRequest `type`

##### Namespace

Nullafi.Domains.StaticVault.Managers.Passport

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Passport-PassportRequest-AuthTag'></a>
### AuthTag `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Passport-PassportRequest-Iv'></a>
### Iv `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Passport-PassportRequest-Passport'></a>
### Passport `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Passport-PassportRequest-PassportHash'></a>
### PassportHash `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Passport-PassportRequest-Tags'></a>
### Tags `property`

##### Summary



<a name='T-Nullafi-Domains-StaticVault-Managers-Passport-PassportResponse'></a>
## PassportResponse `type`

##### Namespace

Nullafi.Domains.StaticVault.Managers.Passport

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Passport-PassportResponse-AuthTag'></a>
### AuthTag `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Passport-PassportResponse-CreatedAt'></a>
### CreatedAt `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Passport-PassportResponse-Id'></a>
### Id `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Passport-PassportResponse-Iv'></a>
### Iv `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Passport-PassportResponse-Passport'></a>
### Passport `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Passport-PassportResponse-PassportAlias'></a>
### PassportAlias `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Passport-PassportResponse-Tags'></a>
### Tags `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Passport-PassportResponse-UpdatedAt'></a>
### UpdatedAt `property`

##### Summary



<a name='T-Nullafi-Domains-StaticVault-Managers-PlaceOfBirth-PlaceOfBirthManager'></a>
## PlaceOfBirthManager `type`

##### Namespace

Nullafi.Domains.StaticVault.Managers.PlaceOfBirth

##### Summary

PlaceOfBirth Manager

<a name='M-Nullafi-Domains-StaticVault-Managers-PlaceOfBirth-PlaceOfBirthManager-#ctor-Nullafi-Domains-StaticVault-StaticVault-'></a>
### #ctor(vault) `constructor`

##### Summary

Create an instance of PlaceOfBirth Manager

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| vault | [Nullafi.Domains.StaticVault.StaticVault](#T-Nullafi-Domains-StaticVault-StaticVault 'Nullafi.Domains.StaticVault.StaticVault') |  |

<a name='M-Nullafi-Domains-StaticVault-Managers-PlaceOfBirth-PlaceOfBirthManager-Create-System-String,System-Collections-Generic-List{System-String}-'></a>
### Create(placeofbirth,tags) `method`

##### Summary

Create a new PlaceOfBirth string to be aliased within static vault

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| placeofbirth | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| tags | [System.Collections.Generic.List{System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{System.String}') |  |

<a name='M-Nullafi-Domains-StaticVault-Managers-PlaceOfBirth-PlaceOfBirthManager-Create-System-String,System-String,System-Collections-Generic-List{System-String}-'></a>
### Create(placeofbirth,state,tags) `method`

##### Summary

Create a new PlaceOfBirth string to be aliased within static vault

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| placeofbirth | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| state | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| tags | [System.Collections.Generic.List{System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{System.String}') |  |

<a name='M-Nullafi-Domains-StaticVault-Managers-PlaceOfBirth-PlaceOfBirthManager-Delete-System-String-'></a>
### Delete(aliasId) `method`

##### Summary

Delete the PlaceOfBirth alias from static vault

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| aliasId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-Nullafi-Domains-StaticVault-Managers-PlaceOfBirth-PlaceOfBirthManager-Retrieve-System-String-'></a>
### Retrieve(aliasId) `method`

##### Summary

Retrieve the PlaceOfBirth string alias from a static vault. Returns an array of matching values. Array will be sorted by date created.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| aliasId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='T-Nullafi-Domains-StaticVault-Managers-PlaceOfBirth-PlaceOfBirthRequest'></a>
## PlaceOfBirthRequest `type`

##### Namespace

Nullafi.Domains.StaticVault.Managers.PlaceOfBirth

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-PlaceOfBirth-PlaceOfBirthRequest-AuthTag'></a>
### AuthTag `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-PlaceOfBirth-PlaceOfBirthRequest-Iv'></a>
### Iv `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-PlaceOfBirth-PlaceOfBirthRequest-PlaceOfBirth'></a>
### PlaceOfBirth `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-PlaceOfBirth-PlaceOfBirthRequest-PlaceOfBirthHash'></a>
### PlaceOfBirthHash `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-PlaceOfBirth-PlaceOfBirthRequest-Tags'></a>
### Tags `property`

##### Summary



<a name='T-Nullafi-Domains-StaticVault-Managers-PlaceOfBirth-PlaceOfBirthResponse'></a>
## PlaceOfBirthResponse `type`

##### Namespace

Nullafi.Domains.StaticVault.Managers.PlaceOfBirth

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-PlaceOfBirth-PlaceOfBirthResponse-AuthTag'></a>
### AuthTag `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-PlaceOfBirth-PlaceOfBirthResponse-CreatedAt'></a>
### CreatedAt `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-PlaceOfBirth-PlaceOfBirthResponse-Id'></a>
### Id `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-PlaceOfBirth-PlaceOfBirthResponse-Iv'></a>
### Iv `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-PlaceOfBirth-PlaceOfBirthResponse-PlaceOfBirth'></a>
### PlaceOfBirth `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-PlaceOfBirth-PlaceOfBirthResponse-PlaceOfBirthAlias'></a>
### PlaceOfBirthAlias `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-PlaceOfBirth-PlaceOfBirthResponse-Tags'></a>
### Tags `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-PlaceOfBirth-PlaceOfBirthResponse-UpdatedAt'></a>
### UpdatedAt `property`

##### Summary



<a name='T-Nullafi-Services-Crypto-RSA'></a>
## RSA `type`

##### Namespace

Nullafi.Services.Crypto

##### Summary

RSA

<a name='M-Nullafi-Services-Crypto-RSA-RSAGenerateManager'></a>
### RSAGenerateManager() `method`

##### Summary

Create instance of RSAManager

##### Parameters

This method has no parameters.

<a name='T-Nullafi-Services-Crypto-RSAManager'></a>
## RSAManager `type`

##### Namespace

Nullafi.Services.Crypto

##### Summary

RSAManager

<a name='P-Nullafi-Services-Crypto-RSAManager-Decrypt'></a>
### Decrypt `property`

##### Summary

Decrypt object to a base64 string using a private key

<a name='P-Nullafi-Services-Crypto-RSAManager-PublicKey'></a>
### PublicKey `property`

##### Summary



<a name='T-Nullafi-Domains-StaticVault-Managers-Race-RaceManager'></a>
## RaceManager `type`

##### Namespace

Nullafi.Domains.StaticVault.Managers.Race

##### Summary

Race Manager

<a name='M-Nullafi-Domains-StaticVault-Managers-Race-RaceManager-#ctor-Nullafi-Domains-StaticVault-StaticVault-'></a>
### #ctor(vault) `constructor`

##### Summary

Create an instance of Race Manager

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| vault | [Nullafi.Domains.StaticVault.StaticVault](#T-Nullafi-Domains-StaticVault-StaticVault 'Nullafi.Domains.StaticVault.StaticVault') |  |

<a name='M-Nullafi-Domains-StaticVault-Managers-Race-RaceManager-Create-System-String,System-Collections-Generic-List{System-String}-'></a>
### Create(race,tags) `method`

##### Summary

Create a new Race string to be aliased within static vault

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| race | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| tags | [System.Collections.Generic.List{System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{System.String}') |  |

<a name='M-Nullafi-Domains-StaticVault-Managers-Race-RaceManager-Delete-System-String-'></a>
### Delete(aliasId) `method`

##### Summary

Delete the Race alias from static vault

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| aliasId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-Nullafi-Domains-StaticVault-Managers-Race-RaceManager-Retrieve-System-String-'></a>
### Retrieve(aliasId) `method`

##### Summary

Retrieve the race string alias from a static vault. Returns an array of matching values. Array will be sorted by date created.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| aliasId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='T-Nullafi-Domains-StaticVault-Managers-Race-RaceRequest'></a>
## RaceRequest `type`

##### Namespace

Nullafi.Domains.StaticVault.Managers.Race

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Race-RaceRequest-AuthTag'></a>
### AuthTag `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Race-RaceRequest-Iv'></a>
### Iv `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Race-RaceRequest-Race'></a>
### Race `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Race-RaceRequest-RaceHash'></a>
### RaceHash `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Race-RaceRequest-Tags'></a>
### Tags `property`

##### Summary



<a name='T-Nullafi-Domains-StaticVault-Managers-Race-RaceResponse'></a>
## RaceResponse `type`

##### Namespace

Nullafi.Domains.StaticVault.Managers.Race

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Race-RaceResponse-AuthTag'></a>
### AuthTag `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Race-RaceResponse-CreatedAt'></a>
### CreatedAt `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Race-RaceResponse-Id'></a>
### Id `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Race-RaceResponse-Iv'></a>
### Iv `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Race-RaceResponse-Race'></a>
### Race `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Race-RaceResponse-RaceAlias'></a>
### RaceAlias `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Race-RaceResponse-Tags'></a>
### Tags `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Race-RaceResponse-UpdatedAt'></a>
### UpdatedAt `property`

##### Summary



<a name='T-Nullafi-Domains-StaticVault-Managers-Random-RandomManager'></a>
## RandomManager `type`

##### Namespace

Nullafi.Domains.StaticVault.Managers.Random

##### Summary

Random Manager

<a name='M-Nullafi-Domains-StaticVault-Managers-Random-RandomManager-#ctor-Nullafi-Domains-StaticVault-StaticVault-'></a>
### #ctor(vault) `constructor`

##### Summary

Create an instance of Random Manager

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| vault | [Nullafi.Domains.StaticVault.StaticVault](#T-Nullafi-Domains-StaticVault-StaticVault 'Nullafi.Domains.StaticVault.StaticVault') |  |

<a name='M-Nullafi-Domains-StaticVault-Managers-Random-RandomManager-Create-System-String,System-Collections-Generic-List{System-String}-'></a>
### Create(data,tags) `method`

##### Summary

Create a new Random string to be aliased within static vault

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| data | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| tags | [System.Collections.Generic.List{System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{System.String}') |  |

<a name='M-Nullafi-Domains-StaticVault-Managers-Random-RandomManager-Delete-System-String-'></a>
### Delete(aliasId) `method`

##### Summary

Delete the Random alias from static vault

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| aliasId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-Nullafi-Domains-StaticVault-Managers-Random-RandomManager-Retrieve-System-String-'></a>
### Retrieve(aliasId) `method`

##### Summary

Retrieve the Random string alias from a static vault. Returns an array of matching values. Array will be sorted by date created.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| aliasId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='T-Nullafi-Domains-StaticVault-Managers-Random-RandomRequest'></a>
## RandomRequest `type`

##### Namespace

Nullafi.Domains.StaticVault.Managers.Random

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Random-RandomRequest-AuthTag'></a>
### AuthTag `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Random-RandomRequest-Data'></a>
### Data `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Random-RandomRequest-DataHash'></a>
### DataHash `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Random-RandomRequest-Iv'></a>
### Iv `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Random-RandomRequest-Tags'></a>
### Tags `property`

##### Summary



<a name='T-Nullafi-Domains-StaticVault-Managers-Random-RandomResponse'></a>
## RandomResponse `type`

##### Namespace

Nullafi.Domains.StaticVault.Managers.Random

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Random-RandomResponse-Alias'></a>
### Alias `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Random-RandomResponse-AuthTag'></a>
### AuthTag `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Random-RandomResponse-CreatedAt'></a>
### CreatedAt `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Random-RandomResponse-Data'></a>
### Data `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Random-RandomResponse-Id'></a>
### Id `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Random-RandomResponse-Iv'></a>
### Iv `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Random-RandomResponse-Tags'></a>
### Tags `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Random-RandomResponse-UpdatedAt'></a>
### UpdatedAt `property`

##### Summary



<a name='T-Nullafi-Security'></a>
## Security `type`

##### Namespace

Nullafi

##### Summary

Security

<a name='M-Nullafi-Security-#ctor'></a>
### #ctor() `constructor`

##### Summary

Create an instance of Security

##### Parameters

This constructor has no parameters.

<a name='P-Nullafi-Security-Aes'></a>
### Aes `property`

##### Summary

Aesgcm

<a name='P-Nullafi-Security-Hmac'></a>
### Hmac `property`

##### Summary

Hmac

<a name='P-Nullafi-Security-RSA'></a>
### RSA `property`

##### Summary

RSA

<a name='T-Nullafi-Domains-StaticVault-Managers-Ssn-SsnManager'></a>
## SsnManager `type`

##### Namespace

Nullafi.Domains.StaticVault.Managers.Ssn

##### Summary

SSN Manager

<a name='M-Nullafi-Domains-StaticVault-Managers-Ssn-SsnManager-#ctor-Nullafi-Domains-StaticVault-StaticVault-'></a>
### #ctor(vault) `constructor`

##### Summary

Create an instance of SSN Manager

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| vault | [Nullafi.Domains.StaticVault.StaticVault](#T-Nullafi-Domains-StaticVault-StaticVault 'Nullafi.Domains.StaticVault.StaticVault') |  |

<a name='M-Nullafi-Domains-StaticVault-Managers-Ssn-SsnManager-Create-System-String,System-Collections-Generic-List{System-String}-'></a>
### Create(ssn,tags) `method`

##### Summary

Create a new SSN string to be aliased within static vault

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| ssn | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| tags | [System.Collections.Generic.List{System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{System.String}') |  |

<a name='M-Nullafi-Domains-StaticVault-Managers-Ssn-SsnManager-Create-System-String,System-String,System-Collections-Generic-List{System-String}-'></a>
### Create(ssn,state,tags) `method`

##### Summary

Create a new SSN string to be aliased within static vault

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| ssn | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| state | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| tags | [System.Collections.Generic.List{System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{System.String}') |  |

<a name='M-Nullafi-Domains-StaticVault-Managers-Ssn-SsnManager-Delete-System-String-'></a>
### Delete(aliasId) `method`

##### Summary

Delete the SSN alias from static vault

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| aliasId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-Nullafi-Domains-StaticVault-Managers-Ssn-SsnManager-Retrieve-System-String-'></a>
### Retrieve(aliasId) `method`

##### Summary

Retrieve the SSN string alias from a static vault. Returns an array of matching values. Array will be sorted by date created.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| aliasId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='T-Nullafi-Domains-StaticVault-Managers-Ssn-SsnRequest'></a>
## SsnRequest `type`

##### Namespace

Nullafi.Domains.StaticVault.Managers.Ssn

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Ssn-SsnRequest-AuthTag'></a>
### AuthTag `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Ssn-SsnRequest-Iv'></a>
### Iv `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Ssn-SsnRequest-Ssn'></a>
### Ssn `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Ssn-SsnRequest-SsnHash'></a>
### SsnHash `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Ssn-SsnRequest-Tags'></a>
### Tags `property`

##### Summary



<a name='T-Nullafi-Domains-StaticVault-Managers-Ssn-SsnResponse'></a>
## SsnResponse `type`

##### Namespace

Nullafi.Domains.StaticVault.Managers.Ssn

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Ssn-SsnResponse-AuthTag'></a>
### AuthTag `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Ssn-SsnResponse-CreatedAt'></a>
### CreatedAt `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Ssn-SsnResponse-Id'></a>
### Id `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Ssn-SsnResponse-Iv'></a>
### Iv `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Ssn-SsnResponse-Ssn'></a>
### Ssn `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Ssn-SsnResponse-SsnAlias'></a>
### SsnAlias `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Ssn-SsnResponse-Tags'></a>
### Tags `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-Ssn-SsnResponse-UpdatedAt'></a>
### UpdatedAt `property`

##### Summary



<a name='T-Nullafi-Domains-StaticVault-StaticVault'></a>
## StaticVault `type`

##### Namespace

Nullafi.Domains.StaticVault

##### Summary

Static Vault

<a name='M-Nullafi-Domains-StaticVault-StaticVault-#ctor-Nullafi-Client,System-String,System-String,System-String-'></a>
### #ctor(client,vaultId,vaultName,masterKey) `constructor`

##### Summary

Create an instance of StaticVault

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| client | [Nullafi.Client](#T-Nullafi-Client 'Nullafi.Client') |  |
| vaultId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| vaultName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| masterKey | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

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

Create the API to create a new static vault

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| client | [Nullafi.Client](#T-Nullafi-Client 'Nullafi.Client') |  |
| name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| tags | [System.Collections.Generic.List{System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{System.String}') |  |

<a name='M-Nullafi-Domains-StaticVault-StaticVault-Hash-System-String-'></a>
### Hash(value) `method`

##### Summary

Generate a hash for the real data

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-Nullafi-Domains-StaticVault-StaticVault-RetrieveStaticVault-Nullafi-Client,System-String,System-String-'></a>
### RetrieveStaticVault(client,vaultId,masterKey) `method`

##### Summary

Retrieve the static vault from id

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| client | [Nullafi.Client](#T-Nullafi-Client 'Nullafi.Client') |  |
| vaultId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| masterKey | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='T-Nullafi-Domains-StaticVault-Managers-TaxPayer-TaxPayerManager'></a>
## TaxPayerManager `type`

##### Namespace

Nullafi.Domains.StaticVault.Managers.TaxPayer

##### Summary

TaxPayer Manager

<a name='M-Nullafi-Domains-StaticVault-Managers-TaxPayer-TaxPayerManager-#ctor-Nullafi-Domains-StaticVault-StaticVault-'></a>
### #ctor(vault) `constructor`

##### Summary

Create an instance of TaxPayer Manager

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| vault | [Nullafi.Domains.StaticVault.StaticVault](#T-Nullafi-Domains-StaticVault-StaticVault 'Nullafi.Domains.StaticVault.StaticVault') |  |

<a name='M-Nullafi-Domains-StaticVault-Managers-TaxPayer-TaxPayerManager-Create-System-String,System-Collections-Generic-List{System-String}-'></a>
### Create(taxpayer,tags) `method`

##### Summary

Create a new TaxPayer string to be aliased within static vault

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| taxpayer | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| tags | [System.Collections.Generic.List{System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{System.String}') |  |

<a name='M-Nullafi-Domains-StaticVault-Managers-TaxPayer-TaxPayerManager-Delete-System-String-'></a>
### Delete(aliasId) `method`

##### Summary

Delete the TaxPayer alias from static vault

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| aliasId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-Nullafi-Domains-StaticVault-Managers-TaxPayer-TaxPayerManager-Retrieve-System-String-'></a>
### Retrieve(aliasId) `method`

##### Summary

Retrieve the TaxPayer string alias from a static vault. Returns an array of matching values. Array will be sorted by date created.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| aliasId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='T-Nullafi-Domains-StaticVault-Managers-TaxPayer-TaxPayerRequest'></a>
## TaxPayerRequest `type`

##### Namespace

Nullafi.Domains.StaticVault.Managers.TaxPayer

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-TaxPayer-TaxPayerRequest-AuthTag'></a>
### AuthTag `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-TaxPayer-TaxPayerRequest-Iv'></a>
### Iv `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-TaxPayer-TaxPayerRequest-Tags'></a>
### Tags `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-TaxPayer-TaxPayerRequest-TaxPayer'></a>
### TaxPayer `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-TaxPayer-TaxPayerRequest-TaxPayerHash'></a>
### TaxPayerHash `property`

##### Summary



<a name='T-Nullafi-Domains-StaticVault-Managers-TaxPayer-TaxPayerResponse'></a>
## TaxPayerResponse `type`

##### Namespace

Nullafi.Domains.StaticVault.Managers.TaxPayer

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-TaxPayer-TaxPayerResponse-AuthTag'></a>
### AuthTag `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-TaxPayer-TaxPayerResponse-CreatedAt'></a>
### CreatedAt `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-TaxPayer-TaxPayerResponse-Id'></a>
### Id `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-TaxPayer-TaxPayerResponse-Iv'></a>
### Iv `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-TaxPayer-TaxPayerResponse-Tags'></a>
### Tags `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-TaxPayer-TaxPayerResponse-TaxPayer'></a>
### TaxPayer `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-TaxPayer-TaxPayerResponse-TaxPayerAlias'></a>
### TaxPayerAlias `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-TaxPayer-TaxPayerResponse-UpdatedAt'></a>
### UpdatedAt `property`

##### Summary



<a name='T-Nullafi-Domains-StaticVault-Managers-VehicleRegistration-VehicleRegistrationManager'></a>
## VehicleRegistrationManager `type`

##### Namespace

Nullafi.Domains.StaticVault.Managers.VehicleRegistration

##### Summary

Create an instance of VehicleRegistration Manager

<a name='M-Nullafi-Domains-StaticVault-Managers-VehicleRegistration-VehicleRegistrationManager-#ctor-Nullafi-Domains-StaticVault-StaticVault-'></a>
### #ctor(vault) `constructor`

##### Summary

Create an instance of VehicleRegistration Manager

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| vault | [Nullafi.Domains.StaticVault.StaticVault](#T-Nullafi-Domains-StaticVault-StaticVault 'Nullafi.Domains.StaticVault.StaticVault') |  |

<a name='M-Nullafi-Domains-StaticVault-Managers-VehicleRegistration-VehicleRegistrationManager-Create-System-String,System-Collections-Generic-List{System-String}-'></a>
### Create(vehicleregistration,tags) `method`

##### Summary

Create a new VehicleRegistration string to be aliased within static vault

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| vehicleregistration | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| tags | [System.Collections.Generic.List{System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{System.String}') |  |

<a name='M-Nullafi-Domains-StaticVault-Managers-VehicleRegistration-VehicleRegistrationManager-Delete-System-String-'></a>
### Delete(aliasId) `method`

##### Summary

Delete the VehicleRegistration alias from static vault

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| aliasId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-Nullafi-Domains-StaticVault-Managers-VehicleRegistration-VehicleRegistrationManager-Retrieve-System-String-'></a>
### Retrieve(aliasId) `method`

##### Summary

Retrieve the VehicleRegistration string alias from a static vault. Returns an array of matching values. Array will be sorted by date created.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| aliasId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='T-Nullafi-Domains-StaticVault-Managers-VehicleRegistration-VehicleRegistrationRequest'></a>
## VehicleRegistrationRequest `type`

##### Namespace

Nullafi.Domains.StaticVault.Managers.VehicleRegistration

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-VehicleRegistration-VehicleRegistrationRequest-AuthTag'></a>
### AuthTag `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-VehicleRegistration-VehicleRegistrationRequest-Iv'></a>
### Iv `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-VehicleRegistration-VehicleRegistrationRequest-Tags'></a>
### Tags `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-VehicleRegistration-VehicleRegistrationRequest-VehicleRegistration'></a>
### VehicleRegistration `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-VehicleRegistration-VehicleRegistrationRequest-VehicleRegistrationHash'></a>
### VehicleRegistrationHash `property`

##### Summary



<a name='T-Nullafi-Domains-StaticVault-Managers-VehicleRegistration-VehicleRegistrationResponse'></a>
## VehicleRegistrationResponse `type`

##### Namespace

Nullafi.Domains.StaticVault.Managers.VehicleRegistration

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-VehicleRegistration-VehicleRegistrationResponse-AuthTag'></a>
### AuthTag `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-VehicleRegistration-VehicleRegistrationResponse-CreatedAt'></a>
### CreatedAt `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-VehicleRegistration-VehicleRegistrationResponse-Id'></a>
### Id `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-VehicleRegistration-VehicleRegistrationResponse-Iv'></a>
### Iv `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-VehicleRegistration-VehicleRegistrationResponse-Tags'></a>
### Tags `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-VehicleRegistration-VehicleRegistrationResponse-UpdatedAt'></a>
### UpdatedAt `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-VehicleRegistration-VehicleRegistrationResponse-VehicleRegistration'></a>
### VehicleRegistration `property`

##### Summary



<a name='P-Nullafi-Domains-StaticVault-Managers-VehicleRegistration-VehicleRegistrationResponse-VehicleRegistrationAlias'></a>
### VehicleRegistrationAlias `property`

##### Summary


