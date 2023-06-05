using BankClient.Interfaces;
using BankClient.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BankClient.ViewModels
{
    public partial class MainViewModel : IQueryAttributable, INotifyPropertyChanged
    {
        private IInvoiceService invoiceService;
        private string ipAdress;

        private Timer timer;
        public MainViewModel(IInvoiceService _invoiceService) 
        {
            invoiceService = _invoiceService;

            //timer = new Timer(TimerElapsed, null, TimeSpan.Zero, TimeSpan.FromSeconds(30));
        }

        private async void TimerElapsed(object state)
        {
            await LoadData();
        }

        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                OnPropertyChanged();
            }
        }

        private string secondName;
        public string SecondName
        {
            get { return secondName; }
            set
            {
                secondName = value;
                OnPropertyChanged();
            }
        }

        private decimal balanse;
        public decimal Balanse
        {
            get { return balanse; }
            set {
                balanse = value;
                OnPropertyChanged();
            }
        }

        private UserModel currentUser;
        public UserModel CurrentUser
        {
            get { return currentUser; }
            set
            {
                currentUser = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<InvoiceModel> Invoices { get; set; } = new();

        [RelayCommand]
        private async void OnLoadData() => await LoadData();
        private async Task LoadData()
        {
            var response = await invoiceService.GetInvoices(CurrentUser, ipAdress, 8080);

            if (response.Status)
            {
                Invoices.Clear();
                var list = response.Data;

                foreach(var item in list)
                {
                    Invoices.Add(item);
                }
            }

            Balanse = Invoices.Sum(item => item.Balanse);

            var userName = CurrentUser.Name;
            var name = userName.Split(" ");

            FirstName = $"{name[0]} {name[1]}";
            SecondName = name[2];
        }

        [RelayCommand]
        private async void OnCreateInvoice() => await CreateInvoice();
        private async Task CreateInvoice()
        {
            var response = await invoiceService.CreateInvoice(CurrentUser, ipAdress, 8080);

            if (response.Status)
            {
                Invoices.Clear();
                var list = response.Data;
                
                foreach(var item in list)
                {
                    Invoices.Add(item);
                }
            }
        }

        [RelayCommand]
        private async void OnInvoiceManager(InvoiceModel invoice) => await InvoiceManager(invoice);

        private async Task InvoiceManager(InvoiceModel invoice)
        {
            IDictionary<string, object> parameters =
            new Dictionary<string, object>()
            {
                {"Invoice", invoice },
                {"IpAdress", ipAdress }
            };

            await Shell.Current.GoToAsync(nameof(InvoiceManagerPage), parameters);
        }

        [RelayCommand]
        private async void OnHome() => await Home();

        private async Task Home()
        {
            await Shell.Current.Navigation.PopAsync();
        }

        [RelayCommand]
        private async void OnHistory() => await History();

        private async Task History()
        {
            IDictionary<string, object> parameters =
            new Dictionary<string, object>()
            {
                {"UserModel", CurrentUser },
                {"IpAdress", ipAdress }
            };

            await Shell.Current.GoToAsync(nameof(HistoryPage), parameters);
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            CurrentUser = query["User"] as UserModel;
            ipAdress = query["IpAdress"] as string;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
