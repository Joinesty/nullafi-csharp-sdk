using System.Collections.Generic;

namespace Nullafi.Domains.CommunicationVault
{
    internal class CommunicationVaultResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Iv { get; set; }
        public string AuthTag { get; set; }
        public string MasterKey { get; set; }
        public string SessionKey { get; set; }
        public List<string> Tags { get; set; }
    }
}
