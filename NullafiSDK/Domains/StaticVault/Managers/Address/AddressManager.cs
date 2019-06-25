using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nullafi.Domains.StaticVault.Managers.Address
{
    /// <summary>
    /// AddressManager
    /// </summary>
    public class AddressManager
    {
        private readonly StaticVault _vault;

        /// <summary>
        /// Create an instance of AddressManager
        /// </summary>
        /// <param name="vault"></param>
        public AddressManager(StaticVault vault)
        {
            _vault = vault;
        }

        /// <summary>
        /// Create a new Address string to be aliased for static vault
        /// </summary>
        /// <param name="address"></param>
        /// <param name="tags"></param>
        /// <returns>Returns a promise containing: address, addressAlias, tags, iv, authTag, tags, createdAt</returns>
        public async Task<AddressResponse> Create(string address, List<string> tags)
        {
            return await this.Create(address, null, tags);
        }

        /// <summary>
        /// Create a new Address string to be aliased for static vault
        /// </summary>
        /// <param name="address"></param>
        /// <param name="state"></param>
        /// <param name="tags"></param>
        /// <returns>Returns a promise containing: id, address, addressAlias, tags, iv, authTag, tags, createdAt</returns>
        public async Task<AddressResponse> Create(string address, string state = null, List<string> tags = null)
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

        /// <summary>
        /// Retrieve the Address string alias from a static vault.
        /// </summary>
        /// <remarks>
        /// <para>Returns an array of matching values.</para>
        /// <para>Array will be sorted by date created.</para>
        /// </remarks>
        /// <param name="aliasId"></param>
        /// <returns>Returns a promise containing: id, address, addressAlias, tags, iv, authTag, tags, createdAt</returns>
        public async Task<AddressResponse> Retrieve(string aliasId)
        {
            var response = await _vault.Client.Get<AddressResponse>($"/vault/static/{_vault.VaultId}/address/{aliasId}");
            response.Address = _vault.Decrypt(response.Iv, response.AuthTag, response.Address);
            return response;
        }

        /// <summary>
        /// Retrieve the Address alias from real address.
        /// Real value must be an exact match and will also be case sensitive.
        /// Returns an array of matching values.Array will be sorted by date created.
        /// </summary>
        /// <param name="address"></param>
        /// <param name="tags"></param>
        /// <returns>Returns a promise containing: id, address, addressAlias, tags, iv, authTag, tags, createdAt</returns>
        public async Task<List<AddressResponse>> RetrieveFromRealData(string address, List<string> tags = null)
        {
            var hash = this._vault.Hash(address);
            var url = $"/vault/static/address?hash={hash}";

            if (tags != null)
            {
                url += $"&tags={string.Join("&tags=", tags)}";
            }

            var responses = await _vault.Client.Get<List<AddressResponse>>(url);

            foreach (var response in responses)
            {
                response.Address = _vault.Decrypt(response.Iv, response.AuthTag, response.Address);
            }

            return responses;
        }

        /// <summary>
        /// Delete the Address alias from static vault
        /// </summary>
        /// <param name="aliasId"></param>
        /// <returns>Returns a promise containing: ok</returns>
        public async Task Delete(string aliasId)
        {
            await _vault.Client.Delete($"/vault/static/{_vault.VaultId}/address/{aliasId}");
        }
    }
}
