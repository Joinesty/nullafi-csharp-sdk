using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Nullafi.Domains.StaticVault;

namespace Nullafi.Domains.StaticVault.Managers.Generic
{
  public class GenericManager
{
  StaticVault vault;

  public GenericManager(StaticVault vault)
{
  this.vault = vault;
}

  public async Task<GenericResponse> Create(string data, List<string> tags)
  {
    var result = this.vault.Encrypt(data);
    var payload = new GenericRequest
    {
        Data = result.EncryptedData,
        DataHash = this.vault.Hash(data),
        Tags = tags,
        Iv = result.Iv,
        AuthTag = result.AuthTag
    };

    var response = await this.vault.client.Post<GenericRequest, GenericResponse>($"/vault/static/{this.vault.VaultId}/generic", payload);
    response.Data = this.vault.Decrypt(response.Iv, response.AuthTag, response.Data);
    return response;
  }

  public async Task<GenericResponse> Retrieve(string aliasId)
  {
    var response = await this.vault.client.Get<GenericResponse>($"/vault/static/{this.vault.VaultId}/generic/{aliasId}");
    response.Data = this.vault.Decrypt(response.Iv, response.AuthTag, response.Data);
    return response;
  }

  public async Task Delete(string aliasId)
  {
    await this.vault.client.Delete($"/vault/static/{this.vault.VaultId}/gender/{aliasId}");
  }
}
}
