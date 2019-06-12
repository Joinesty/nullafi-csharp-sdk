using System.Threading.Tasks;

namespace Nullafi
{
    public class NullafiSDK
    {
        private readonly string _apiKey;

        public NullafiSDK(string apiKey)
        {
            _apiKey = apiKey;
        }

        public async Task<Client> CreateClient()
        {
            var client = new Client();
            await client.Authenticate(_apiKey);
            return client;
        }
    }
}
