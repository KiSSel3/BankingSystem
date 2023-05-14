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
        //private IRepository<UserModel> users;
        //private IRepository<InvoiceModel> invoices;

        //Services    || Выполняют основную логику
        private IUserService userService;
        private IInvoiceService invoiceService;
        private ITransactionService transactionService;
        private IRegistrationService registrationService;
        private IAuthorizationService authorizationService;

        //Listeners   || Прослушивают определённые порты
        //UserListener userListener;
        //InvoiceListener invoiceListener;
        RegistrationListener registrationListener;
        AuthorizationListener authorizationListener;

        //GeneratorsId
        IGeneratorId userGeneratorId;
        IGeneratorId invoiceGeneratorId;
        IGeneratorId transactionGeneratorId;

        //Generator Number for invoice
        IGeneratorNumberInvoice generatorNumberInvoice;

        //Encoder
        IEncoderService encoderService;

/*        public Bank(IRepository<UserModel> _users, IRepository<InvoiceModel> _invoices,
            IUserService _userService, IInvoiceService _invoiceService, ITransactionService _transactionService,
            IRegistrationService _registrationService, IAuthorizationService _authorizationService,
            IGeneratorId _userGeneratorId, IGeneratorId _invoiceGeneratorId, IGeneratorId _transactionGeneratorId, IEncoderService _encoderService,
            IGeneratorNumberInvoice _generatorNumberInvoice)
        {
            users = _users;
            invoices = _invoices;
            userService = _userService;

            invoiceService = _invoiceService;
            transactionService = _transactionService;
            registrationService = _registrationService;
            authorizationService = _authorizationService;

            userGeneratorId = _userGeneratorId;
            invoiceGeneratorId = _invoiceGeneratorId;
            transactionGeneratorId = _transactionGeneratorId;

            encoderService = _encoderService;

            generatorNumberInvoice = _generatorNumberInvoice;

            userListener = new UserListener(8080, encoderService, users, userService, userGeneratorId);
            registrationListener = new RegistrationListener(8081, encoderService, users, registrationService, userGeneratorId);
            authorizationListener = new AuthorizationListener(8082, encoderService, users, authorizationService, userService);
            invoiceListener = new InvoiceListener(8083, encoderService, invoices, invoiceService, invoiceGeneratorId, generatorNumberInvoice);
        }*/

        public void Start()
        {
            //userListener.Start();
            registrationListener.Start();
            authorizationListener.Start();
            //invoiceListener.Start();
        }

        public void Stop()
        {
            //userListener.Stop();
            registrationListener.Stop();
            authorizationListener.Stop();
            //invoiceListener.Stop();
        }
    }
}
