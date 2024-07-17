using CommunityToolkit.Mvvm.ComponentModel;
using Contacts.UseCases.Interfaces;
using Contact = Contacts.CoreBusiness.Contact;

namespace Contacts.Maui.ViewModels;

public partial class ContactViewModel : ObservableObject
{
	private Contact? _contact;
	private readonly IViewContactUseCase _viewContactUseCase;

	public Contact? Contact
	{
		get => _contact;
		set
		{
			SetProperty(ref _contact, value);
		}
	}

	public ContactViewModel(IViewContactUseCase viewContactUseCase)
	{
		this.Contact = new Contact();
		_viewContactUseCase = viewContactUseCase;
	}

	public async Task LoadContact(int contactId)
	{
		this.Contact = await _viewContactUseCase.ExecuteAsync(contactId);
	}

	//[RelayCommand]
	//public void SaveContact()
	//{
	//	if (this.Contact is not null)
	//	{
	//		ContactRepository.UpdateContact(this.Contact.ContactId, this.Contact);
	//	}
	//}
}
