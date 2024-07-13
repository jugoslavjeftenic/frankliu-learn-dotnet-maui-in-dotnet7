using Contacts.UseCases.PluginInterfaces;
using Contact = Contacts.CoreBusiness.Contact;

namespace Contacts.Plugins.DataStore.InMemory;

public class ContactInMemoryRepository : IContactRepository
{
	private readonly static List<Contact> _contacts = [
		new Contact { ContactId = 1, Name = "John Doe", Email = "JohnDoe@gmail.com" },
		new Contact { ContactId = 2, Name = "Jane Doe", Email = "JaneDoe@gmail.com" },
		new Contact { ContactId = 3, Name = "Tom Hanks", Email = "TomHanks@gmail.com" },
		new Contact { ContactId = 4, Name = "Frank Liu", Email = "FrankLiu@gmail.com" },
	];

	public Task<Contact> GetContactByIdAsync(int contactId)
	{
		var contact = _contacts.FirstOrDefault(x => x.ContactId.Equals(contactId));
		if (contact is not null)
		{
			return Task.FromResult(new Contact()
			{
				ContactId = contactId,
				Name = contact.Name,
				Email = contact.Email,
				Phone = contact.Phone,
				Address = contact.Address
			});
		}

		return Task.FromResult(new Contact());
	}

	public Task<List<Contact>> GetContactsAsync(string filterText)
	{
		if (string.IsNullOrWhiteSpace(filterText))
		{
			return Task.FromResult(_contacts);
		}

		var contacts = _contacts
			.Where(x => string.IsNullOrWhiteSpace(x.Name) is false &&
						x.Name.StartsWith(filterText, StringComparison.OrdinalIgnoreCase))
			.ToList();

		if (contacts is null || contacts.Count < 1)
		{
			contacts = _contacts
				.Where(x => string.IsNullOrWhiteSpace(x.Email) is false &&
							x.Email.StartsWith(filterText, StringComparison.OrdinalIgnoreCase))
				.ToList();
		}
		else
		{
			return Task.FromResult(contacts);
		}

		if (contacts is null || contacts.Count < 1)
		{
			contacts = _contacts
				.Where(x => string.IsNullOrWhiteSpace(x.Phone) is false &&
							x.Phone.StartsWith(filterText, StringComparison.OrdinalIgnoreCase))
				.ToList();
		}
		else
		{
			return Task.FromResult(contacts);
		}

		if (contacts is null || contacts.Count < 1)
		{
			contacts = _contacts
				.Where(x => string.IsNullOrWhiteSpace(x.Address) is false &&
							x.Address.StartsWith(filterText, StringComparison.OrdinalIgnoreCase))
				.ToList();
		}
		else
		{
			return Task.FromResult(contacts);
		}

		return Task.FromResult(contacts);
	}

	public Task UpdateContactAsync(int contactId, Contact contact)
	{
		if (contactId.Equals(contact.ContactId) is false) return Task.CompletedTask;

		var contactToUpdate = _contacts.FirstOrDefault(x => x.ContactId.Equals(contactId));
		if (contactToUpdate is not null)
		{
			contactToUpdate.Name = contact.Name;
			contactToUpdate.Email = contact.Email;
			contactToUpdate.Phone = contact.Phone;
			contactToUpdate.Address = contact.Address;
		}

		return Task.CompletedTask;
	}
}
