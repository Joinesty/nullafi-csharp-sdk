using System;
using System.Collections.Generic;
using System.Text;

namespace NullafiSDK.Domains.StaticVault.Managers.PlaceOfBirth
{
    public class PlaceOfBirthModel
    {
        public string Id { get; set; }
        public string PlaceOfBirth { get; set; }
        public string PlaceOfBirthToken { get; set; }
        public string PlaceOfBirthHash { get; set; }
        public string Iv { get; set; }
        public string AuthTag { get; set; }
        public string Tags { get; set; }
    }
}
