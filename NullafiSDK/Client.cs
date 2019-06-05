using Nullafi.Domains.CommunicationVault;
using Nullafi.Domains.StaticVault;
using Nullafi.Models;
using Nullafi.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nullafi
{
    public class Client : API
    {
        public string HashKey { get; private set; }

        public async Task Authenticate(string apiKey)
        {
            var payload = new AuthenticationPayload() { ApiKey = apiKey };
            var response = await this.Post<AuthenticationPayload, AuthenticationResponse>("/authentication/token", payload);

            this.HashKey = response.HashKey;
            SetSessionAlias(response.Token);
        }

        public async Task<StaticVault> CreateStaticVault(string name, List<string> tags)
        {
            return await StaticVault.CreateStaticVault(this, name, null);
        }

        public async Task<StaticVault> RetrieveStaticVault(string vaultId, string masterKey)
        {
            return await StaticVault.RetrieveStaticVault(this, vaultId, masterKey);
        }

        public async Task<CommunicationVault> CreateCommunicationVault(string name, List<string> tags)
        {
            return await CommunicationVault.CreateCommunicationVault(this, name, null);
        }

        public async Task<CommunicationVault> RetrieveCommunicationVault(string vaultId, string masterKey)
        {
            return await CommunicationVault.RetrieveCommunicationVault(this, vaultId, masterKey);
        }
    }
}