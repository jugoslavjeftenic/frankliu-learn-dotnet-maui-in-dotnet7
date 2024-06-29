namespace Contacts.Maui.Models;

public static class ContactRepository
{
	public static List<Contact> _contacts = [
		new Contact {Name = "John Doe", Email = "JohnDoe@gmail.com"},
		new Contact {Name = "Jane Doe", Email = "JaneDoe@gmail.com"},
		new Contact {Name = "Tom Hanks", Email = "TomHanks@gmail.com"},
		new Contact {Name = "Frank Liu", Email = "FrankLiu@gmail.com"},
		];

	public static List<Contact> GetContacts() => _contacts;

	public static Contact? GetContactById(int contactId)
	{
		return _contacts.FirstOrDefault(x => x.ContactId == contactId);
	}
}
