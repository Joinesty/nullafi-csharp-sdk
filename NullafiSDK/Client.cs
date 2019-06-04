using NullafiSDK.Models;
using NullafiSDK.Services;
using System;
using System.Threading.Tasks;

namespace NullafiSDK
{
    public class Client: API
    {
        private string sessionToken;
        public string HashKey { get; private set; }

        public async void Authenticate(string apiKey)
        {
            var payload = new AuthenticationPayload() { ApiKey = apiKey };
            var response = await this.Post<AuthenticationPayload, AuthenticationResponse>("/authentication/token", payload);

            this.HashKey = response.HashKey;
            SetSessionToken(response.Token);
        }
    }
}