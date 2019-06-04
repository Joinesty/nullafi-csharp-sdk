using System;
using System.Collections.Generic;
using System.Text;

namespace NullafiSDK.Domains.StaticVault.Managers.Passport
{
    public class PassportModel
    {
        public string Id { get; set; }
        public string Passport { get; set; }
        public string PassportToken { get; set; }
        public string Iv { get; set; }
        public string AuthTag { get; set; }
        public string Tags { get; set; }
    }
}
