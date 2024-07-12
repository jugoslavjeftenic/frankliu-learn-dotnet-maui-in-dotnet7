using Contacts.UseCases.Interfaces;

namespace Contacts.Maui.Views;

[QueryProperty(nameof(ContactId), "Id")]
public partial class EditContactPage : ContentPage
{
	private CoreBusiness.Contact? _contact;
	private readonly IViewContactUseCase _viewContactUseCase;

	public EditContactPage(IViewContactUseCase viewContactUseCase)
	{
		InitializeComponent();
		_viewContactUseCase = viewContactUseCase;
	}

	public string ContactId
	{
		set
		{
			_contact = _viewContactUseCase
				.ExecuteAsync(int.Parse(value))
				.GetAwaiter()
				.GetResult();

			if (_contact is not null)
			{
				ContactCtrl.Name = _contact.Name;
				ContactCtrl.Email = _contact.Email;
				ContactCtrl.Phone = _contact.Phone;
				ContactCtrl.Address = _contact.Address;
			}
		}
	}

	private void UpdateBtn_Clicked(object sender, EventArgs e)
	{
		if (_contact is not null)
		{
			_contact.Name = ContactCtrl.Name;
			_contact.Email = ContactCtrl.Email;
			_contact.Phone = ContactCtrl.Phone;
			_contact.Address = ContactCtrl.Address;

			//ContactRepository.UpdateContact(_contact.ContactId, _contact);
			Shell.Current.GoToAsync("..");
		}
	}

	private void CancelBtn_Clicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("..");
	}

	private void ContactCtrl_OnError(object sender, string e)
	{
		DisplayAlert("Error", e, "Ok");
	}
}