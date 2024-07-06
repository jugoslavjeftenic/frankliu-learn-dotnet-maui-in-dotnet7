using Contacts.Maui.Models;
using Contact = Contacts.Maui.Models.Contact;

namespace Contacts.Maui.Views;

[QueryProperty(nameof(ContactId), "Id")]
public partial class EditContactPage : ContentPage
{
	private Contact? _contact;

	public EditContactPage()
	{
		InitializeComponent();
	}

	public string ContactId
	{
		set
		{
			_contact = ContactRepository.GetContactById(int.Parse(value));

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

			ContactRepository.UpdateContact(_contact.ContactId, _contact);
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