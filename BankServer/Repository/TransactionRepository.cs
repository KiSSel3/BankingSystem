﻿using BankServer.Interfaces;
using BankServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BankServer.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        //Временно лист
        private List<TransactionModel> transactions = new();

        public async Task<bool> Create(TransactionModel item)
        {
            try
            {
                transactions.Add(item);
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
                transactions.Remove(item);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<TransactionModel?> GetById(ulong id)
        {
            return transactions.FirstOrDefault(item => item.Id == id, null);
        }

        public async Task<IEnumerable<TransactionModel>> GetByRecipient(InvoiceModel recipient)
        {
            return transactions.Where(item => item.Recipient == recipient);
        }

        public async Task<IEnumerable<TransactionModel>> GetBySender(InvoiceModel sender)
        {
            return transactions.Where(item => item.Sender == sender);
        }

        public async Task<IEnumerable<TransactionModel>> Select()
        {
            return transactions;
        }

        public async Task<TransactionModel> Update(TransactionModel item)
        {
            await Delete(await GetById(item.Id));
            await Create(item);
            return item;
        }
    }
}
