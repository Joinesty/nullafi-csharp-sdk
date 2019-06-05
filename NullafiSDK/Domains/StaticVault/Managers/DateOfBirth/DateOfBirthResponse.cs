using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nullafi.Domains.StaticVault.Managers.DateOfBirth
{
    public class DateOfBirthResponse
    {
        public string Id { get; set; }
        [JsonProperty(PropertyName = "dateofbirth")]
        public string DateOfBirth { get; set; }
        [JsonProperty(PropertyName = "dateofbirthAlias")]
        public string DateOfBirthAlias { get; set; }
        public string Iv { get; set; }
        public string AuthTag { get; set; }
        public List<string> Tags { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
