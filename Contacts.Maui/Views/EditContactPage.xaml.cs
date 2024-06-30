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
				EntryName.Text = _contact.Name;
				EntryEmail.Text = _contact.Email;
				EntryPhone.Text = _contact.Phone;
				EntryAddress.Text = _contact.Address;
			}
		}
	}

	private void UpdateBtn_Clicked(object sender, EventArgs e)
	{
		if (NameValidator.IsNotValid)
		{
			DisplayAlert("Error", "Name is required.", "Ok");
			return;
		}

		if (EmailValidator.IsNotValid)
		{
			foreach (var error in EmailValidator.Errors!)
			{
				DisplayAlert("Error", error?.ToString(), "Ok");
			}
			return;
		}

		if (_contact is not null)
		{
			_contact.Name = EntryName.Text;
			_contact.Email = EntryEmail.Text;
			_contact.Phone = EntryPhone.Text;
			_contact.Address = EntryAddress.Text;

			ContactRepository.UpdateContact(_contact.ContactId, _contact);
			Shell.Current.GoToAsync("..");
		}
	}

	private void CancelBtn_Clicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("..");
	}
}