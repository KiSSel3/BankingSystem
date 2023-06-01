using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankServer.Interfaces
{
    public interface IBaseGenerator
    {
        public string GetNextValue();
        public string GetCurrentValue();
        public void AddFreeItem(string item);
    }
}
