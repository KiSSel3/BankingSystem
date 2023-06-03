using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BankClient.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        [ObservableProperty]
        private string surname;

        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private string fathersName;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string ipAdress = "vanakis.keenetic.link";

        [RelayCommand]
        private async void OnRegister() => await Register();

        [RelayCommand]
        private async void OnAuthorization() => await Authorization();

        private async Task Register()
        {
 
        }

        private async Task Authorization()
        {

        }
    }
}
