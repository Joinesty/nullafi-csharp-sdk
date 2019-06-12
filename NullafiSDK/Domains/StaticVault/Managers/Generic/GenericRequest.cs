using System.Collections.Generic;

namespace Nullafi.Domains.StaticVault.Managers.Generic
{
    public class GenericRequest
    {
        public string Data { get; set; }
        public string DataHash { get; set; }
        public string Iv { get; set; }
        public string AuthTag { get; set; }
        public List<string> Tags { get; set; }
    }
}
