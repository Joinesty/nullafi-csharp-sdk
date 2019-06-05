using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nullafi.Domains.StaticVault.Managers.TaxPayer
{
    public class TaxPayerRequest
    {
        [JsonProperty(PropertyName = "taxpayer")]
        public string TaxPayer { get; set; }
        [JsonProperty(PropertyName = "taxpayerHash")]
        public string TaxPayerHash { get; set; }
        public string Iv { get; set; }
        public string AuthTag { get; set; }
        public List<string> Tags { get; set; }
    }
}
