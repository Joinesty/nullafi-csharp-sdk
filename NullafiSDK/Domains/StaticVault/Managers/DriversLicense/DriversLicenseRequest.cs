using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nullafi.Domains.StaticVault.Managers.DriversLicense
{
    /// <summary>
    /// 
    /// </summary>
    public class DriversLicenseRequest
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "driverslicense")]
        public string DriversLicense { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "driverslicenseHash")]
        public string DriversLicenseHash { get; set; }

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
