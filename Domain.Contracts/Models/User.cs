using Microsoft.AspNetCore.Identity;

namespace Domain.Contracts.Models
{
    public class User: IdentityUser
    {
        public List<RefreshToken> RefreshTokens { get; set; }
    }
}
