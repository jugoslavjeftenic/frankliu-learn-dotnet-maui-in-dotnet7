using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Contacts.Maui.Models;
using Contact = Contacts.Maui.Models.Contact;

namespace Contacts.Maui.ViewModels;

public partial class ContactViewModel : ObservableObject
{
	private Contact? _contact;
	public Contact? Contact
	{
		get => _contact;
		set
		{
			SetProperty(ref _contact, value);
		}
	}

	public ContactViewModel()
	{
		this.Contact = new Contact();
	}

	public void LoadContact(int contactId)
	{
		this.Contact = ContactRepository.GetContactById(contactId);
	}

	[RelayCommand]
	public void SaveContact()
	{
		if (this.Contact is not null)
		{
			ContactRepository.UpdateContact(this.Contact.ContactId, this.Contact);
		}
	}
}
