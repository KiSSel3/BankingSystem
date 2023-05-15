using BankServer.Generators;
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
        private IUserRepository users;
        private IInvoiceRepository invoices;
        private ITransactionRepository transactions;

        //Services    || Выполняют основную логику
        private IInvoiceService invoiceService;
        private ITransactionService transactionService;
        private IRegistrationService registrationService;
        private IAuthorizationService authorizationService;

        //Listeners   || Прослушивают определённые порты
        InvoiceListener invoiceListener;
        RegistrationListener registrationListener;
        AuthorizationListener authorizationListener;
        TransactionListener transactionListener;

        //GeneratorsId
        IGeneratorId userGeneratorId;
        IGeneratorId invoiceGeneratorId;
        IGeneratorId transactionGeneratorId;

        //Generator Number for invoice
        IGeneratorNumberInvoice generatorNumberInvoice;

        //Encoder
        IEncoderService encoderService;

        public Bank(IUserRepository _users, IInvoiceRepository _invoices, ITransactionRepository _transactions,
            IInvoiceService _invoiceService, ITransactionService _transactionService, IRegistrationService _registrationService, IAuthorizationService _authorizationService,
            IGeneratorId _userGeneratorId, IGeneratorId _invoiceGeneratorId, IGeneratorId _transactionGeneratorId,
            IGeneratorNumberInvoice _generatorNumberInvoice,
            IEncoderService _encoderService)
        {
            users = _users;
            invoices = _invoices;
            transactions = _transactions;

            invoiceService = _invoiceService;
            transactionService = _transactionService;
            registrationService = _registrationService;
            authorizationService = _authorizationService;

            userGeneratorId = _userGeneratorId;
            invoiceGeneratorId = _invoiceGeneratorId;
            transactionGeneratorId = _transactionGeneratorId;

            generatorNumberInvoice = _generatorNumberInvoice;

            encoderService = _encoderService;

            invoiceListener = new InvoiceListener(8080, encoderService, invoices, invoiceService, invoiceGeneratorId, generatorNumberInvoice);
            registrationListener = new RegistrationListener(8081, encoderService, users, registrationService, userGeneratorId);
            authorizationListener = new AuthorizationListener(8082, encoderService, users, authorizationService);
            transactionListener = new TransactionListener(8083, encoderService, transactions, invoices, transactionService, transactionGeneratorId);

        }

        public void Start()
        {
            registrationListener.Start();
            authorizationListener.Start();
            invoiceListener.Start();
            transactionListener.Start();
        }

        public void Stop()
        {
            registrationListener.Stop();
            authorizationListener.Stop();
            invoiceListener.Stop();
            transactionListener.Stop();
        }
    }
}