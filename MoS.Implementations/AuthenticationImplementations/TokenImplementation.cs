using MoS.Services.UserServices;
using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using static MoS.Services.UserServices.TokenService;
using static MoS.Models.Constants.Enums.Exception;

namespace MoS.Implementations.AuthenticationImplementations
{
    public class TokenImplementation : ITokenService
    {
        private readonly IConfiguration _config;

        public TokenImplementation(IConfiguration config)
        {
            _config = config;
        }

        private const double EXPIRY_DURATION_MINUTES = 30;

        public string BuildToken(TokenService.TokenProps info)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, info.Id.ToString()),
                new Claim(ClaimTypes.Role, info.Role)
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"].ToString()));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new JwtSecurityToken(
                                        _config["Jwt:Issuer"],
                                        _config["Jwt:Issuer"],
                                        claims,
                                        expires: DateTime.Now.AddMinutes(EXPIRY_DURATION_MINUTES),
                                        signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        public void ExtractToken(TokenInfo token, Action<TokenProps> onSuccess, Action<Guid> onFail)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var jsonToken = tokenHandler.ReadToken(token.Token);
                var info = jsonToken as JwtSecurityToken;

                onSuccess(new TokenService.TokenProps
                {
                    Id = new Guid(info.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value),
                    Role = info.Claims.First(claim => claim.Type == ClaimTypes.Role).Value
                });
            }
            catch (Exception)
            {
                onFail(AuthenticationExceptionMessages["UNAUTHORIZED"]);
            }
        }
    }
}
