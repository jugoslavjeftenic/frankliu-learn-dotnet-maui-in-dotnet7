using Contacts.UseCases.Interfaces;
using Contact = Contacts.CoreBusiness.Contact;

namespace Contacts.Maui.Views;

public partial class AddContactPage : ContentPage
{
	private readonly IAddContactUseCase _addContactUseCase;

	public AddContactPage(IAddContactUseCase addContactUseCase)
	{
		InitializeComponent();
		_addContactUseCase = addContactUseCase;
	}

	private async void ContactCtrl_OnSave(object sender, EventArgs e)
	{
		await _addContactUseCase.ExecuteAsync(new Contact()
		{
			Name = ContactCtrl.Name,
			Email = ContactCtrl.Email,
			Phone = ContactCtrl.Phone,
			Address = ContactCtrl.Address
		});

		await Shell.Current.GoToAsync("..");
	}

	private void ContactCtrl_OnCancel(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("..");
	}

	private void ContactCtrl_OnError(object sender, string e)
	{
		DisplayAlert("Error", e, "Ok");
	}
}