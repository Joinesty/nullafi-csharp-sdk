using System;
using System.Collections.Generic;
using System.Text;

namespace Nullafi.Domains.StaticVault.Managers.Race
{
    public class RaceResponse
    {
        public string Id { get; set; }
        public string Race { get; set; }
        public string RaceAlias { get; set; }
        public string Iv { get; set; }
        public string AuthTag { get; set; }
        public List<string> Tags { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
