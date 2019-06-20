using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Nullafi.Domains.StaticVault.Managers.DriversLicense
{
    /// <summary>
    /// 
    /// </summary>
    public class DriversLicenseResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "driverslicense")]
        public string DriversLicense { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "driverslicenseAlias")]
        public string DriversLicenseAlias { get; set; }

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

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime UpdatedAt { get; set; }
    }
}
