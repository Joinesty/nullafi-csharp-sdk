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
                Client.BaseAddress = new Uri("http://localhost:5000");
            }

            if (!Client.DefaultRequestHeaders.Contains("Accept"))
            {
                Client.DefaultRequestHeaders.Add("Accept", "application/json");
            }
        }

        protected void SetSessionAlias(string sessionToken)
        {
            if (Client.DefaultRequestHeaders.Contains("Authorization"))
            {
                Client.DefaultRequestHeaders.Remove("Authorization");
            }

            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {sessionToken}");
        }

        private string GetQueryString(object obj)
        {
            var properties = from p in obj.GetType().GetProperties()
                             where p.GetValue(obj, null) != null
                             select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

            return string.Join("&", properties.ToArray());
        }

        internal async Task<TResponse> Get<TResponse>(string path)
        {
            return JsonConvert.DeserializeObject<TResponse>(await Client.GetStringAsync(path));
        }

        internal async Task<TResponse> Patch<TPayload, TResponse>(string path, TPayload data)
        {
            var content = new StringContent(JsonConvert.SerializeObject(data, SerializerSettings), Encoding.UTF8, "application/json");
            var response = await Client.PatchAsync(path, content);
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<TResponse>(await response.Content.ReadAsStringAsync(), SerializerSettings);
        }

        internal async Task<TResponse> Post<TPayload, TResponse>(string path, TPayload data)
        {
            var content = new StringContent(JsonConvert.SerializeObject(data, SerializerSettings), Encoding.UTF8, "application/json");
            var response = await Client.PostAsync(path, content);
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<TResponse>(await response.Content.ReadAsStringAsync(), SerializerSettings);
        }

        internal async Task<TResponse> Put<TPayload, TResponse>(string path, TPayload data)
        {
            var content = new StringContent(JsonConvert.SerializeObject(data, SerializerSettings), Encoding.UTF8, "application/json");
            var response = await Client.PutAsync(path, content);
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
