using Contacts.Maui.Models;
using Contacts.UseCases.Interfaces;
using System.Collections.ObjectModel;
using Contact = Contacts.CoreBusiness.Contact;

namespace Contacts.Maui.Views;

public partial class ContactsPage : ContentPage
{
	private readonly IViewContactsUseCase _viewContactsUseCase;
	private readonly IDeleteContactUseCase _deleteContactUseCase;

	public ContactsPage
		(IViewContactsUseCase viewContactsUseCase, IDeleteContactUseCase deleteContactUseCase)
	{
		InitializeComponent();
		_viewContactsUseCase = viewContactsUseCase;
		_deleteContactUseCase = deleteContactUseCase;
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		SearchBar.Text = string.Empty;
		LoadContacts();
	}

	private async void ListContacts_ItemSelected(object sender, SelectedItemChangedEventArgs e)
	{
		if (listContacts.SelectedItem is not null)
		{
			await Shell.Current
				.GoToAsync($"{nameof(EditContactPage)}?Id={((Contact)listContacts.SelectedItem).ContactId}");
		}
	}

	private void ListContacts_ItemTapped(object sender, ItemTappedEventArgs e)
	{
		listContacts.SelectedItem = null;
	}

	private void AddContactBtn_Clicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync(nameof(AddContactPage));
	}

	private async void DeleteMenuItem_Clicked(object sender, EventArgs e)
	{
		var menuItem = sender as MenuItem;
		var contact = menuItem?.CommandParameter as Contact;

		if (contact is not null)
		{
			await _deleteContactUseCase.ExecuteAsync(contact.ContactId);
			LoadContacts();
		}
	}

	private async void LoadContacts()
	{
		var contacts = new ObservableCollection<Contact>
			(await _viewContactsUseCase.ExecuteAsync(string.Empty));
		listContacts.ItemsSource = contacts;
	}

	private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
	{
		var contacts = new ObservableCollection<Contact>
			(await _viewContactsUseCase.ExecuteAsync(((SearchBar)sender).Text));
		listContacts.ItemsSource = contacts;
	}
}