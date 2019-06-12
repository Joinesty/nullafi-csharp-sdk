using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nullafi.Domains.StaticVault.Managers.Random
{
  public class RandomManager
{
    private readonly StaticVault _vault;

  public RandomManager(StaticVault vault)
{
  _vault = vault;
}

  public async Task<RandomResponse> Create(string data, List<string> tags = null)
  {
    var result = _vault.Encrypt(data);
    var payload = new RandomRequest
    {
      Data = result.EncryptedData,
      DataHash = _vault.Hash(data),
      Tags = tags,
      Iv = result.Iv,
      AuthTag = result.AuthTag
    };

    var response = await _vault.Client.Post<RandomRequest, RandomResponse>($"/vault/static/{_vault.VaultId}/random", payload);
    response.Data = _vault.Decrypt(response.Iv, response.AuthTag, response.Data);
    return response;
  }

  public async Task<RandomResponse> Retrieve(string aliasId)
  {
    var response = await _vault.Client.Get<RandomResponse>($"/vault/static/{_vault.VaultId}/random/{aliasId}");
    response.Data = _vault.Decrypt(response.Iv, response.AuthTag, response.Data);
    return response;
  }

  public async Task Delete(string aliasId)
  {
    await _vault.Client.Delete($"/vault/static/{_vault.VaultId}/random/{aliasId}");
  }
}
}
