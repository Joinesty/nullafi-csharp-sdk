using System;
using System.Collections.Generic;
using System.Text;

namespace NullafiSDK.Domains.StaticVault.Managers.VehicleRegistration
{
    public class VehicleRegistrationModel
    {
        public string Id { get; set; }
        public string VehicleRegistration { get; set; }
        public string VehicleRegistrationToken { get; set; }
        public string VehicleRegistrationHash { get; set; }
        public string Iv { get; set; }
        public string AuthTag { get; set; }
        public string Tags { get; set; }
    }
}
