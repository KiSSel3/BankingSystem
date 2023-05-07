using BankServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankServer.Interfaces
{
    public interface IRegistrationService
    {
        public bool IsNewName(IRepository<UserModel> users, string name);
        public void AddUser(IRepository<UserModel> users, UserModel user);
    }
}
