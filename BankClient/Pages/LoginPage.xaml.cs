using BankClient.ViewModels;

namespace BankClient.Pages;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginViewModel _loginViewModel)
    {
        BindingContext = _loginViewModel;
        InitializeComponent();
    }

}