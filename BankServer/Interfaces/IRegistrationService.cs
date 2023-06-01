using BankServer.Models;
using BankServer.Response;

namespace BankServer.Interfaces
{
    public interface IRegistrationService
    {
        public Task<BaseResponse<UserModel>> Registration(IUserRepository users, UserModel user);
    }
}