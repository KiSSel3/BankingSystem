using BankClient.ViewModels;

namespace BankClient.Pages;

public partial class TransactionPage : ContentPage
{
	public TransactionPage(TransactionViewModel transactionViewModel)
	{
		BindingContext = transactionViewModel;
		InitializeComponent();
	}
}