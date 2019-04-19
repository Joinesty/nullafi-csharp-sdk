using System;
using System.Collections.Generic;
using System.Text;

namespace NullafiSDK.Models.Tokens
{
    public class RandomModel
    {
        public string Id { get; set; }
        public string Data { get; set; }
        public string Token { get; set; }
        public string Iv { get; set; }
        public string AuthTag { get; set; }
        public string Tags { get; set; }
    }
}
