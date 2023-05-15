using BankServer.Serever;
using BankServer.Repository;
using BankServer.Models;
using BankServer.Services;
using BankServer.Generators;

public class Program
{
    private static void Main(string[] args)
    {
        Bank bank = new Bank(new UserRepository(), new InvoiceRepository(), new TransactionRepository(),
            new InvoiceService(), new TransactionService(), new RegistrationService(), new AuthorizationService(),
            new BaseGeneratorId(), new BaseGeneratorId(), new BaseGeneratorId(),
            new GeneratorNumberInvoice(),
            new EncoderService());

        bank.Start();
        Console.ReadKey();
        bank.Stop();
    }
}