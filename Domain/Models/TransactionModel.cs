using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    [Serializable]
    public class TransactionModel : BaseModel
    {
        //Поля
        public InvoiceModel Sender { get; set; }
        public ulong SenderId { get; set; }

        public InvoiceModel Recipient { get; set; }
        public ulong RecipientId { get; set; }

        public decimal Amount { get; set; }
        public DateTime Time { get; set; }


        //Методы
        public TransactionModel()
        {
            Recipient = null;
            Sender = null;
            Amount = 0;

            Time = DateTime.Now;
        }

        public TransactionModel(InvoiceModel recipient, InvoiceModel sender, decimal amount)
        {
            Recipient = recipient;
            Sender = sender;
            Amount = amount;

            Time = DateTime.Now;
        }

        public override bool Equals(object? obj)
        {
            if (obj is TransactionModel transactionModel)
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
