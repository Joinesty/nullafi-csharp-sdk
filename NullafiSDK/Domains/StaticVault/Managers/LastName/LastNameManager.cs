using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NullafiSDK.Domains.StaticVault;

namespace NullafiSDK.Domains.StaticVault.Managers.LastName
{
  public class LastNameManager
{
  StaticVault vault;

  public LastNameManager(StaticVault vault)
{
  this.vault = vault;
}

  public async Task<LastNameModel> Create(string lastname, List<string> tags)
  {
    var result = this.vault.Encrypt(lastname);
    var payload = new LastNameModel
    {
      LastName = result.EncryptedData,
        Iv = result.Iv,
        AuthTag = result.AuthTag
    };

    var response = await this.vault.client.Post<LastNameModel, LastNameModel>($"/vault/static/${this.vault.VaultId}/lastname", payload);
    return response;
  }

  public async Task<LastNameModel> Retrieve(string tokenId)
  {
    return await this.vault.client.Get<LastNameModel>($"/vault/static/{this.vault.VaultId}/lastname/{tokenId}");
  }

  public async void Delete(string tokenId)
  {
    await this.vault.client.Delete($"/vault/static/{this.vault.VaultId}/lastname/{tokenId}");
  }
}
}
