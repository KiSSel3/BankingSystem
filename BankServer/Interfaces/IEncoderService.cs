using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankServer.Interfaces
{
    public interface IEncoderService
    {
        public string Encript(string text, string key);
        public string Decrypt(string text, string key);
    }
}