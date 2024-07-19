using System.Runtime.CompilerServices;

namespace Contacts.Maui.Views_MVVM.Controls;

public partial class ContactControl_MVVM : ContentView
{
	public bool IsForEdit { get; set; }
	public bool IsForAdd { get; set; }

	public ContactControl_MVVM()
	{
		InitializeComponent();
	}

	protected override void OnPropertyChanged([CallerMemberName] string? propertyName = null)
	{
		base.OnPropertyChanged(propertyName);

		if (IsForAdd && IsForEdit is false)
		{
			SaveBtn.SetBinding(Button.CommandProperty, "AddContactCommand");
		}
		else if (IsForAdd is false && IsForEdit)
		{
			SaveBtn.SetBinding(Button.CommandProperty, "EditContactCommand");
		}
	}
}