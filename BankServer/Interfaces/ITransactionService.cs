using BankServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BankServer.Interfaces
{
    public interface ITransactionService
    {
        /*int id, InvoiceModel recipient, InvoiceModel sender, decimal amount*/
        public TransactionModel Transaction(decimal id, InvoiceModel recipient, InvoiceModel sender, decimal amount);



    }
}
