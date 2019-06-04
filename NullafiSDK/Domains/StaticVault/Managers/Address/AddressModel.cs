using System;
using System.Collections.Generic;
using System.Text;

namespace NullafiSDK.Domains.StaticVault.Managers.Address
{
    public class AddressModel
    {
        public string Id { get; set; }
        public string Address { get; set; }
        public string AddressToken { get; set; }
        public string Iv { get; set; }
        public string AuthTag { get; set; }
        public string Tags { get; set; }
    }
}
