using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nullafi.Domains.StaticVault.Managers.VehicleRegistration
{
    /// <summary>
    /// 
    /// </summary>
    public class VehicleRegistrationRequest
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "vehicleregistration")]
        public string VehicleRegistration { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "vehicleregistrationHash")]
        public string VehicleRegistrationHash { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Iv { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AuthTag { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<string> Tags { get; set; }
    }
}
