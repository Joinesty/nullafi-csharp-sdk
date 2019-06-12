﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nullafi.Domains.StaticVault.Managers.DriversLicense
{
    public class DriversLicenseRequest
    {
        [JsonProperty(PropertyName = "driverslicense")]
        public string DriversLicense { get; set; }
        [JsonProperty(PropertyName = "driverslicenseHash")]
        public string DriversLicenseHash { get; set; }
        public string Iv { get; set; }
        public string AuthTag { get; set; }
        public List<string> Tags { get; set; }
    }
}