using BankServer.Interfaces;
using Domain.Models;
using Domain.Response;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankServer.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        public async Task<BaseResponse<UserModel>> Authorization(IUserRepository users, UserModel user)
        {
            try
            {
                var wantedUser = await users.GetByName(user.Name);

                if(wantedUser is not null && wantedUser.Password == user.Password)
                {
                    return new BaseResponse<UserModel>(true, wantedUser);
                }

                return new BaseResponse<UserModel>(false, null);
            }
            catch
            {
                return new BaseResponse<UserModel>(false, null);
            }
        }
    }
}