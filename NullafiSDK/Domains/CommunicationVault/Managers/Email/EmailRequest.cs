using System.Collections.Generic;

namespace Nullafi.Domains.CommunicationVault.Managers.Email
{
    /// <summary>
    /// 
    /// </summary>
    public class EmailRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string EmailHash { get; set; }

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
