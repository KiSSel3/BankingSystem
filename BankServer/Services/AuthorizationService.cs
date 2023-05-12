using BankServer.Interfaces;
using BankServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankServer.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        public bool IsUserRegistered(IRepository<UserModel> users, string userName, string userPassword)
        {
            return users.GetAll().Any(item => item.Name == userName && item.Password == userPassword);
        }
    }
}
