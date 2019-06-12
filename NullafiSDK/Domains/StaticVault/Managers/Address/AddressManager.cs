using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nullafi.Domains.StaticVault.Managers.Address
{
    public class AddressManager
    {
        private readonly StaticVault _vault;

        public AddressManager(StaticVault vault)
        {
            _vault = vault;
        }

        public async Task<AddressResponse> Create(string address, List<string> tags, string state)
        {
            var result = _vault.Encrypt(address);
            var payload = new AddressRequest
            {
                Address = result.EncryptedData,
                AddressHash = _vault.Hash(address),
                Tags = tags,
                Iv = result.Iv,
                AuthTag = result.AuthTag
            };

            var url = $"/vault/static/{_vault.VaultId}/address";
            if (state != null) url += $"/{state}";

            var response = await _vault.Client.Post<AddressRequest, AddressResponse>(url, payload);
            response.Address = _vault.Decrypt(response.Iv, response.AuthTag, response.Address);
            return response;
        }

        public async Task<AddressResponse> Retrieve(string aliasId)
        {
            var response = await _vault.Client.Get<AddressResponse>($"/vault/static/{_vault.VaultId}/address/{aliasId}");
            response.Address = _vault.Decrypt(response.Iv, response.AuthTag, response.Address);
            return response;
        }

        public async Task Delete(string aliasId)
        {
            await _vault.Client.Delete($"/vault/static/{_vault.VaultId}/address/{aliasId}");
        }
    }
}
