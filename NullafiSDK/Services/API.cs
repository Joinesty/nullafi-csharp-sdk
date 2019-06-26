using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Nullafi.Services
{
    /// <summary>
    /// Api
    /// </summary>
    public abstract class Api
    {
        private static readonly HttpClient Client = new HttpClient();
        private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Ignore,
        };

        /// <summary>
        /// Create an instance of Api
        /// </summary>
        /// <returns></returns>

        internal Api()
        {
            if (Client.BaseAddress == null)
            {
                var apiUrl = System.Environment.GetEnvironmentVariable("NULLAFI_API_URL");
                if (string.IsNullOrWhiteSpace(apiUrl))
                {
                    apiUrl = "https://enterprise-api.nullafi.com";
                }

                Client.BaseAddress = new Uri(apiUrl);
            }

            if (!Client.DefaultRequestHeaders.Contains("Accept"))
            {
                Client.DefaultRequestHeaders.Add("Accept", "application/json");
            }
        }

        internal void SetSessionAlias(string sessionToken)
        {
            if (Client.DefaultRequestHeaders.Contains("Authorization"))
            {
                Client.DefaultRequestHeaders.Remove("Authorization");
            }

            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {sessionToken}");
        }

        internal async Task<TResponse> Get<TResponse>(string path)
        {
            return JsonConvert.DeserializeObject<TResponse>(await Client.GetStringAsync(path));
        }

        internal async Task<TResponse> Post<TPayload, TResponse>(string path, TPayload data)
        {
            var content = new StringContent(JsonConvert.SerializeObject(data, SerializerSettings), Encoding.UTF8, "application/json");
            var response = await Client.PostAsync(path, content);
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<TResponse>(await response.Content.ReadAsStringAsync(), SerializerSettings);
        }

        internal async Task Delete(string path)
        {
            var response = await Client.DeleteAsync(path);
            response.EnsureSuccessStatusCode();
        }
    }
}
