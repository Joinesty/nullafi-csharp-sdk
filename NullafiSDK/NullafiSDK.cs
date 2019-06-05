using System;
using System.Threading.Tasks;

namespace Nullafi
{
    public class NullafiSDK
    {
        readonly string apiKey;

        public NullafiSDK(string apiKey)
        {
            this.apiKey = apiKey;
        }

        public async Task<Client> CreateClient()
        {
            var client = new Client();
            await client.Authenticate(this.apiKey);
            return client;
        }
    }
}
