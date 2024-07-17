using Contacts.UseCases.Interfaces;
using System.Collections.ObjectModel;
using Contact = Contacts.CoreBusiness.Contact;

namespace Contacts.Maui.ViewModels;

public class ContactsViewModel
{
	private readonly IViewContactsUseCase _viewContactsUseCase;

	public ObservableCollection<Contact> Contacts { get; set; }

	public ContactsViewModel(IViewContactsUseCase viewContactsUseCase)
	{
		_viewContactsUseCase = viewContactsUseCase;

		this.Contacts = new ObservableCollection<Contact>();
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
}
