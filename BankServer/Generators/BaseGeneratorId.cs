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
        private ulong currentId = 0;

        public ulong Next()
        {
            return ++currentId;
        }

        public ulong Current()
        {
            return currentId;
        }
    }
}
