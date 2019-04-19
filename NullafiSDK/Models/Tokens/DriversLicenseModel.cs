using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NullafiSDK.Models.Tokens
{
    public class DriversLicenseModel
    {
       public string Id { get; set; }
        [JsonProperty(PropertyName = "driverslicense")]
        public string DriversLicense { get; set; }
        [JsonProperty(PropertyName = "driverslicenseToken")]
        public string DriversLicenseToken { get; set; }
        public string Iv { get; set; }
        public string AuthTag { get; set; }
        public string Tags { get; set; }
    }
}
