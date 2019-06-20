using System.Collections.Generic;

namespace Nullafi.Domains.StaticVault.Managers.Gender
{
    /// <summary>
    /// 
    /// </summary>
    public class GenderRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string GenderHash { get; set; }

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
