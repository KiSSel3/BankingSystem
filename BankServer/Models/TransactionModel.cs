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

        public TransactionModel(ulong id, InvoiceModel recipient, InvoiceModel sender, decimal amount)
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

        public override bool Equals(object? obj)
        {
            if(obj is TransactionModel transactionModel)
            {
                return (Sender.Equals(transactionModel.Sender)) && (Recipient.Equals(transactionModel.Recipient)) && (Amount.Equals(transactionModel.Amount)) && (Time.Equals(transactionModel.Time));
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Sender.GetHashCode() + Recipient.GetHashCode() + Amount.GetHashCode() + Time.GetHashCode();
        }
    }
}
