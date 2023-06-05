using BankClient.Interfaces;
using BankClient.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BankClient.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private IRegistrationService registrationService;
        private IAuthorizationService authorizationService;

        private string ipAdress = "vanakis.keenetic.link";
        public LoginViewModel(IRegistrationService _registrationService, IAuthorizationService _authorizationService)
        {
            registrationService = _registrationService;
            authorizationService = _authorizationService;

            IsVisible = false;
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
        private bool isVisible;

        [ObservableProperty]
        private string errorMessage;

        [RelayCommand]
        private async void OnRegister() => await Register();

        [RelayCommand]
        private async void OnAuthorization() => await Authorization();

        [RelayCommand]
        private async void OnChangeIp() => await ChangeIp();

        [RelayCommand]
        private async void OnHideError() => await HideError();
        private async Task Register()
        {
            try
            {
                IsVisible = false;

                var response = await registrationService.Registration($"{Surname} {Username} {FathersName}", Password, ipAdress, 8081);

                if (!response.Status)
                {
                    ErrorMessage = "Error! The user is already registered!";
                    IsVisible = true;
                    return;
                }

                IDictionary<string, object> parameters = new Dictionary<string, object>()
                {
                    { "User", response.Data},
                    { "IpAdress", ipAdress}
                };

                await Shell.Current.GoToAsync(nameof(MainPage), parameters);
            }
            catch
            {
                ErrorMessage = "Server connection error!";
                IsVisible = true;
            }
        }

        private async Task Authorization()
        {
            try
            {
                IsVisible = false;

                var response = await authorizationService.Authorization($"{Surname} {Username} {FathersName}", Password, ipAdress, 8082);

                if (!response.Status)
                {
                    ErrorMessage = "Error! Incorrect data entered!";
                    IsVisible = true;
                    return;
                }

                IDictionary<string, object> parameters = new Dictionary<string, object>()
                {
                    { "User", response.Data},
                    { "IpAdress", ipAdress}
                };

                await Shell.Current.GoToAsync(nameof(MainPage), parameters);
            }
            catch
            {
                ErrorMessage = "Server connection error!";
                IsVisible = true;
            }
        }

        private async Task ChangeIp()
        {
            var newIpAdress = await Application.Current.MainPage.DisplayPromptAsync("Ip adress", "Type ip address:");

            if (newIpAdress is not null)
            {
                ipAdress = newIpAdress;
            }
        }

        private async Task HideError()
        {
            IsVisible = false;
        }
    }
}
