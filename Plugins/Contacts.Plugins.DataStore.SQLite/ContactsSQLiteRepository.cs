using Contacts.UseCases.PluginInterfaces;
using SQLite;
using Contact = Contacts.CoreBusiness.Contact;

namespace Contacts.Plugins.DataStore.SQLite;

public class ContactsSQLiteRepository : IContactRepository
{
	private SQLiteAsyncConnection _database;

	public ContactsSQLiteRepository()
	{
		_database = new SQLiteAsyncConnection(Constants.DatabasePath);
		_database.CreateTableAsync<Contact>();
	}

	public async Task AddContactAsync(Contact contact)
	{
		await _database.InsertAsync(contact);
	}

	public async Task DeleteContactAsync(int contactId)
	{
		var contact = await GetContactByIdAsync(contactId);
		if (contact is not null && contact.ContactId.Equals(contactId))
		{
			await _database.DeleteAsync(contact);
		}
	}

	public async Task<Contact> GetContactByIdAsync(int contactId)
	{
		return await _database
			.Table<Contact>()
			.Where(x => x.ContactId.Equals(contactId)).FirstOrDefaultAsync();
	}

	public async Task<List<Contact>> GetContactsAsync(string filterText)
	{
		if (string.IsNullOrWhiteSpace(filterText))
		{
			return await _database.Table<Contact>().ToListAsync();
		}

		return await _database.QueryAsync<Contact>(@"
			SELECT *
			FROM Contact
			WHERE
				Name LIKE ? OR
				Email LIKE ? OR
				Phone LIKE ? OR
				Address LIKE ?
			", $"{filterText}%", $"{filterText}%", $"{filterText}%", $"{filterText}%");
	}

	public async Task UpdateContactAsync(int contactId, Contact contact)
	{
		if (contactId.Equals(contact.ContactId))
		{
			await _database.UpdateAsync(contact);
		}
	}
}
