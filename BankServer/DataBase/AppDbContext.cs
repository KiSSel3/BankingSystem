using BankServer.Models;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankServer.DataBase
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Bank.db");
        }

        public DbSet<UserModel> Users { get; set; } = null!;
        public DbSet<InvoiceModel> Invoices { get; set; } = null!;
        public DbSet<TransactionModel> Transactions { get; set; } = null!;
    }
}
