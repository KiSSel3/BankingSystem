using Domain.Models;
using Domain.Response;

namespace BankServer.Interfaces
{
    public interface IAuthorizationService
    {
        public Task<BaseResponse<UserModel>> Authorization(IUserRepository users, UserModel user);
    }
}