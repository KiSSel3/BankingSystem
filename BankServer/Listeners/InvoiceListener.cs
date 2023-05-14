/*using BankServer.Interfaces;
using BankServer.Models;
using BankSerializer;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BankServer.Listeners
{
    public class InvoiceListener : BaseListener
    {
        private IRepository<InvoiceModel> invoices;
        private IInvoiceService invoiceService;
        private IGeneratorId invoiceGeneratorId;
        private IGeneratorNumberInvoice generatorNumberInvoice;

        private Serializer bankSerializer;

        public InvoiceListener(int port, IEncoderService encoderService, IRepository<InvoiceModel> _invoices, IInvoiceService _invoiceService, IGeneratorId _invoiceGeneratorId, IGeneratorNumberInvoice _generatorNumberInvoice) : base(port, encoderService)
        {
            invoices = _invoices;
            invoiceService = _invoiceService;
            invoiceGeneratorId = _invoiceGeneratorId;
            generatorNumberInvoice = _generatorNumberInvoice;

            bankSerializer = new();
        }

        protected override async Task HandleClientAsync(TcpClient client)
        {
            using (client)
            {
                stream = client.GetStream();

                GetRequest();
                var currentUser = bankSerializer.DeSerializeJSON<UserModel>(request);

                GetRequest();
                if(request == "add")
                {
                    invoiceService.AddInvoice(invoices, new InvoiceModel(currentUser, invoiceGeneratorId.Next(), generatorNumberInvoice.Next()));
                }
                
                if(request == "delete")
                {
                    GetRequest();
                    invoiceService.DeleteInvoice(invoices, bankSerializer.DeSerializeJSON<InvoiceModel>(request));
                }

                if(request == "show")
                {
                    await SendingMesageAsync(bankSerializer.SerializeJSONList<InvoiceModel>(invoiceService.GetAllUserInvoice(invoices, currentUser)));
                }

                stream = null;
            }
        }
    }
}
*/