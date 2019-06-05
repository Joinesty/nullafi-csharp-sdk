using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nullafi.Domains.StaticVault.Managers.TaxPayer
{
    public class TaxPayerResponse
    {
        public string Id { get; set; }
        [JsonProperty(PropertyName = "taxpayer")]
        public string TaxPayer { get; set; }
        [JsonProperty(PropertyName = "taxpayerAlias")]
        public string TaxPayerAlias { get; set; }
        public string Iv { get; set; }
        public string AuthTag { get; set; }
        public List<string> Tags { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
