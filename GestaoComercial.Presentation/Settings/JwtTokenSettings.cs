namespace GestaoComercial.Presentation.Settings
{
    public class JwtTokenSettings
    {
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string SecurityKey { get; set; } = string.Empty;
        public string JwtClaimNamesSub { get; set; } = string.Empty;
        public string ExpirationInHours { get; set; } = string.Empty;
    }
}
