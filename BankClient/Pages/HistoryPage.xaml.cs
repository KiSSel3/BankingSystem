using BankClient.ViewModels;

namespace BankClient.Pages;

public partial class HistoryPage : ContentPage
{
	public HistoryPage(HistoryViewModel historyViewModel)
	{
		BindingContext = historyViewModel;
		InitializeComponent();
	}
}