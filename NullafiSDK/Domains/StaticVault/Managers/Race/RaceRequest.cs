using System.Collections.Generic;

namespace Nullafi.Domains.StaticVault.Managers.Race
{
    public class RaceRequest
    {
        public string Race { get; set; }
        public string RaceHash { get; set; }
        public string Iv { get; set; }
        public string AuthTag { get; set; }
        public List<string> Tags { get; set; }
    }
}
