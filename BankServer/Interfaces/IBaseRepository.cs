using BankServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankServer.Interfaces
{
    public interface IBaseRepository<Model> where Model : BaseModel
    {
        public Task<bool> Create(Model item);
        public Task<Model> Update(Model item);
        public Task<bool> Delete(Model item);
        public Task<Model?> GetById(ulong id);
        public Task<List<Model>> Select();
    }
}
