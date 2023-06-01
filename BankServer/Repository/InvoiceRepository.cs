using BankServer.DataBase;
using BankServer.Interfaces;
using BankServer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BankServer.Repository
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly AppDbContext dbContext;

        public InvoiceRepository(AppDbContext DbContext)
        {
            dbContext = DbContext;

            dbContext.Database.EnsureCreated();
            dbContext.Invoices.Load();
        }

        public async Task<bool> Create(InvoiceModel item)
        {
            try
            {
                await dbContext.Invoices.AddAsync(item);
                await dbContext.SaveChangesAsync();

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
                dbContext.Invoices.Remove(item);
                await dbContext.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<InvoiceModel?> GetById(ulong id)
        {
            return await dbContext.Invoices.FirstOrDefaultAsync(item => item.Id == id);
        }

        public async Task<InvoiceModel?> GetByNumber(string number)
        {
            return await dbContext.Invoices.FirstOrDefaultAsync(item => item.Number == number);
        }

        public async Task<IEnumerable<InvoiceModel>> GetByUser(UserModel user)
        {
            return dbContext.Invoices.Where(item => item.InvoiceUser.Equals(user));
        }

        public async Task<IEnumerable<InvoiceModel>> Select()
        {
            return await dbContext.Invoices.ToListAsync();
        }

        public async Task<InvoiceModel> Update(InvoiceModel item)
        {
            dbContext.Invoices.Update(item);
            await dbContext.SaveChangesAsync();

            return item;
        }
    }
}