using BankServer.Interfaces;
using BankServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BankServer.Repository
{
    public class InvoiceRepository : IInvoiceRepository
    {
        //Временно лист
        private List<InvoiceModel> invoices = new();

        public async Task<bool> Create(InvoiceModel item)
        {
            try
            {
                invoices.Add(item);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Delete(InvoiceModel item)
        {
            try
            {
                invoices.Remove(item);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<InvoiceModel?> GetById(ulong id)
        {
            return invoices.FirstOrDefault(item => item.Id == id, null);
        }

        public async Task<InvoiceModel?> GetByNumber(string number)
        {
            return invoices.FirstOrDefault(item => item.Number == number, null);
        }

        public async Task<IEnumerable<InvoiceModel>> GetByUser(UserModel user)
        {
            return invoices.Where(item => item.InvoiceUser.Equals(user));
        }

        public async Task<IEnumerable<InvoiceModel>> Select()
        {
            return invoices;
        }

        public async Task<InvoiceModel> Update(InvoiceModel item)
        {
            await Delete(await GetById(item.Id));
            await Create(item);
            return item;
        }
    }
}
