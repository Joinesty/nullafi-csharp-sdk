using Nullafi.Services.Crypto;

namespace Nullafi
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Security
    {
        /// <summary>
        /// 
        /// </summary>
        public Aesgcm Aes {get; }

        /// <summary>
        /// 
        /// </summary>
        public RSA RSA {get; }

        /// <summary>
        /// 
        /// </summary>
        public Hmac Hmac {get; }

        /// <summary>
        /// 
        /// </summary>
        public Security()
        {
            Aes = new Aesgcm();
            RSA = new RSA();
            Hmac = new Hmac();
        }
    }
}
