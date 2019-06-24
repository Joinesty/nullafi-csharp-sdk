using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nullafi.Domains.StaticVault.Managers.PlaceOfBirth
{
    /// <summary>
    /// 
    /// </summary>
    public class PlaceOfBirthRequest
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "placeofbirth")]
        public string PlaceOfBirth { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "placeofbirthHash")]
        public string PlaceOfBirthHash { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Iv { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AuthTag { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<string> Tags { get; set; }
    }
}
