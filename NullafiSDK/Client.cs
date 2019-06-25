using Nullafi.Domains.CommunicationVault;
using Nullafi.Domains.StaticVault;
using Nullafi.Models;
using Nullafi.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nullafi
{
    /// <summary>
    /// Client class
    /// </summary>
    public class Client : Api
    {
        /// <summary>
        /// 
        /// </summary>
        public string HashKey { get; private set; }

        /// <summary>
        /// Authenticate the client API
        /// </summary>
        /// <param name="apiKey"></param>
        public async Task Authenticate(string apiKey)
        {
            var payload = new AuthenticationPayload() { ApiKey = apiKey };
            var response = await Post<AuthenticationPayload, AuthenticationResponse>("/authentication/token", payload);

            HashKey = response.HashKey;
            SetSessionAlias(response.Token);
        }

        /// <summary>
        /// Create a new static vault
        /// </summary>
        /// <param name="name"></param>
        /// <param name="tags"></param>
        /// <returns>Static vault object</returns>
        public async Task<StaticVault> CreateStaticVault(string name, List<string> tags = null)
        {
            return await StaticVault.CreateStaticVault(this, name, tags);
        }

        /// <summary>
        /// retrieve an existing static vault
        /// </summary>
        /// <param name="vaultId"></param>
        /// <param name="masterKey"></param>
        /// <returns>Static vault object</returns>
        public async Task<StaticVault> RetrieveStaticVault(string vaultId, string masterKey)
        {
            return await StaticVault.RetrieveStaticVault(this, vaultId, masterKey);
        }

        /// <summary>
        /// delete an existing static vault
        /// </summary>
        /// <param name="vaultId"></param>
        /// <returns></returns>
        public async Task DeleteStaticVault(string vaultId)
        {
            await StaticVault.DeleteStaticVault(this, vaultId);
        }

        /// <summary>
        /// Create a new communication vault
        /// </summary>
        /// <param name="name"></param>
        /// <param name="tags"></param>
        /// <returns>Communication vault object</returns>
        public async Task<CommunicationVault> CreateCommunicationVault(string name, List<string> tags = null)
        {
            return await CommunicationVault.CreateCommunicationVault(this, name, tags);
        }

        /// <summary>
        /// retrieve an existing communication vault
        /// </summary>
        /// <param name="vaultId"></param>
        /// <param name="masterKey"></param>
        /// <returns>Communication vault object</returns>
        public async Task<CommunicationVault> RetrieveCommunicationVault(string vaultId, string masterKey)
        {
            return await CommunicationVault.RetrieveCommunicationVault(this, vaultId, masterKey);
        }

        /// <summary>
        /// delete an existing communication vault
        /// </summary>
        /// <param name="vaultId"></param>
        /// <returns></returns>
        public async Task DeleteCommunicationVault(string vaultId)
        {
            await CommunicationVault.DeleteCommunicationVault(this, vaultId);
        }
    }
}