using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NullafiSDK.Models.Tokens
{
    public class LastNameModel
    {
        public string Id { get; set; }
        [JsonProperty(PropertyName = "lastname")]
        public string LastName { get; set; }
        [JsonProperty(PropertyName = "lastnameToken")]
        public string LastNameToken { get; set; }
        public string Iv { get; set; }
        public string AuthTag { get; set; }
        public string Tags { get; set; }
    }
}
