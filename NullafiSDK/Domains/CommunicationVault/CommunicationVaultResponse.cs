using System;
using System.Collections.Generic;
using System.Text;

namespace Nullafi.Domains.CommunicationVault
{
    class CommunicationVaultResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string PublicKey { get; set; }
        public string Iv { get; set; }
        public string AuthTag { get; set; }
        public string MasterKey { get; set; }
        public string SessionKey { get; set; }
        public List<string> Tags { get; set; }
    }
}
