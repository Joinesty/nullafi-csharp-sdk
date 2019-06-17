using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Nullafi.Domains.StaticVault.Managers.DateOfBirth
{
    /// <summary>
    /// 
    /// </summary>
    public class DateOfBirthResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "dateofbirth")]
        public string DateOfBirth { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "dateofbirthAlias")]
        public string DateOfBirthAlias { get; set; }

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
