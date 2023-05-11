using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankServer.Interfaces
{
    public interface IGeneratorNumberInvoice
    {
        public string Next();
        public string Current();
    }
}
