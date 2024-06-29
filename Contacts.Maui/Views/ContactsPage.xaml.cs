using Contacts.Maui.Models;
using Contact = Contacts.Maui.Models.Contact;
namespace Contacts.Maui.Views;

public partial class ContactsPage : ContentPage
{
	public ContactsPage()
	{
		InitializeComponent();

		List<Contact> contacts = ContactRepository.GetContacts();

		listContacts.ItemsSource = contacts;
	}

	private async void ListContacts_ItemSelected(object sender, SelectedItemChangedEventArgs e)
	{
		if (listContacts.SelectedItem is not null)
		{
			await Shell.Current.GoToAsync(nameof(EditContactPage));
		}
	}

	private void ListContacts_ItemTapped(object sender, ItemTappedEventArgs e)
	{
		listContacts.SelectedItem = null;
	}
}