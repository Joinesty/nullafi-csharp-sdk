using System;
using System.Collections.Generic;
using System.Text;

namespace NullafiSDK.Domains.StaticVault.Managers.FirstName
{
    public class FirstNameModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string FirstNameToken { get; set; }
        public string Iv { get; set; }
        public string AuthTag { get; set; }
        public string Tags { get; set; }
    }
}
