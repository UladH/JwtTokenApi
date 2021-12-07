using Domain.Contracts.Models;
using Microsoft.AspNetCore.Identity;

namespace Domain.Contracts.Interfaces.Identity
{
    public interface IAppUserManager: IDisposable
    {
        Task<IdentityResult> CreateAsync(User user, string password);
        Task<User> FindByEmailAsync(string email);
        Task<User> FindByNameAsync(string userName);
    }
}
