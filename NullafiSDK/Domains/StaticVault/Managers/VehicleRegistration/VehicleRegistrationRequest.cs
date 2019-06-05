using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nullafi.Domains.StaticVault.Managers.VehicleRegistration
{
    public class VehicleRegistrationRequest
    {
        [JsonProperty(PropertyName = "vehicleregistration")]
        public string VehicleRegistration { get; set; }
        [JsonProperty(PropertyName = "vehicleregistrationHash")]
        public string VehicleRegistrationHash { get; set; }
        public string Iv { get; set; }
        public string AuthTag { get; set; }
        public List<string> Tags { get; set; }
    }
}
