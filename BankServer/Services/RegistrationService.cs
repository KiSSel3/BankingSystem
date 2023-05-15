using BankServer.Interfaces;
using BankServer.Models;
using BankServer.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankServer.Services
{
    public class RegistrationService : IRegistrationService
    {
        public async Task<BaseResponse<UserModel>> Registration(IUserRepository users, UserModel user, IGeneratorId generatorId)
        {
            try
            {
                var wantedUser = await users.GetByName(user.Name);

                if (wantedUser is null)
                {
                    user.Id = generatorId.Next();
                    await users.Create(user);

                    return new BaseResponse<UserModel>(true, user);
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
