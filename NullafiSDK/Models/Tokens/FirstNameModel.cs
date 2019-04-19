using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NullafiSDK.Models.Tokens
{
    public class FirstNameModel
    {
        public string Id { get; set; }
        [JsonProperty(PropertyName = "firstname")]
        public string FirstName { get; set; }
        [JsonProperty(PropertyName = "firstnameToken")]
        public string FirstNameToken { get; set; }
        public string Iv { get; set; }
        public string AuthTag { get; set; }
        public string Tags { get; set; }

    }
}
