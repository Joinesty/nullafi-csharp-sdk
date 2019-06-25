using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nullafi.Domains.StaticVault.Managers.DateOfBirth
{
    /// <summary>
    /// AddressManager
    /// </summary>
    public class DateOfBirthManager
    {
        private readonly StaticVault _vault;

        /// <summary>
        /// Create an instance of DateOfBirthManager
        /// </summary>
        /// <param name="vault"></param>
        public DateOfBirthManager(StaticVault vault)
        {
            _vault = vault;
        }

        /// <summary>
        /// Create a new DateOfBirth string to be aliased within static vault
        /// </summary>
        /// <param name="dateOfBirth"></param>
        /// <param name="tags"></param>
        /// <returns>Returns a promise containing: id, dateOfBirth, dateOfBirthAlias, tags, iv, authTag, tags, createdAt</returns>
        public async Task<DateOfBirthResponse> Create(string dateOfBirth, List<string> tags)
        {
            return await this.Create(dateOfBirth, null, null, tags);
        }

        /// <summary>
        /// Create a new DateOfBirth string to be aliased within static vault
        /// </summary>
        /// <param name="dateOfBirth"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="tags"></param>
        /// <returns>Returns a promise containing: id, dateOfBirth, dateOfBirthAlias, tags, iv, authTag, tags, createdAt</returns>
        public async Task<DateOfBirthResponse> Create(string dateOfBirth, int? year = null, int? month = null, List<string> tags = null)
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

            var url = $"/vault/static/{_vault.VaultId}/dateofbirth?";
            if (year != null) url += $"year={year}";
            if (month != null) url += year != null ? "&" : "" + $"month={month}";

            var response = await _vault.Client.Post<DateOfBirthRequest, DateOfBirthResponse>(url, payload);
            response.DateOfBirth = _vault.Decrypt(response.Iv, response.AuthTag, response.DateOfBirth);
            return response;
        }

        /// <summary>
        /// Retrieve the Address string alias from a static vault.
        /// </summary>
        /// <remarks>
        /// <para>Returns an array of matching values.</para>
        /// <para>Array will be sorted by date created.</para>
        /// </remarks>
        /// <param name="aliasId"></param>
        /// <returns>Returns a promise containing: id, dateOfBirth, dateOfBirthAlias, tags, iv, authTag, tags, createdAt</returns>
        public async Task<DateOfBirthResponse> Retrieve(string aliasId)
        {
            var response = await _vault.Client.Get<DateOfBirthResponse>($"/vault/static/{_vault.VaultId}/dateofbirth/{aliasId}");
            response.DateOfBirth = _vault.Decrypt(response.Iv, response.AuthTag, response.DateOfBirth);
            return response;
        }

        /// <summary>
        /// Retrieve the Date of Birth alias from real date of birth.
        /// Real value must be an exact match and will also be case sensitive.
        /// Returns an array of matching values.Array will be sorted by date created.
        /// </summary>
        /// <param name="dateOfBirth"></param>
        /// <param name="tags"></param>
        /// <returns>Returns a promise containing: id, dateOfBirth, dateOfBirthAlias, tags, iv, authTag, tags, createdAt</returns>
        public async Task<DateOfBirthResponse> RetrieveFromRealData(string dateOfBirth, List<string> tags = null)
        {
            var hash = this._vault.Hash(dateOfBirth);
            var url = $"/vault/static/dateofbirth?hash={hash}";

            if (tags != null)
            {
                url += $"&tags={string.Join("&tags=", tags)}";
            }

            var response = await _vault.Client.Get<DateOfBirthResponse>(url);
            response.DateOfBirth = _vault.Decrypt(response.Iv, response.AuthTag, response.DateOfBirth);
            return response;
        }

        /// <summary>
        /// Delete the Address alias from static vault
        /// </summary>
        /// <param name="aliasId"></param>
        /// <returns>Returns a promise containing: ok</returns>
        public async Task Delete(string aliasId)
        {
            await _vault.Client.Delete($"/vault/static/{_vault.VaultId}/dateofbirth/{aliasId}");
        }
    }
}
