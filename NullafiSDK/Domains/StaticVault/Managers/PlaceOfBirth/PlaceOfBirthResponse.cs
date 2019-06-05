using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nullafi.Domains.StaticVault.Managers.PlaceOfBirth
{
    public class PlaceOfBirthResponse
    {
        public string Id { get; set; }
        [JsonProperty(PropertyName = "placeofbirth")]
        public string PlaceOfBirth { get; set; }
        [JsonProperty(PropertyName = "placeofbirthAlias")]
        public string PlaceOfBirthAlias { get; set; }
        public string Iv { get; set; }
        public string AuthTag { get; set; }
        public List<string> Tags { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
