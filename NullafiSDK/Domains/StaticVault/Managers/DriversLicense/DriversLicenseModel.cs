using System;
using System.Collections.Generic;
using System.Text;

namespace NullafiSDK.Domains.StaticVault.Managers.DriversLicense
{
    public class DriversLicenseModel
    {
        public string Id { get; set; }
        public string DriversLicense { get; set; }
        public string DriversLicenseToken { get; set; }
        public string Iv { get; set; }
        public string AuthTag { get; set; }
        public string Tags { get; set; }
    }
}
