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

	public ObservableCollection<Contact> Contacts { get; set; } = [];

	private string? _filterText;

	public string? FilterText
	{
		get => _filterText;
		set
		{
			SetProperty(ref _filterText, value);
			LoadContactsAsync(_filterText).ConfigureAwait(true);
		}
	}

	public ContactsViewModel
		(IViewContactsUseCase viewContactsUseCase, IDeleteContactUseCase deleteContactUseCase)
	{
		_viewContactsUseCase = viewContactsUseCase;
		_deleteContactUseCase = deleteContactUseCase;
	}

	public async Task LoadContactsAsync(string? filterText = null)
	{
		Contacts?.Clear();

		var contacts = await _viewContactsUseCase.ExecuteAsync(filterText ?? string.Empty);
		if (contacts is not null && contacts.Count > 0)
		{
			foreach (var contact in contacts)
			{
				Contacts?.Add(contact);
			}
		}
	}

	[RelayCommand]
	public async Task DeleteContactAsync(int contactId)
	{
		await _deleteContactUseCase.ExecuteAsync(contactId);
		await LoadContactsAsync(FilterText);
	}

	[RelayCommand]
	public async Task GoToEditContactAsync(int contactId)
	{
		await Shell.Current.GoToAsync($"{nameof(EditContact_MVVM_Page)}?Id={contactId}");
		await LoadContactsAsync();
	}

	[RelayCommand]
	public async Task GoToAddContactAsync()
	{
		await Shell.Current.GoToAsync($"{nameof(AddContact_MVVM_Page)}");
	}
}
