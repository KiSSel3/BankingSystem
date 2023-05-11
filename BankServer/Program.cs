using BankServer.Serever;
using BankServer.Repository;
using BankServer.Models;
using BankServer.Services;
using BankServer.Generators;

public class Program
{
    private static void Main(string[] args)
    {
        Bank newBank = new Bank(new Repository<UserModel>(), new Repository<InvoiceModel>(), new UserService(), new InvoiceService(), new TransactionService(), new RegistrationService(), new AuthorizationService(), new BaseGeneratorId(), new BaseGeneratorId(), new BaseGeneratorId(), new EncoderService(), new GeneratorNumberInvoice());

        newBank.Start();

        Console.ReadKey();

        newBank.Stop();
    }
}