using System;
using System.Collections.Generic;

namespace Nullafi.Domains.StaticVault.Managers.Random
{
    public class RandomResponse
    {
        public string Id { get; set; }
        public string Data { get; set; }
        public string Alias { get; set; }
        public string Iv { get; set; }
        public string AuthTag { get; set; }
        public List<string> Tags { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
