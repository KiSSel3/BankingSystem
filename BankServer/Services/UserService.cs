using BankServer.Interfaces;
using BankServer.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BankServer.Services
{
    public class UserService : IUserService
    {
        public UserModel GetUser(IRepository<UserModel> users, string name)
        {
            return users.GetAll().First(item => item.Name == name);
        }

        public UserModel GetUser(IRepository<UserModel> users, ulong id)
        {
            return users.GetAll().First(item => item.Id == id);
        }
    }
}
