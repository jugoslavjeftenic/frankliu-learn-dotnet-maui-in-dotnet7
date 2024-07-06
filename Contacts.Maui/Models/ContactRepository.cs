namespace Contacts.Maui.Models;

public static class ContactRepository
{
	private readonly static List<Contact> _contacts = [
		new Contact { ContactId = 1, Name = "John Doe", Email = "JohnDoe@gmail.com" },
		new Contact { ContactId = 2, Name = "Jane Doe", Email = "JaneDoe@gmail.com" },
		new Contact { ContactId = 3, Name = "Tom Hanks", Email = "TomHanks@gmail.com" },
		new Contact { ContactId = 4, Name = "Frank Liu", Email = "FrankLiu@gmail.com" },
		];

	public static List<Contact> GetContacts() => _contacts;

	public static Contact? GetContactById(int contactId)
	{
		var contact = _contacts.FirstOrDefault(x => x.ContactId == contactId);
		if (contact is not null)
		{
			return new Contact()
			{
				ContactId = contactId,
				Name = contact.Name,
				Email = contact.Email,
				Phone = contact.Phone,
				Address = contact.Address
			};
		}

		return null;
	}

	public static void UpdateContact(int contactId, Contact contact)
	{
		if (contactId != contact.ContactId) return;

		var contactToUpdate = _contacts.FirstOrDefault(x => x.ContactId == contactId);
		if (contactToUpdate is not null)
		{
			contactToUpdate.Name = contact.Name;
			contactToUpdate.Email = contact.Email;
			contactToUpdate.Phone = contact.Phone;
			contactToUpdate.Address = contact.Address;
		}
	}

	public static void AddContact(Contact contact)
	{
		var maxId = _contacts.Max(x => x.ContactId);
		contact.ContactId = maxId + 1;
		_contacts.Add(contact);
	}
}
