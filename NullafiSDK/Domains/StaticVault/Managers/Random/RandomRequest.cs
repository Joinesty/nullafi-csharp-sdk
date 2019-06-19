using System.Collections.Generic;

namespace Nullafi.Domains.StaticVault.Managers.Random
{
    /// <summary>
    /// 
    /// </summary>
    public class RandomRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DataHash { get; set; }

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
