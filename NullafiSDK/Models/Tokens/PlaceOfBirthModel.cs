using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NullafiSDK.Models.Tokens
{
    public class PlaceOfBirthModel
    {
        public string Id { get; set; }
        [JsonProperty(PropertyName = "placeofbirth")]
        public string PlaceOfBirth { get; set; }
        [JsonProperty(PropertyName = "placeofbirthToken")]
        public string PlaceOfBirthToken { get; set; }
        public string Iv { get; set; }
        public string AuthTag { get; set; }
        public string Tags { get; set; }
    }
}
