﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NullafiSDK.Domains.StaticVault.Managers.Race
{
    public class RaceModel
    {
        public string Id { get; set; }
        public string Race { get; set; }
        public string RaceToken { get; set; }
        public string RaceHash { get; set; }
        public string Iv { get; set; }
        public string AuthTag { get; set; }
        public string Tags { get; set; }
    }
}
