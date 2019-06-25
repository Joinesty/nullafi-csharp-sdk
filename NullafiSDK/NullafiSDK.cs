using System.Threading.Tasks;

namespace Nullafi
{
    /// <summary>
    /// NullafiSDK class
    /// </summary>
    public class NullafiSDK
    {
        private readonly string _apiKey;

        /// <summary>
        /// Create an instance of NullafiSDK
        /// </summary>
        /// <param name="apiKey"></param>
        public NullafiSDK(string apiKey)
        {
            _apiKey = apiKey;
        }

        /// <summary>
        /// Create a new instance of client
        /// </summary>
        /// <returns></returns>
        public async Task<Client> CreateClient()
        {
            var client = new Client();
            await client.Authenticate(_apiKey);
            return client;
        }
    }
}
