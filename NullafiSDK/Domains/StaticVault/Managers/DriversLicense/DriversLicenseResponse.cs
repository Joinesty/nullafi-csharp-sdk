using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nullafi.Domains.StaticVault.Managers.DriversLicense
{
    public class DriversLicenseResponse
    {
        public string Id { get; set; }
        [JsonProperty(PropertyName = "driverslicense")]
        public string DriversLicense { get; set; }
        [JsonProperty(PropertyName = "driverslicenseAlias")]
        public string DriversLicenseAlias { get; set; }
        public string Iv { get; set; }
        public string AuthTag { get; set; }
        public List<string> Tags { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
