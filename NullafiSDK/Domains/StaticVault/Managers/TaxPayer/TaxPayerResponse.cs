using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Nullafi.Domains.StaticVault.Managers.TaxPayer
{
    /// <summary>
    /// 
    /// </summary>
    public class TaxPayerResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "taxpayer")]
        public string TaxPayer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "taxpayerAlias")]
        public string TaxPayerAlias { get; set; }

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
