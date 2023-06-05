using BankClient.Pages;

namespace BankClient
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(InvoiceManagerPage), typeof(InvoiceManagerPage));
            InitializeComponent();
        }
    }
}