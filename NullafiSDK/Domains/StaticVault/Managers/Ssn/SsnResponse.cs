﻿using System;
using System.Collections.Generic;

namespace Nullafi.Domains.StaticVault.Managers.Ssn
{
    public class SsnResponse
    {
        public string Id { get; set; }
        public string Ssn { get; set; }
        public string SsnAlias { get; set; }
        public string Iv { get; set; }
        public string AuthTag { get; set; }
        public List<string> Tags { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
