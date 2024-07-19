using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Contacts.Maui.Views_MVVM;
using Contacts.UseCases.Interfaces;
using Contact = Contacts.CoreBusiness.Contact;

namespace Contacts.Maui.ViewModels;

public partial class ContactViewModel : ObservableObject
{
	private Contact? _contact;
	private readonly IViewContactUseCase _viewContactUseCase;
	private readonly IEditContactUseCase _editContactUseCase;
	private readonly IAddContactUseCase _addContactUseCase;

	public Contact? Contact
	{
		get => _contact;
		set
		{
			SetProperty(ref _contact, value);
		}
	}

	public ContactViewModel(
		IViewContactUseCase viewContactUseCase,
		IEditContactUseCase editContactUseCase,
		IAddContactUseCase addContactUseCase)
	{
		this.Contact = new Contact();
		_viewContactUseCase = viewContactUseCase;
		_editContactUseCase = editContactUseCase;
		_addContactUseCase = addContactUseCase;
	}

	public async Task LoadContact(int contactId)
	{
		this.Contact = await _viewContactUseCase.ExecuteAsync(contactId);
	}

	[RelayCommand]
	public async Task EditContact()
	{
		if (this.Contact is not null)
		{
			await _editContactUseCase.ExecuteAsync(this.Contact.ContactId, this.Contact);
		}

		await Shell.Current.GoToAsync($"{nameof(Contacts_MVVM_Page)}");
	}

	[RelayCommand]
	public async Task AddContact()
	{
		if (this.Contact is not null)
		{
			await _addContactUseCase.ExecuteAsync(this.Contact);
		}

		await Shell.Current.GoToAsync($"{nameof(Contacts_MVVM_Page)}");
	}

	[RelayCommand]
	public async Task BackToContacts()
	{
		await Shell.Current.GoToAsync($"{nameof(Contacts_MVVM_Page)}");
	}
}
