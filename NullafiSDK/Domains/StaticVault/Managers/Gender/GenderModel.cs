using System;
using System.Collections.Generic;
using System.Text;

namespace NullafiSDK.Domains.StaticVault.Managers.Gender
{
    public class GenderModel
    {
        public string Id { get; set; }
        public string Gender { get; set; }
        public string GenderToken { get; set; }
        public string GenderHash { get; set; }
        public string Iv { get; set; }
        public string AuthTag { get; set; }
        public string Tags { get; set; }
    }
}
