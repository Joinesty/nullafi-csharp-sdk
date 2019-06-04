using System;
using System.Collections.Generic;
using System.Text;

namespace NullafiSDK.Domains.StaticVault
{
    class StaticVaultResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Tags { get; set; }
    }
}
