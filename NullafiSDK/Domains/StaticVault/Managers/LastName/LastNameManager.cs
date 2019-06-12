using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nullafi.Domains.StaticVault.Managers.LastName
{
    public class LastNameManager
    {
        private readonly StaticVault _vault;

        public LastNameManager(StaticVault vault)
        {
            _vault = vault;
        }

        public async Task<LastNameResponse> Create(string lastname, List<string> tags, string gender)
        {
            var result = _vault.Encrypt(lastname);
            var payload = new LastNameRequest
            {
                LastName = result.EncryptedData,
                LastNameHash = _vault.Hash(lastname),
                Tags = tags,
                Iv = result.Iv,
                AuthTag = result.AuthTag
            };

            var url = $"/vault/static/{_vault.VaultId}/lastname";
            if (gender != null) url += $"/{gender}";

            var response = await _vault.Client.Post<LastNameRequest, LastNameResponse>(url, payload);
            response.LastName = _vault.Decrypt(response.Iv, response.AuthTag, response.LastName);
            return response;
        }

        public async Task<LastNameResponse> Retrieve(string aliasId)
        {
            var response = await _vault.Client.Get<LastNameResponse>($"/vault/static/{_vault.VaultId}/lastname/{aliasId}");
            response.LastName = _vault.Decrypt(response.Iv, response.AuthTag, response.LastName);
            return response;
        }

        public async Task Delete(string aliasId)
        {
            await _vault.Client.Delete($"/vault/static/{_vault.VaultId}/lastname/{aliasId}");
        }
    }
}
