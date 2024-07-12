using Contacts.Maui.Models;
using Contacts.UseCases.Interfaces;
using System.Collections.ObjectModel;
using Contact = Contacts.Maui.Models.Contact;

namespace Contacts.Maui.Views;

public partial class ContactsPage : ContentPage
{
	private readonly IViewContactsUseCase _viewContactsUseCase;

	public ContactsPage(IViewContactsUseCase viewContactsUseCase)
	{
		InitializeComponent();
		_viewContactsUseCase = viewContactsUseCase;
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
				.GoToAsync($"{nameof(EditContactPage)}?Id={((CoreBusiness.Contact)listContacts.SelectedItem).ContactId}");
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

	private void DeleteMenuItem_Clicked(object sender, EventArgs e)
	{
		var menuItem = sender as MenuItem;
		var contact = menuItem?.CommandParameter as Contact;

		if (contact is not null)
		{
			ContactRepository.DeleteContact(contact.ContactId);
			LoadContacts();
		}
	}

	private async void LoadContacts()
	{
		var contacts = new ObservableCollection<CoreBusiness.Contact>
			(await _viewContactsUseCase.ExecuteAsync(string.Empty));
		listContacts.ItemsSource = contacts;
	}

	private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
	{
		var contacts = new ObservableCollection<CoreBusiness.Contact>
			(await _viewContactsUseCase.ExecuteAsync(((SearchBar)sender).Text));
		listContacts.ItemsSource = contacts;
	}
}