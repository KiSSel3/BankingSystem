using BankClient.Interfaces;
using BankClient.Pages;
using CommunityToolkit.Mvvm.Input;
using Domain.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BankClient.ViewModels
{
    public partial class InvoiceManagerViewModel : IQueryAttributable, INotifyPropertyChanged
    {
        private ITransactionService transactionService;
        private IInvoiceService invoiceService;
        private string ipAdress;

        private Timer timer;

        public InvoiceManagerViewModel(ITransactionService _transactionService, IInvoiceService _invoiceService)
        {
            transactionService = _transactionService;
            invoiceService = _invoiceService;

            //timer = new Timer(TimerElapsed, null, TimeSpan.Zero, TimeSpan.FromSeconds(30));
        }

        private async void TimerElapsed(object state)
        {
            await LoadData();
        }

        public ObservableCollection<TransactionModel> Transactions { get; set; } = new();

        private InvoiceModel currentInvoice;
        public InvoiceModel CurrentInvoice
        {
            get { return currentInvoice; }
            set
            {
                currentInvoice = value;
                OnPropertyChanged();
            }
        }


        [RelayCommand]
        private async void OnLoadData() => await LoadData();
        private async Task LoadData()
        {
            var invoiceResponse = await invoiceService.UpdateInvoice(CurrentInvoice.InvoiceUser, CurrentInvoice, ipAdress, 8080);

            if (invoiceResponse.Status)
            {
                CurrentInvoice = invoiceResponse.Data;
            }

            var response = await transactionService.GetTransactionsByInvoice(CurrentInvoice, ipAdress, 8083);

            if (response.Status)
            {
                Transactions.Clear();
                var list = response.Data;

                foreach (var item in list)
                {
                    Transactions.Add(item);
                }
            }
        }

        [RelayCommand]
        private async void OnCreateTransaction() => await CreateTransaction();
        private async Task CreateTransaction()

        {
            IDictionary<string, object> parameters = new Dictionary<string, object>()
                {
                    { "Invoice", CurrentInvoice},
                    { "IpAdress", ipAdress}
                };

            await Shell.Current.GoToAsync(nameof(TransactionPage), parameters);
        }

        [RelayCommand]
        private async void OnHome() => await Home();

        private async Task Home()
        {
            await Shell.Current.Navigation.PopAsync();
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            CurrentInvoice = query["Invoice"] as InvoiceModel;
            ipAdress = query["IpAdress"] as string;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
