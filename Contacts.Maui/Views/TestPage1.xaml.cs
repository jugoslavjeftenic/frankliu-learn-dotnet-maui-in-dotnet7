using Contacts.Maui.ViewModels;

namespace Contacts.Maui.Views;

public partial class TestPage1 : ContentPage
{
	private readonly ContactViewModel _viewModel;

	public TestPage1()
	{
		InitializeComponent();

		_viewModel = new ContactViewModel();
		this.BindingContext = _viewModel;
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();

		_viewModel.LoadContact(1);
	}
}