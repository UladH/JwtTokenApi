using App.Contracts.Interfaces;
using App.Contracts.Models;
using AppConfiguration;
using AppConfiguration.Constants;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace App.Services.Services
{
    public class JwtService: IJwtService
    {
        private IAppConfiguration configuration;

        #region constructor

        public JwtService(IServiceProvider provider)
        {
            configuration = provider.GetService<IAppConfiguration>();
        }

        #endregion

        #region public

        public TokenModel CreateToken(UserForJwtModel user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.Get(TokenKeys.Key)));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var token = new JwtSecurityToken(
                configuration.Get(TokenKeys.Issuer),
                configuration.Get(TokenKeys.Audience),
                claims,
                signingCredentials: credentials,
                expires: DateTime.UtcNow.AddSeconds(configuration.Get<double>(TokenKeys.ExpiryInSeconds)));

            var tokenModel = new TokenModel()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo
            };

            return tokenModel;
        }

        #endregion

    }
}
