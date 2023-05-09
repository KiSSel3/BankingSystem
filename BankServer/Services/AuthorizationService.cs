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
        public bool IsUserRegistered(IRepository<UserModel> users, UserModel user)
        {
            return users.GetAll().Any(item => item.Name == user.Name && item.Password == user.Password);
        }
    }
}
