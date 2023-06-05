using BankClient.Interfaces;
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

namespace BankClient.ViewModels
{
    public partial class InvoiceManagerViewModel : IQueryAttributable, INotifyPropertyChanged
    {
        private ITransactionService transactionService;
        private string ipAdress;

        public InvoiceManagerViewModel(ITransactionService _transactionService)
        {
            transactionService = _transactionService;
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
            //переход
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
