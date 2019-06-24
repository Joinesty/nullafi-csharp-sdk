using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nullafi.Domains.StaticVault.Managers.TaxPayer
{
    /// <summary>
    /// 
    /// </summary>
    public class TaxPayerRequest
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "taxpayer")]
        public string TaxPayer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "taxpayerHash")]
        public string TaxPayerHash { get; set; }

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
