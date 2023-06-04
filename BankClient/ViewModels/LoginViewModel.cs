using BankClient.Interfaces;
using Domain.Response;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BankClient.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private IRegistrationService registrationService;

        public LoginViewModel(IRegistrationService _registrationService)
        {
            registrationService = _registrationService;
        }

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
            var response = await registrationService.Registration($"{Surname} {Username} {FathersName}", Password, ipAdress, 8081);

            if (!response.Status)
            {
                return; // далее сообщение об ошибке
            }

            //переход на страницу
        }

        private async Task Authorization()
        {

        }
    }
}
