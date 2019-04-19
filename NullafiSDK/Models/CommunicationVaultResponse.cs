using System;
using System.Collections.Generic;
using System.Text;

namespace NullafiSDK.Models
{
    class CommunicationVaultResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string PublicKey { get; set; }
        public string Iv { get; set; }
        public string AuthTag { get; set; }
        public string VaultMasterKey { get; set; }
        public string SessionKey { get; set; }
        public List<string> Tags { get; set; }
    }
}
