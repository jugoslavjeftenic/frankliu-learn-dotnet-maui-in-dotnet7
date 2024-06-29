using Contacts.Maui.Models;
using Contact = Contacts.Maui.Models.Contact;

namespace Contacts.Maui.Views;

[QueryProperty(nameof(ContactId), "Id")]
public partial class EditContactPage : ContentPage
{
	private Contact? _contact;

	public EditContactPage()
	{
		InitializeComponent();
	}

	private void BtnCancel_Clicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("..");
	}

	public string ContactId
	{
		set
		{
			_contact = ContactRepository.GetContactById(int.Parse(value));
			LblName.Text = _contact?.Name;
		}
	}
}