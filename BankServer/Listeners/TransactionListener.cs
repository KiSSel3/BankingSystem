using BankServer.Interfaces;
using BankServer.Models;
using BankSerializer;

using BankServer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BankServer.Listeners
{
    public class TransactionListener : BaseListener
    {
        private IRepository<TransactionModel> transactions;
        private IRepository<InvoiceModel> invoices;
        private ITransactionService transactionService;
        private IGeneratorId transactionGeneratorId;

        public Serializer bankSerializer;

        public TransactionService(int port, IEncoderService encoderService, IRepository<TransactionModel> _transactions, IRepository<InvoiceModel> _invoices, ITransactionService _transactionService, IGeneratorId _transactionGeneratorId) : base(port, encoderService)
        {
            transactions = _transactions;
            invoices = _invoices;
            transactionService = _transactionService;
            transactionGeneratorId = _transactionGeneratorId;

            bankSerializer = new();
        }

        protected override Task HandleClientAsync(TcpClient client)
        {
            using (client)
            {
                stream = client.GetStream();

                GetRequest();
                var senderInvoiceNumber = request;

                while (true)
                {
                    
                }


                stream = null;
            }
        }

        /*        public InvoiceModel Sender { get; set; }
        public InvoiceModel Recipient { get; set; }
        public decimal Amount { get; set; }
        public DateTime Time { get; set; }*/
    }
}
