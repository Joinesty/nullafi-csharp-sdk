using System;
using System.Collections.Generic;
using System.Text;

namespace NullafiSDK.Models
{
    public class AddressModel
    {
        public string Id { get; set; }
        public string Address { get; set; }
        public string Iv { get; set; }
        public string AuthTag { get; set; }
        public string Tags { get; set; }
    }
}
