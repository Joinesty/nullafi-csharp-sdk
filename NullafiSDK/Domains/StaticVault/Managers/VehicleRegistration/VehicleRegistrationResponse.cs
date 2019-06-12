using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Nullafi.Domains.StaticVault.Managers.VehicleRegistration
{
    public class VehicleRegistrationResponse
    {
        public string Id { get; set; }
        [JsonProperty(PropertyName = "vehicleregistration")]
        public string VehicleRegistration { get; set; }
        [JsonProperty(PropertyName = "vehicleregistrationAlias")]
        public string VehicleRegistrationAlias { get; set; }
        public string Iv { get; set; }
        public string AuthTag { get; set; }
        public List<string> Tags { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
