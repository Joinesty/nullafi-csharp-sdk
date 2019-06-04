using System;
using System.Collections.Generic;
using System.Text;

namespace NullafiSDK.Domains.StaticVault.Managers.DateOfBirth
{
    public class DateOfBirthModel
    {
        public string Id { get; set; }
        public string DateOfBirth { get; set; }
        public string DateOfBirthToken { get; set; }
        public string Iv { get; set; }
        public string AuthTag { get; set; }
        public string Tags { get; set; }
    }
}
