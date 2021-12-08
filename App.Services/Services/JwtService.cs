using App.Contracts.Interfaces;
using App.Contracts.Models;
using AppConfiguration;
using AppConfiguration.Constants;
using Domain.Contracts.Interfaces.Identity;
using Domain.Contracts.Interfaces.Repositories;
using Domain.Contracts.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace App.Services.Services
{
    public class JwtService: IJwtService
    {
        private IAppUserManager userManager;
        private IRepositoryManager repositoryManager;
        private IAppConfiguration configuration;

        #region constructor

        public JwtService(IServiceProvider provider)
        {
            configuration = provider.GetService<IAppConfiguration>();
            userManager = provider.GetService<IAppUserManager>();
            repositoryManager = provider.GetService<IRepositoryManager>();
        }

        #endregion

        #region public

        public async Task<TokenPairModel> CreateTokenPair(UserForJwtModel userForJwt)
        {
            var user = await userManager.FindByEmailAsync(userForJwt.Email);
            var token = GenerateJwtToken(userForJwt);
            var refreshToken = GenerateRefreshToken(user);

            repositoryManager.RefreshTokenRepository.Add(refreshToken);

            var tokenModel = new TokenPairModel()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo,
                RefreshToken = refreshToken.Token
            };

            repositoryManager.SaveChanges();
            return tokenModel;
        }

        #endregion

        #region private

        private JwtSecurityToken GenerateJwtToken(UserForJwtModel user)
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

            return token;
        }

        private RefreshToken GenerateRefreshToken(User user)
        {
            var tokenLength = configuration.Get<int>(TokenKeys.RefreshTokenLength);
            var tokenExpiryInSeconds = configuration.Get<int>(TokenKeys.RefreshTokenExpiryInSeconds);
            var token = GenerateRandomString(tokenLength);

            return new RefreshToken()
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddSeconds(configuration.Get<double>(TokenKeys.RefreshTokenExpiryInSeconds)),
                User = user
            };
        }

        private string GenerateRandomString(int length)
        {
            var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            byte[] randomBytes = new byte[length];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }

        #endregion
    }
}
