using MoS.Services.UserServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
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
                new Claim(ClaimTypes.Name, info.Username),
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

        public void ExtractToken(TokenInfo token, Action<TokenProps> onSuccess, Action<AuthenticationExceptionMessage> onFail)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var jsonToken = tokenHandler.ReadToken(token.Token);
                var info = jsonToken as JwtSecurityToken;

                onSuccess(new TokenService.TokenProps
                {
                    Username = info.Claims.First(claim => claim.Type == ClaimTypes.Name).Value,
                    Role = info.Claims.First(claim => claim.Type == ClaimTypes.Role).Value
                });
            }
            catch (Exception)
            {
                onFail(AuthenticationExceptionMessage.UNAUTHORIZED);
            }
        }
    }
}
