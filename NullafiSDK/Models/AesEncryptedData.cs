using System;
using System.Collections.Generic;
using System.Text;

namespace NullafiSDK.Models
{
    public class AesEncryptedData
    {
        public string EncryptedData { get; set; }
        public string AuthTag { get; set; }
        public string Iv { get; set; }
    }
}
