using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankServer.Request
{
    [Serializable]
    public class BaseRequest<T>
    {
        public BaseRequest()
        {
            Path = default(string);
            Data = default(T);
        }

        public BaseRequest(string path, T data)
        {
            Path = path;
            Data = data;
        }

        public string Path { get; set; }
        public T Data { get; set; } 
    }
}