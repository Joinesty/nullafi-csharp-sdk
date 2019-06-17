using System.Collections.Generic;

namespace Nullafi.Domains.StaticVault.Managers.Address
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class AddressRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AddressHash { get; set; }

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
