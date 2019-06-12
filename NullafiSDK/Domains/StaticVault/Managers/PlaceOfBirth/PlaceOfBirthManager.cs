using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Nullafi.Domains.StaticVault;

namespace Nullafi.Domains.StaticVault.Managers.PlaceOfBirth
{
    public class PlaceOfBirthManager
    {
        StaticVault vault;

        public PlaceOfBirthManager(StaticVault vault)
        {
            this.vault = vault;
        }

        public async Task<PlaceOfBirthResponse> Create(string placeofbirth, List<string> tags, string state)
        {
            var result = this.vault.Encrypt(placeofbirth);
            var payload = new PlaceOfBirthRequest
            {
                PlaceOfBirth = result.EncryptedData,
                PlaceOfBirthHash = this.vault.Hash(placeofbirth),
                Tags = tags,
                Iv = result.Iv,
                AuthTag = result.AuthTag
            };

            String url = $"/vault/static/{this.vault.VaultId}/placeofbirth";
            if (state != null) url += $"/{state}";

            var response = await this.vault.client.Post<PlaceOfBirthRequest, PlaceOfBirthResponse>(url, payload);
            response.PlaceOfBirth = this.vault.Decrypt(response.Iv, response.AuthTag, response.PlaceOfBirth);
            return response;
        }

        public async Task<PlaceOfBirthResponse> Retrieve(string aliasId)
        {
            var response = await this.vault.client.Get<PlaceOfBirthResponse>($"/vault/static/{this.vault.VaultId}/placeofbirth/{aliasId}");
            response.PlaceOfBirth = this.vault.Decrypt(response.Iv, response.AuthTag, response.PlaceOfBirth);
            return response;
        }

        public async Task Delete(string aliasId)
        {
            await this.vault.client.Delete($"/vault/static/{this.vault.VaultId}/placeofbirth/{aliasId}");
        }
    }
}
