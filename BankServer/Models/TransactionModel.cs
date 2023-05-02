using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BankServer.Models
{
    public class TransactionModel : BaseModel
    {
        public TransactionModel()
        {
            Id = 0;
            Recipient = null;
            Sender = null;
            Amount = 0;

            Time = DateTime.Now;
        }

        public TransactionModel(int id, InvoiceModel recipient, InvoiceModel sender, decimal amount)
        {
            Id = id;
            Recipient = recipient;
            Sender = sender;
            Amount = amount;

            Time = DateTime.Now;
        }

        public InvoiceModel Sender { get; set; }
        public InvoiceModel Recipient { get; set; }
        public decimal Amount { get; set; }
        public DateTime Time { get; set; }
    }
}
