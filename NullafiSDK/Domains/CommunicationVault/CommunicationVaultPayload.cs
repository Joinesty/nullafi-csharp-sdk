using System.Collections.Generic;

namespace Nullafi.Domains.CommunicationVault
{
    internal class CommunicationVaultPayload
    {
        public string Name { get; set; }
        public string PublicKey { get; set; }
        public List<string> Tags { get; set; }
    }
}
