using System.Collections.Generic;

namespace Nullafi.Domains.StaticVault.Managers.Passport
{
    /// <summary>
    /// 
    /// </summary>
    public class PassportRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public string Passport { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PassportHash { get; set; }

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
