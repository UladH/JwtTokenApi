using App.Contracts.Models;

namespace App.Contracts.Interfaces
{
    public interface IUserService
    {
        Task<bool> Register(UserRegisterModel registerModel);
        Task<TokenModel> Login(UserLoginModel userLoginModel);
    }
}
