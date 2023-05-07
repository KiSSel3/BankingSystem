using BankServer.Serever;
using BankServer.Repository;
using BankServer.Models;
using BankServer.Services;

public class Program
{
    private static void Main(string[] args)
    {
        Bank newBank = new Bank(new Repository<UserModel>(), new Repository<InvoiceModel>(), new UserService(), new InvoiceService(), new TransactionService(), new RegistrationService());

        newBank.Start();

        Console.ReadKey();

        newBank.Stop();
    }
}