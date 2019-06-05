using System;
using System.Collections.Generic;
using System.Text;

namespace Nullafi.Domains.CommunicationVault
{
    class CommunicationVaultPayload
    {
        public string Name { get; set; }
        public string PublicKey { get; set; }
        public List<string> Tags { get; set; }
    }
}
