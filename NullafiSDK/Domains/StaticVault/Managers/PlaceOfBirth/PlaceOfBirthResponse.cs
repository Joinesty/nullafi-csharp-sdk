using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Nullafi.Domains.StaticVault.Managers.PlaceOfBirth
{
    /// <summary>
    /// 
    /// </summary>
    public class PlaceOfBirthResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "placeofbirth")]
        public string PlaceOfBirth { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "placeofbirthAlias")]
        public string PlaceOfBirthAlias { get; set; }

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

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime UpdatedAt { get; set; }
    }
}
