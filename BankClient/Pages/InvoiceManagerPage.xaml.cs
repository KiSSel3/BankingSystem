using BankClient.ViewModels;

namespace BankClient.Pages;

public partial class InvoiceManagerPage : ContentPage
{
	public InvoiceManagerPage(InvoiceManagerViewModel invoiceManagerViewModel)
	{
		BindingContext = invoiceManagerViewModel;
		InitializeComponent();
	}
}