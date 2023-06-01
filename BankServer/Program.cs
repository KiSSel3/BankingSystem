using BankServer.DataBase;
using BankServer.Generators;
using BankServer.Repository;
using BankServer.Serever;
using BankServer.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

public class Program
{
    private static void Main(string[] args)
    {
        var dataBase = new AppDbContext();


        Bank bank = new Bank(new UserRepository(dataBase), new InvoiceRepository(dataBase), new TransactionRepository(dataBase),
            new InvoiceService(), new TransactionService(), new RegistrationService(), new AuthorizationService(),
            new NumberGenerator(),
            new EncoderService());

        bank.Start();
        Console.ReadKey();
        bank.Stop();
    }
}