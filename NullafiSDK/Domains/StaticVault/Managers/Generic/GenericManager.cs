using System;
        using System.Collections.Generic;
        using System.Text;
        using System.Threading.Tasks;
        using NullafiSDK.Domains.StaticVault;

        namespace NullafiSDK.Domains.StaticVault.Managers.Generic
        {
public class GenericManager
{
    StaticVault vault;

    public GenericManager(StaticVault vault)
    {
        this.vault = vault;
    }

    public async Task<GenericModel> Create(string generic, List<string> tags)
{
    var result = this.vault.Encrypt(generic);
    var payload = new GenericModel
    {
        Generic = result.EncryptedData,
                Iv = result.Iv,
                AuthTag = result.AuthTag
    };

    var response = await this.vault.client.Post<GenericModel, GenericModel>($"/vault/static/${this.vault.VaultId}/generic", payload);
    return response;
}

    public async Task<GenericModel> Retrieve(string tokenId)
{
    return await this.vault.client.Get<GenericModel>($"/vault/static/{this.vault.VaultId}/generic/{tokenId}");
}

    public async void Delete(string tokenId)
{
    await this.vault.client.Delete($"/vault/static/{this.vault.VaultId}/generic/{tokenId}");
}
}
}
