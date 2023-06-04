using Domain.Models;
using Domain.Response;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BankClient.Interfaces
{
    public interface IRegistrationService
    {
        public Task<BaseResponse<UserModel>> Registration(string name, string password, string ipAdress, int port);
    }
}
