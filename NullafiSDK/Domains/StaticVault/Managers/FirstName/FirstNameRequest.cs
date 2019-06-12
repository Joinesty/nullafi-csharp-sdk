﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nullafi.Domains.StaticVault.Managers.FirstName
{
    public class FirstNameRequest
    {
        [JsonProperty(PropertyName = "firstname")]
        public string FirstName { get; set; }
        [JsonProperty(PropertyName = "firstnameHash")]
        public string FirstNameHash { get; set; }
        public string Iv { get; set; }
        public string AuthTag { get; set; }
        public List<string> Tags { get; set; }
    }
}