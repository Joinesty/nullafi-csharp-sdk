using System;
using System.Collections.Generic;
using System.Text;

namespace NullafiSDK.Domains.StaticVault.Managers.Ssn
{
    public class SsnModel
    {
        public string Id { get; set; }
        public string Ssn { get; set; }
        public string SsnToken { get; set; }
        public string Iv { get; set; }
        public string AuthTag { get; set; }
        public string Tags { get; set; }
    }
}
