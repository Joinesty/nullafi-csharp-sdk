using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nullafi.Domains.StaticVault.Managers.Generic
{
  public class GenericManager
{
    private readonly StaticVault _vault;

  public GenericManager(StaticVault vault)
{
  _vault = vault;
}

  public async Task<GenericResponse> Create(string data, List<string> tags)
  {
    var result = _vault.Encrypt(data);
    var payload = new GenericRequest
    {
        Data = result.EncryptedData,
        DataHash = _vault.Hash(data),
        Tags = tags,
        Iv = result.Iv,
        AuthTag = result.AuthTag
    };

    var response = await _vault.Client.Post<GenericRequest, GenericResponse>($"/vault/static/{_vault.VaultId}/generic", payload);
    response.Data = _vault.Decrypt(response.Iv, response.AuthTag, response.Data);
    return response;
  }

  public async Task<GenericResponse> Retrieve(string aliasId)
  {
    var response = await _vault.Client.Get<GenericResponse>($"/vault/static/{_vault.VaultId}/generic/{aliasId}");
    response.Data = _vault.Decrypt(response.Iv, response.AuthTag, response.Data);
    return response;
  }

  public async Task Delete(string aliasId)
  {
    await _vault.Client.Delete($"/vault/static/{_vault.VaultId}/gender/{aliasId}");
  }
}
}
