using System.Threading.Tasks;

namespace Nullafi
{
    /// <summary>
    /// 
    /// </summary>
    public class NullafiSDK
    {
        private readonly string _apiKey;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="apiKey"></param>
        public NullafiSDK(string apiKey)
        {
            _apiKey = apiKey;
        }

        /// <summary>
        /// 
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
