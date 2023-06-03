using BankClient.ViewModels;

namespace BankClient.Pages;

public partial class LoginPage : ContentPage
{
    private LoginViewModel loginViewModel;
    public LoginPage(LoginViewModel _loginViewModel)
    {
        loginViewModel = _loginViewModel;
        BindingContext = loginViewModel;
        InitializeComponent();
    }

    public async void OnKiSSelClicked(object sender, EventArgs e)
    {

        var newIpAdress = await DisplayPromptAsync("Ip adress", "Type ip address:");

        if(newIpAdress is not null)
        {
            loginViewModel.IpAdress = newIpAdress;
        }
    }
}