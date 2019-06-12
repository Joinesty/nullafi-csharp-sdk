using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nullafi.Domains.StaticVault.Managers.DateOfBirth
{
    public class DateOfBirthManager
    {
        private readonly StaticVault _vault;

        public DateOfBirthManager(StaticVault vault)
        {
            _vault = vault;
        }

        public async Task<DateOfBirthResponse> Create(string dateOfBirth, List<string> tags, int? year, int? month)
        {
            var result = _vault.Encrypt(dateOfBirth);
            var payload = new DateOfBirthRequest
            {
                DateOfBirth = result.EncryptedData,
                DateOfBirthHash = _vault.Hash(dateOfBirth),
                Tags = tags,
                Iv = result.Iv,
                AuthTag = result.AuthTag
            };

            var url = $"/vault/static/{_vault.VaultId}/dateofbirth";
            if (year != null) url += $"year={year}&";
            if (month != null) url += $"month={month}";

            var response = await _vault.Client.Post<DateOfBirthRequest, DateOfBirthResponse>(url, payload);
            response.DateOfBirth = _vault.Decrypt(response.Iv, response.AuthTag, response.DateOfBirth);
            return response;
        }

        public async Task<DateOfBirthResponse> Retrieve(string aliasId)
        {
            var response = await _vault.Client.Get<DateOfBirthResponse>($"/vault/static/{_vault.VaultId}/dateofbirth/{aliasId}");
            response.DateOfBirth = _vault.Decrypt(response.Iv, response.AuthTag, response.DateOfBirth);
            return response;
        }

        public async Task Delete(string aliasId)
        {
            await _vault.Client.Delete($"/vault/static/{_vault.VaultId}/dateofbirth/{aliasId}");
        }
    }
}
