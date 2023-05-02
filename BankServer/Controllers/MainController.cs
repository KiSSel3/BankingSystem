using BankServer.Interfaces;
using BankServer.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankServer.Controllers
{
    public class MainController : IMainController
    {
        //<temporarily>
        private IRepository<TransactionModel> transactions;
        //<temporarily/>

        //Repository
        private IRepository<UserModel> users;
        private IRepository<InvoiceModel> invoices;

        //Services
        private IUserService userService;
        private IInvoiceService invoiceService;
        private ITransactionService transactionService;

        //Generators
        private IGeneratorId generatorUserId;
        private IGeneratorId generatorInvoiceId;
        private IGeneratorId generatorTransactionId;

        public MainController(IRepository<TransactionModel> transactions, IRepository<UserModel> users,
                              IRepository<InvoiceModel> invoices, IUserService userService, IInvoiceService invoiceService,
                              ITransactionService transactionService, IGeneratorId generatorUserId, IGeneratorId generatorInvoiceId,
                              IGeneratorId generatorTransactionId)
        {
            this.transactions = transactions;
            this.users = users;
            this.invoices = invoices;
            this.userService = userService;
            this.invoiceService = invoiceService;
            this.transactionService = transactionService;
            this.generatorUserId = generatorUserId;
            this.generatorInvoiceId = generatorInvoiceId;
            this.generatorTransactionId = generatorTransactionId;
        }

        public void AddUser(string name, string password)
        {

        }
    }
}
