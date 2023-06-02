using BankServer.Models;
using Microsoft.EntityFrameworkCore;

namespace BankServer.DataBase
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; } = null!;
        public DbSet<InvoiceModel> Invoices { get; set; } = null!;
        public DbSet<TransactionModel> Transactions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Bank.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InvoiceModel>()
                .HasOne<UserModel>(i => i.InvoiceUser)
                .WithMany()
                .HasForeignKey(i => i.InvoiceUserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TransactionModel>()
                .HasOne<InvoiceModel>(t => t.Sender)
                .WithMany()
                .HasForeignKey(t => t.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TransactionModel>()
                .HasOne<InvoiceModel>(t => t.Recipient)
                .WithMany()
                .HasForeignKey(t => t.RecipientId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
