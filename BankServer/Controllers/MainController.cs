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

        private IRepository<UserModel> users;
        private IRepository<InvoiceModel> invoices;

        public async Task Route(List<string> data)
        {



        }
    }
}
