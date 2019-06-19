using System.Collections.Generic;

namespace Nullafi.Domains.StaticVault.Managers.Race
{
    /// <summary>
    /// 
    /// </summary>
    public class RaceRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public string Race { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RaceHash { get; set; }

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
