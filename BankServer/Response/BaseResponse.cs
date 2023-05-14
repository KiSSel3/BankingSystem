using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankServer.Response
{
    [Serializable]
    public class BaseResponse<T>
    {
        public bool Status { get; set; }
        public T? Data { get; set; }
    }
}
