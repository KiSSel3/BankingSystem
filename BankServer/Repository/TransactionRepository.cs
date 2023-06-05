using BankServer.DataBase;
using BankServer.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BankServer.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext dbContext;
        public TransactionRepository(AppDbContext DbContext)
        {
            dbContext = DbContext;

            dbContext.Database.EnsureCreated();
            dbContext.Transactions.Load();
        }

        public async Task<bool> Create(TransactionModel item)
        {
            try
            {
                await dbContext.Transactions.AddAsync(item);
                await dbContext.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Delete(TransactionModel item)
        {
            try
            {
                dbContext.Transactions.Remove(item);
                await dbContext.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<TransactionModel?> GetById(ulong id)
        {
            return await dbContext.Transactions.FirstOrDefaultAsync(item => item.Id == id);
        }

        public async Task<IEnumerable<TransactionModel>> GetByRecipient(InvoiceModel recipient)
        {
            return  dbContext.Transactions.Where(item => item.Recipient.Equals(recipient));
        }

        public async Task<IEnumerable<TransactionModel>> GetBySender(InvoiceModel sender)
        {
            return  dbContext.Transactions.Where(item => item.Sender.Equals(sender) || item.Recipient.Equals(sender));
        }

        public async Task<IEnumerable<TransactionModel>> GetByUser(UserModel user)
        {
            return dbContext.Transactions.Where(item => item.Sender.InvoiceUserId.Equals(user.Id) || item.Recipient.InvoiceUserId.Equals(user.Id));
        }

        public async Task<TransactionModel> Normalization(TransactionModel item)
        {
            var dbTransaction = await dbContext.Transactions.FirstOrDefaultAsync(dbItem => dbItem.Equals(item));

            if (dbTransaction is not null)
                return dbTransaction;

            return item;
        }

        public async Task<IEnumerable<TransactionModel>> Select()
        {
            return await dbContext.Transactions.ToListAsync();
        }

        public async Task<TransactionModel> Update(TransactionModel item)
        {
            dbContext.Transactions.Update(item);
            await dbContext.SaveChangesAsync();

            return item;
        }
    }
}