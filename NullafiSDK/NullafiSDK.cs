using System;

namespace NullafiSDK
{
    public class NullafiSDK
    {
        readonly string apiKey;

        public NullafiSDK(string apiKey)
        {
            this.apiKey = apiKey;
        }

        public Client CreateClient()
        {
            var client = new Client();
            client.Authenticate(this.apiKey);
            return client;
        }
    }
}
