using Contacts.Maui.ViewModels;

namespace Contacts.Maui.Views_MVVM;

public partial class AddContact_MVVM_Page : ContentPage
{
	private readonly ContactViewModel _contactViewModel;

	public AddContact_MVVM_Page(ContactViewModel contactViewModel)
	{
		InitializeComponent();
		_contactViewModel = contactViewModel;

		this.BindingContext = _contactViewModel;
	}
}