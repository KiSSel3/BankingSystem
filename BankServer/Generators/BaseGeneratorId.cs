using BankServer.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankServer.Generators
{
    public class BaseGeneratorId : IGeneratorId
    {
        private decimal currentId = 0;

        public decimal Next()
        {
            return ++currentId;
        }

        public decimal Current()
        {
            return currentId;
        }
    }
}
