using BankServer.Interfaces;
using BankServer.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankServer.Repository
{
    public class Repository<Model> : IRepository<Model> where Model : BaseModel
    {
        private List<Model> models = new List<Model>();

        public Model Add(Model model)
        {
            models.Add(model);
            return model;
        }

        public Model Delete(int id)
        {
            var toDelete = models.FirstOrDefault(item => item.Id == id);
            models.Remove(toDelete);
            return toDelete;
        }

        public Model Get(int id)
        {
            return models.FirstOrDefault(item => item.Id == id);
        }

        public List<Model> GetAll()
        {
            return models;
        }
    }
}
