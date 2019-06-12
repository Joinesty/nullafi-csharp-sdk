using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nullafi.Domains.StaticVault.Managers.FirstName
{
    public class FirstNameManager
    {
        private readonly StaticVault _vault;

        public FirstNameManager(StaticVault vault)
        {
            _vault = vault;
        }

        public async Task<FirstNameResponse> Create(string firstname, List<string> tags)
        {
            return await this.Create(firstname, null, tags);
        }

        public async Task<FirstNameResponse> Create(string firstname, string gender = null, List<string> tags = null)
        {
            var result = _vault.Encrypt(firstname);
            var payload = new FirstNameRequest
            {
                FirstName = result.EncryptedData,
                FirstNameHash = _vault.Hash(firstname),
                Tags = tags,
                Iv = result.Iv,
                AuthTag = result.AuthTag
            };

            var url = $"/vault/static/{_vault.VaultId}/firstname";
            if (gender != null) url += $"/{gender}";

            var response = await _vault.Client.Post<FirstNameRequest, FirstNameResponse>(url, payload);
            response.FirstName = _vault.Decrypt(response.Iv, response.AuthTag, response.FirstName);
            return response;
        }

        public async Task<FirstNameResponse> Retrieve(string aliasId)
        {
            var response = await _vault.Client.Get<FirstNameResponse>($"/vault/static/{_vault.VaultId}/firstname/{aliasId}");
            response.FirstName = _vault.Decrypt(response.Iv, response.AuthTag, response.FirstName);
            return response;
        }

        public async Task Delete(string aliasId)
        {
            await _vault.Client.Delete($"/vault/static/{_vault.VaultId}/firstname/{aliasId}");
        }
    }
}
