using System;
using System.Collections.Generic;

namespace Nullafi.Domains.StaticVault.Managers.Random
{
    /// <summary>
    /// 
    /// </summary>
    public class RandomResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Alias { get; set; }

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
