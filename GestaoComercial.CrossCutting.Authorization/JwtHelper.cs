using GestaoComercial.CrossCutting.Authorization.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GestaoComercial.CrossCutting.Authorization
{
    public class JwtHelper
    {
        private readonly JwtTokenSettingsIdentity _jwtSettings;

        public JwtHelper(IOptions<JwtTokenSettingsIdentity> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public ClaimsPrincipal? GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.SecurityKey!);

            // Parâmetros de validação do token
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _jwtSettings.Issuer,
                ValidateAudience = true,
                ValidAudience = _jwtSettings.Audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = true, // Validar se o token não expirou
                ClockSkew = TimeSpan.Zero // Tolerância de tempo zero para expiração
            };

            try
            {
                // Valida o token e retorna o ClaimsPrincipal
                var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

                // Verifica se o token é um JWT válido
                if (validatedToken is JwtSecurityToken jwtToken && jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    return principal;
                }
            }
            catch (SecurityTokenException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }

            return null;
        }

        public List<Claim> ListClaimValue(ClaimsPrincipal principal, string claimType)
        {
            return principal.Claims.Where(c => c.Type == claimType).ToList();
        }
    }    
}
