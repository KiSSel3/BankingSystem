using BankServer.Interfaces;
using BankServer.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankServer.Services
{
    public class UserService : IUserService
    {
        public string GetFullInformation(UserModel user)
        {
            return $"{user.Id}//{user.Name}//{user.Password}";
        }

        public decimal GetUserId(UserModel user)
        {
            return user.Id;
        }

        public string GetUserName(UserModel user)
        {
            return user.Name;
        }

        public string GetUserPassword(UserModel user)
        {
            return user.Password;
        }
    }
}
