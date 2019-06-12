using System.Collections.Generic;

namespace Nullafi.Domains.StaticVault.Managers.Gender
{
    public class GenderRequest
    {
        public string Gender { get; set; }
        public string GenderHash { get; set; }
        public string Iv { get; set; }
        public string AuthTag { get; set; }
        public List<string> Tags { get; set; }
    }
}
