using Contacts.Maui.Models;
using System.Collections.ObjectModel;
using Contact = Contacts.Maui.Models.Contact;

namespace Contacts.Maui.Views;

public partial class ContactsPage : ContentPage
{
	public ContactsPage()
	{
		InitializeComponent();
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();

		var contacts = new ObservableCollection<Contact>(ContactRepository.GetContacts());
		listContacts.ItemsSource = contacts;
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
}