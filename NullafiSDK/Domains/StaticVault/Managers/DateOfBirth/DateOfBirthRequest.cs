using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nullafi.Domains.StaticVault.Managers.DateOfBirth
{
    public class DateOfBirthRequest
    {
        [JsonProperty(PropertyName = "dateofbirth")]
        public string DateOfBirth { get; set; }
        [JsonProperty(PropertyName = "dateofbirthHash")]
        public string DateOfBirthHash { get; set; }
        public string Iv { get; set; }
        public string AuthTag { get; set; }
        public List<string> Tags { get; set; }
    }
}
