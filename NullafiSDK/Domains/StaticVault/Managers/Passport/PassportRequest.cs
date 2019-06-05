using System;
using System.Collections.Generic;
using System.Text;

namespace Nullafi.Domains.StaticVault.Managers.Passport
{
    public class PassportRequest
    {
        public string Passport { get; set; }
        public string PassportHash { get; set; }
        public string Iv { get; set; }
        public string AuthTag { get; set; }
        public List<string> Tags { get; set; }
    }
}
