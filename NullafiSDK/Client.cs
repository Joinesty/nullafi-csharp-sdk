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
        public string HashKey { get; private set; }

        /// <summary>
        /// Authenticate the client API
        /// </summary>
        /// <param name="apiKey"></param>
        /// <returns></returns>
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
        /// <returns></returns>
        public async Task<StaticVault> CreateStaticVault(string name, List<string> tags)
        {
            return await StaticVault.CreateStaticVault(this, name, null);
        }

        /// <summary>
        /// retrieve an existing static vault
        /// </summary>
        /// <param name="vaultId"></param>
        /// <param name="masterKey"></param>
        /// <returns></returns>
        public async Task<StaticVault> RetrieveStaticVault(string vaultId, string masterKey)
        {
            return await StaticVault.RetrieveStaticVault(this, vaultId, masterKey);
        }

        /// <summary>
        /// Create a new communication vault
        /// </summary>
        /// <param name="name"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        public async Task<CommunicationVault> CreateCommunicationVault(string name, List<string> tags)
        {
            return await CommunicationVault.CreateCommunicationVault(this, name, null);
        }

        /// <summary>
        /// retrieve an existing communication vault
        /// </summary>
        /// <param name="vaultId"></param>
        /// <param name="masterKey"></param>
        /// <returns></returns>
        public async Task<CommunicationVault> RetrieveCommunicationVault(string vaultId, string masterKey)
        {
            return await CommunicationVault.RetrieveCommunicationVault(this, vaultId, masterKey);
        }
    }
}