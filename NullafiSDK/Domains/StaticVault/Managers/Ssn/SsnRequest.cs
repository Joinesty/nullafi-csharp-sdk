using System.Collections.Generic;

namespace Nullafi.Domains.StaticVault.Managers.Ssn
{
    /// <summary>
    /// 
    /// </summary>
    public class SsnRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public string Ssn { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SsnHash { get; set; }

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
