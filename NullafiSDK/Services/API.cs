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
    public class API
    {
        private static readonly HttpClient client = new HttpClient();
        private static readonly JsonSerializerSettings serializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
        };

        public API()
        {
            if (client.BaseAddress == null)
            {
                client.BaseAddress = new Uri("http://localhost:5000");
            }

            if (!client.DefaultRequestHeaders.Contains("Accept"))
            {
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            }
        }

        protected void SetSessionAlias(string sessionToken)
        {
            if (client.DefaultRequestHeaders.Contains("Authorization"))
            {
                client.DefaultRequestHeaders.Remove("Authorization");
            }

            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {sessionToken}");
        }

        private string GetQueryString(object obj)
        {
            var properties = from p in obj.GetType().GetProperties()
                             where p.GetValue(obj, null) != null
                             select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

            return String.Join("&", properties.ToArray());
        }

        public async Task<TResponse> Get<TResponse>(string path)
        {
            return JsonConvert.DeserializeObject<TResponse>(await client.GetStringAsync(path));
        }

        public async Task<TResponse> Patch<TPayload, TResponse>(string path, TPayload data)
        {
            var content = new StringContent(JsonConvert.SerializeObject(data, serializerSettings), Encoding.UTF8, "application/json");
            var response = await client.PatchAsync(path, content);
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<TResponse>(await response.Content.ReadAsStringAsync(), serializerSettings);
        }

        public async Task<TResponse> Post<TPayload, TResponse>(string path, TPayload data)
        {
            var content = new StringContent(JsonConvert.SerializeObject(data, serializerSettings), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(path, content);
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<TResponse>(await response.Content.ReadAsStringAsync(), serializerSettings);
        }

        public async Task<TResponse> Put<TPayload, TResponse>(string path, TPayload data)
        {
            var content = new StringContent(JsonConvert.SerializeObject(data, serializerSettings), Encoding.UTF8, "application/json");
            var response = await client.PutAsync(path, content);
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<TResponse>(await response.Content.ReadAsStringAsync(), serializerSettings);
        }

        public async Task Delete(string path)
        {
            var response = await client.DeleteAsync(path);
            response.EnsureSuccessStatusCode();
        }
    }
}
