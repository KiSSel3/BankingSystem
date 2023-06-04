using Domain.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankServer.Interfaces
{
    public interface IUserRepository : IBaseRepository<UserModel>
    {
        public Task<UserModel?> GetByName(string name);
    }
}