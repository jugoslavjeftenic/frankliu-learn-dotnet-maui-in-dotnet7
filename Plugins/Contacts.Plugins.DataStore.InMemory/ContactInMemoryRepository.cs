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

	public Task<List<Contact>> GetContactsAsync(string filterText)
	{
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
}
