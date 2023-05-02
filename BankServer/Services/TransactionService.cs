using BankServer.Interfaces;
using BankServer.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankServer.Services
{
    public class TransactionService : ITransactionService
    {
        public TransactionModel Transaction(decimal id, InvoiceModel recipient, InvoiceModel sender, decimal amount)
        {
            recipient.Balanse -= amount;
            sender.Balanse += amount;

            return new TransactionModel(id, recipient, sender, amount);
        }
    }
}
