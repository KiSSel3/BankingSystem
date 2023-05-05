using BankServer.Serever;

public class Program
{
    private static void Main(string[] args)
    {
        Bank newBank = new Bank();

        newBank.Start();
        newBank.Stop();
    }
}