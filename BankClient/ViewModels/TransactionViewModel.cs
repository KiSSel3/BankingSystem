using BankClient.Interfaces;
using BankClient.Pages;
using CommunityToolkit.Mvvm.Input;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BankClient.ViewModels
{
    public partial class TransactionViewModel : IQueryAttributable, INotifyPropertyChanged
    {
        private ITransactionService transactionService;
        private IInvoiceService invoiceService;
        private string ipAdress;

        private Timer timer;

        public TransactionViewModel(ITransactionService _transactionService, IInvoiceService _invoiceService)
        {
            transactionService = _transactionService;
            invoiceService = _invoiceService;

            IsVisibleError = false;
            IsVisibleTransaction = false;

            //timer = new Timer(TimerElapsed, null, TimeSpan.Zero, TimeSpan.FromSeconds(30));
        }

        private async void TimerElapsed(object state)
        {
            await LoadData();
        }

        private string recipientNumber;
        public string RecipientNumber
        {
            get { return recipientNumber; }
            set
            {
                recipientNumber = value;
                OnPropertyChanged();
            }
        }

        private string amount = "0";
        public string Amount
        {
            get { return amount; }
            set
            {
                amount = value;
                OnPropertyChanged();
            }
        }


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

        private TransactionModel currentTransaction;
        public TransactionModel CurrentTransaction
        {
            get { return currentTransaction; }
            set
            {
                currentTransaction = value;
                OnPropertyChanged();
            }
        }


        private bool isVisibleError;
        public bool IsVisibleError
        {
            get { return isVisibleError; }
            set
            {
                isVisibleError = value;
                OnPropertyChanged();
            }
        }

        private bool isVisibleTransaction;
        public bool IsVisibleTransaction
        {
            get { return isVisibleTransaction; }
            set
            {
                isVisibleTransaction = value;
                OnPropertyChanged();
            }
        }

        private string errorMessage;
        public string ErrorMessage
        {
            get { return errorMessage; }
            set
            {
                errorMessage = value;
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
        }


        [RelayCommand]
        private async void OnTransaction() => await Transaction();
        private async Task Transaction()
        {
            IsVisibleTransaction = false;
            IsVisibleError = false;
            await LoadData();

            try
            {
                var amountDecimal = Convert.ToDecimal(Amount);
                if(amountDecimal < 1 || amountDecimal > CurrentInvoice.Balanse)
                {
                    throw new Exception();
                }

                var response = await transactionService.CreateTransaction(CurrentInvoice, RecipientNumber, amountDecimal, ipAdress, 8083);

                if (!response.Status)
                {
                    ErrorMessage = "Incorect recipient number!";
                    IsVisibleError = true;
                    return;
                }

                CurrentTransaction = response.Data;
                IsVisibleTransaction = true;

                await LoadData();
            }
            catch{
                ErrorMessage = "Incorect amount!";
                IsVisibleError = true;
            }
        }

        [RelayCommand]
        private async void OnHome() => await Home();

        private async Task Home()
        {
            await Shell.Current.Navigation.PopAsync();
        }

        [RelayCommand]
        private async void OnHideError() => await HideError();
        private async Task HideError()
        {
            IsVisibleError = false;
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
