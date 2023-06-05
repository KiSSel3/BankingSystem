using BankClient.ViewModels;

namespace BankClient.Pages;

public partial class MainPage : ContentPage
{
	public MainPage(MainViewModel mainViewModel)
	{
		BindingContext = mainViewModel;
		InitializeComponent();
	}
}