using BankServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankServer.Interfaces
{
    public interface IAuthorizationService
    {
        public bool IsUserRegistered(IRepository<UserModel> users, UserModel user);
    }
}
