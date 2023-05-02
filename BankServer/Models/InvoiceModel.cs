using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BankServer.Models
{
    public class InvoiceModel : BaseModel
    {
        public InvoiceModel()
        {
            Id = 0;
            Number = "None";
            Balanse = 0;
            InvoiceUser = new();
        }

        public InvoiceModel(UserModel invoiceUser, decimal id, string number, decimal balanse = 0)
        {
            Id = id;
            Number = number;
            Balanse = balanse;
            InvoiceUser = invoiceUser;
        }

        public string Number { get; set; }
        public decimal Balanse { get; set; }
        public UserModel InvoiceUser { get; set; }
    }
}
