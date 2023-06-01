using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BankServer.Models
{
    [Serializable]
    public class InvoiceModel : BaseModel
    {
        //Поля
        public string Number { get; set; }
        public decimal Balanse { get; set; }
        public UserModel InvoiceUser { get; set; }

        public ulong InvoiceUserId { get; set; }

        //Методы
        public InvoiceModel()
        {
            Number = "None";
            Balanse = 0;
            InvoiceUser = new();
        }

        public InvoiceModel(UserModel invoiceUser, string number, decimal balanse = 0)
        {
            Number = number;
            Balanse = balanse;
            InvoiceUser = invoiceUser;
        }

        public override bool Equals(object? obj)
        {
            if (obj is InvoiceModel invoiceModel)
            {
                return Number == invoiceModel.Number;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Number.GetHashCode();
        }
    }
}