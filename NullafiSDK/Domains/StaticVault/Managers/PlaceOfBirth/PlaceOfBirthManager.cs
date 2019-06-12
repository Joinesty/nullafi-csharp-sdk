using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nullafi.Domains.StaticVault.Managers.PlaceOfBirth
{
    public class PlaceOfBirthManager
    {
        private readonly StaticVault _vault;

        public PlaceOfBirthManager(StaticVault vault)
        {
            _vault = vault;
        }

        public async Task<PlaceOfBirthResponse> Create(string placeofbirth, List<string> tags)
        {
            return await this.Create(placeofbirth, null, tags);
        }

        public async Task<PlaceOfBirthResponse> Create(string placeofbirth, string state = null, List<string> tags = null)
        {
            var result = _vault.Encrypt(placeofbirth);
            var payload = new PlaceOfBirthRequest
            {
                PlaceOfBirth = result.EncryptedData,
                PlaceOfBirthHash = _vault.Hash(placeofbirth),
                Tags = tags,
                Iv = result.Iv,
                AuthTag = result.AuthTag
            };

            var url = $"/vault/static/{_vault.VaultId}/placeofbirth";
            if (state != null) url += $"/{state}";

            var response = await _vault.Client.Post<PlaceOfBirthRequest, PlaceOfBirthResponse>(url, payload);
            response.PlaceOfBirth = _vault.Decrypt(response.Iv, response.AuthTag, response.PlaceOfBirth);
            return response;
        }

        public async Task<PlaceOfBirthResponse> Retrieve(string aliasId)
        {
            var response = await _vault.Client.Get<PlaceOfBirthResponse>($"/vault/static/{_vault.VaultId}/placeofbirth/{aliasId}");
            response.PlaceOfBirth = _vault.Decrypt(response.Iv, response.AuthTag, response.PlaceOfBirth);
            return response;
        }

        public async Task Delete(string aliasId)
        {
            await _vault.Client.Delete($"/vault/static/{_vault.VaultId}/placeofbirth/{aliasId}");
        }
    }
}
