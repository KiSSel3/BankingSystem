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
        public BaseResponse()
        {
            Status = default(bool);
            Data = default(T);
        }

        public BaseResponse(bool status, T? data)
        {
            Status = status;
            Data = data;
        }

        public bool Status { get; set; }
        public T? Data { get; set; }
    }
}
