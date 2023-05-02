using BankServer.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankServer.Interfaces
{
    public interface IRepository<Model> where Model : BaseModel 
    {
        public List<Model> GetAll();
        public Model Get(int id);
        public Model Add(Model model);
        public Model Delete(int id);
    }
}
