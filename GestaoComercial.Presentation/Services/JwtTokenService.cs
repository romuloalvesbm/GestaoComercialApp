using GestaoComercial.Application.Dtos;
using GestaoComercial.Application.Models;
using GestaoComercial.Presentation.Settings;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GestaoComercial.Presentation.Services
{
    public class JwtTokenService
    {
        private readonly JwtTokenSettings _jwtTokenSettings;

        public JwtTokenService(JwtTokenSettings jwtTokenSettings)
        {
            _jwtTokenSettings = jwtTokenSettings;
        }

        /// <summary>
        /// Método para criar e retornar um TOKEN JWT para um usuário
        /// </summary>
        public string CreateToken(UsuarioPermissaoDTO usuarioPermissao)
        {
            var expiration = DateTime.UtcNow.AddHours(int.Parse(_jwtTokenSettings.ExpirationInHours));
            var token = CreateJwtToken(
                CreateClaims(usuarioPermissao),
                CreateSigningCredentials(),
                expiration
            );
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        private JwtSecurityToken CreateJwtToken(List<Claim> claims, SigningCredentials credentials, DateTime expiration) =>
            new(
                _jwtTokenSettings.Issuer,
                _jwtTokenSettings.Audience,
                claims,
                expires: expiration,
                signingCredentials: credentials
            );

        private List<Claim> CreateClaims(UsuarioPermissaoDTO usuarioPermissao)
        {
            var jwtSub = _jwtTokenSettings.JwtClaimNamesSub;
            try
            {
                var claims = new List<Claim>
                {
                    new (JwtRegisteredClaimNames.Sub, jwtSub),
                    new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new (JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
                    new (ClaimTypes.NameIdentifier, usuarioPermissao.UsuarioId.ToString()),
                    new (ClaimTypes.Name, usuarioPermissao.Nome),
                    new (ClaimTypes.Email, usuarioPermissao.Email),
                };

                foreach (var item in usuarioPermissao.Permissoes)
                {
                    claims.Add(new Claim("CustomizePermission", item));
                }

                return claims;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private SigningCredentials CreateSigningCredentials()
        {
            var symmetricSecurityKey = _jwtTokenSettings.SecurityKey;

            return new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(symmetricSecurityKey)
                ),
                SecurityAlgorithms.HmacSha256
            );
        }
    }
}
