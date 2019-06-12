using System.Collections.Generic;

namespace Nullafi.Domains.StaticVault
{
    internal class StaticVaultResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Tags { get; set; }
    }
}
