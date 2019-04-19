﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NullafiSDK.Models
{
    public class DateOfBirthModel
    {
        public string Id { get; set; }
        [JsonProperty(PropertyName = "dateofbirth")]
        public string DateOfBirth { get; set; }
        [JsonProperty(PropertyName = "dateofbirthToken")]
        public string DateOfBirthToken { get; set; }
        public string Iv { get; set; }
        public string AuthTag { get; set; }
        public string Tags { get; set; }

    }
}
