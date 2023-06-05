using BankClient.Pages;

namespace BankClient
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));

            InitializeComponent();
        }
    }
}