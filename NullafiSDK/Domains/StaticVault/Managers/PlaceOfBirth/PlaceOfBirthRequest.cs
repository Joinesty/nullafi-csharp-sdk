using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nullafi.Domains.StaticVault.Managers.PlaceOfBirth
{
    public class PlaceOfBirthRequest
    {
        [JsonProperty(PropertyName = "placeofbirth")]
        public string PlaceOfBirth { get; set; }
        [JsonProperty(PropertyName = "placeofbirthHash")]
        public string PlaceOfBirthHash { get; set; }
        public string Iv { get; set; }
        public string AuthTag { get; set; }
        public List<string> Tags { get; set; }
    }
}
