using Domain.Contracts.Models;
using Microsoft.AspNetCore.Identity;

namespace Domain.Contracts.Interfaces.Identity
{
    public interface IAppSignInManager
    {
        Task<SignInResult> CheckPasswordSignInAsync(User user, string password, bool lockoutOnFailure);
    }
}
