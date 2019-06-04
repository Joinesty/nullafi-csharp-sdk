using System;
using System.Collections.Generic;
using System.Text;

namespace NullafiSDK.Domains.StaticVault.Managers.TaxPayer
{
    public class TaxPayerModel
    {
        public string Id { get; set; }
        public string TaxPayer { get; set; }
        public string TaxPayerToken { get; set; }
        public string Iv { get; set; }
        public string AuthTag { get; set; }
        public string Tags { get; set; }
    }
}
