using BankServer.Interfaces;
using BankServer.Listeners;
using BankServer.Models;
using BankServer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankServer.Serever
{
    public class Bank
    {
        //Repositorys || В будущем будут заменены на базу данных
        private IRepository<UserModel> users;
        private IRepository<InvoiceModel> invoices;

        //Services    || Выполняют основную логику
        private IUserService userService;
        private IInvoiceService invoiceService;
        private ITransactionService transactionService;
        private IRegistrationService registrationService;

        //Listeners   || Прослушивают определённые порты
        UserListener userListener;
        RegistrationListener registrationListener;

        public Bank(IRepository<UserModel> _users, IRepository<InvoiceModel> _invoices,
            IUserService _userService, IInvoiceService _invoiceService, ITransactionService _transactionService,
            IRegistrationService _registrationService)
        {
            users = _users;
            invoices = _invoices;
            userService = _userService;
            invoiceService = _invoiceService;
            transactionService = _transactionService;
            registrationService = _registrationService;

            userListener = new UserListener(8080, users, userService);
            registrationListener = new RegistrationListener(8081, users, registrationService);
        }

        public void Start()
        {
            userListener.Start();
            registrationListener.Start();
        }

        public void Stop()
        {
            userListener.Stop();
            registrationListener.Stop();
        }
    }
}
