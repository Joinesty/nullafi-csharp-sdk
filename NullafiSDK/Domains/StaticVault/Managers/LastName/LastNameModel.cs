using System;
using System.Collections.Generic;
using System.Text;

namespace NullafiSDK.Domains.StaticVault.Managers.LastName
{
    public class LastNameModel
    {
        public string Id { get; set; }
        public string LastName { get; set; }
        public string LastNameToken { get; set; }
        public string Iv { get; set; }
        public string AuthTag { get; set; }
        public string Tags { get; set; }
    }
}
