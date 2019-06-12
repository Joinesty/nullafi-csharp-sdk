using System.Collections.Generic;

namespace Nullafi.Domains.StaticVault.Managers.Ssn
{
    public class SsnRequest
    {
        public string Ssn { get; set; }
        public string SsnHash { get; set; }
        public string Iv { get; set; }
        public string AuthTag { get; set; }
        public List<string> Tags { get; set; }
    }
}
