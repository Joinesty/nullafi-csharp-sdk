using Nullafi.Services.Crypto;

namespace Nullafi
{

    /// <summary>
    /// Security
    /// </summary>
    public sealed class Security
    {
        /// <summary>
        /// Aesgcm
        /// </summary>
        public Aesgcm Aes {get; }

        /// <summary>
        /// RSA 
        /// </summary>
        public RSA RSA {get; }

        /// <summary>
        /// Hmac
        /// </summary>
        public Hmac Hmac {get; }

        /// <summary>
        /// Create an instance of Security
        /// </summary>
        public Security()
        {
            Aes = new Aesgcm();
            RSA = new RSA();
            Hmac = new Hmac();
        }
    }
}
