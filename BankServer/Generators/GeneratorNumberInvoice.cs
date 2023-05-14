using BankServer.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankServer.Generators
{
    public class GeneratorNumberInvoice : IGeneratorNumberInvoice
    {
        private ulong currentNumber = 0; 

        public string Current()
        {
            return string.Format("{0:0000 0000 0000 0000}", currentNumber);
        }

        public string Next()
        {
            return string.Format("{0:0000 0000 0000 0000}", ++currentNumber);
        }
    }
}
