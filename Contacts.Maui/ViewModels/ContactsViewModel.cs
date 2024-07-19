using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Contacts.Maui.Views_MVVM;
using Contacts.UseCases.Interfaces;
using System.Collections.ObjectModel;
using Contact = Contacts.CoreBusiness.Contact;

namespace Contacts.Maui.ViewModels;

public partial class ContactsViewModel : ObservableObject
{
	private readonly IViewContactsUseCase _viewContactsUseCase;
	private readonly IDeleteContactUseCase _deleteContactUseCase;

	public ObservableCollection<Contact> Contacts { get; set; }

	public ContactsViewModel
		(IViewContactsUseCase viewContactsUseCase, IDeleteContactUseCase deleteContactUseCase)
	{
		_viewContactsUseCase = viewContactsUseCase;
		_deleteContactUseCase = deleteContactUseCase;
		this.Contacts = [];
	}

	public async Task LoadContactsAsync()
	{
		this.Contacts?.Clear();

		var contacts = await _viewContactsUseCase.ExecuteAsync(string.Empty);
		if (contacts is not null && contacts.Count > 0)
		{
			foreach (var contact in contacts)
			{
				this.Contacts?.Add(contact);
			}
		}
	}

	[RelayCommand]
	public async Task DeleteContact(int contactId)
	{
		await _deleteContactUseCase.ExecuteAsync(contactId);
		await LoadContactsAsync();
	}

	[RelayCommand]
	public async Task GoToEditContact(int contactId)
	{
		await Shell.Current.GoToAsync($"{nameof(EditContact_MVVM_Page)}?Id={contactId}");
		await LoadContactsAsync();
	}

	[RelayCommand]
	public async Task GoToAddContact()
	{
		await Shell.Current.GoToAsync($"{nameof(AddContact_MVVM_Page)}");
	}
}
