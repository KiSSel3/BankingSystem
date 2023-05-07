using BankServer.Interfaces;
using BankServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankServer.Services
{
    public class RegistrationService : IRegistrationService
    {
        public void AddUser(IRepository<UserModel> users, UserModel user)
        {
            users.Add(user);
        }

        public bool IsNewName(IRepository<UserModel> users, string name)
        {
            return !users.GetAll().Any(item => item.Name == name);
        }
    }
}
