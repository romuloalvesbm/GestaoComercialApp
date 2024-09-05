﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoComercial.Infra.External.Identity.Settings
{
    public class JwtTokenSettings
    {
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public string? SecurityKey { get; set; }
        public string? JwtClaimNamesSub { get; set; }
        public string? ExpirationInHours { get; set; }
    }
}
