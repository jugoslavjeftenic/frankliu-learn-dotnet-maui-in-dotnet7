using Contacts.Maui.Views;
using Contacts.Maui.Views_MVVM;

namespace Contacts.Maui;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(ContactsPage), typeof(ContactsPage));
		Routing.RegisterRoute(nameof(EditContactPage), typeof(EditContactPage));
		Routing.RegisterRoute(nameof(AddContactPage), typeof(AddContactPage));
		Routing.RegisterRoute(nameof(Contacts_MVVM_Page), typeof(Contacts_MVVM_Page));
		Routing.RegisterRoute(nameof(EditContact_MVVM_Page), typeof(EditContact_MVVM_Page));
		Routing.RegisterRoute(nameof(AddContact_MVVM_Page), typeof(AddContact_MVVM_Page));
	}
}
