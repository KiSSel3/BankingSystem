using Domain.Models;
using Domain.Response;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankClient.Interfaces
{
    public interface IAuthorizationService
    {
        public Task<BaseResponse<UserModel>> Authorization(string name, string password, string ipAdress, int port);
    }
}
