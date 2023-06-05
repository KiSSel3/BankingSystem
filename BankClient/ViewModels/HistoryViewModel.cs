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
    public partial class HistoryViewModel : IQueryAttributable, INotifyPropertyChanged
    {
        private ITransactionService transactionService;
        private string ipAdress;

        public HistoryViewModel(ITransactionService _transactionService)
        {
            transactionService = _transactionService;
        }

        public ObservableCollection<TransactionModel> Transactions { get; set; } = new();

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

        [RelayCommand]
        private async void OnLoadData() => await LoadData();
        private async Task LoadData()
        {
            var response = await transactionService.GetTransactionsByUser(CurrentUser, ipAdress, 8083);

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
        private async void OnHome() => await Home();

        private async Task Home()
        {
            await Shell.Current.Navigation.PopAsync();
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            CurrentUser = query["UserModel"] as UserModel;
            ipAdress = query["IpAdress"] as string;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
