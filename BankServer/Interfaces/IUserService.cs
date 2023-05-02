using BankServer.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankServer.Interfaces
{
    public interface IUserService
    {
        public string GetFullInformation(UserModel user);
        public string GetUserName(UserModel user);
        public string GetUserPassword(UserModel user);
        public decimal GetUserId(UserModel user);
    }
}
