using Domain.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankServer.Interfaces
{
    public interface IInvoiceRepository : IBaseRepository<InvoiceModel>
    {
        public Task<InvoiceModel?> GetByNumber(string number);
        public Task<IEnumerable<InvoiceModel>> GetByUser(UserModel user);
    }
}