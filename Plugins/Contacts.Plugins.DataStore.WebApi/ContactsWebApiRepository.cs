using Contacts.UseCases.PluginInterfaces;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Contact = Contacts.CoreBusiness.Contact;

namespace Contacts.Plugins.DataStore.WebApi;

public class ContactsWebApiRepository : IContactRepository
{
	private HttpClient _httpClient;
	private JsonSerializerOptions _serializerOptions;

	public ContactsWebApiRepository()
	{
		_httpClient = new HttpClient();
		_serializerOptions = new JsonSerializerOptions()
		{
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
			WriteIndented = true
		};
	}

	public async Task AddContactAsync(Contact contact)
	{
		string json = JsonSerializer.Serialize<Contact>(contact, _serializerOptions);
		StringContent stringContent = new(json, Encoding.UTF8, "application/json");

		Uri uri = new($"{Constants.WebApiBaseUrl}/contacts");
		await _httpClient.PostAsync(uri, stringContent);
	}

	public async Task DeleteContactAsync(int contactId)
	{
		Uri uri = new($"{Constants.WebApiBaseUrl}/contacts/{contactId}");
		await _httpClient.DeleteAsync(uri);
	}

	public async Task<Contact> GetContactByIdAsync(int contactId)
	{
		Uri uri = new($"{Constants.WebApiBaseUrl}/contacts/{contactId}");
		Contact contact = new();

		var response = await _httpClient.GetAsync(uri);
		if (response.IsSuccessStatusCode)
		{
			string content = await response.Content.ReadAsStringAsync();
			contact = JsonSerializer.Deserialize<Contact>(content, _serializerOptions) ?? new();
		}

		return contact;
	}

	public async Task<List<Contact>> GetContactsAsync(string filterText)
	{
		List<Contact> contacts = [];

		Uri uri;
		if (string.IsNullOrWhiteSpace(filterText))
		{
			uri = new($"{Constants.WebApiBaseUrl}/contacts");
		}
		else
		{
			uri = new($"{Constants.WebApiBaseUrl}/contacts/s?={filterText}");
		}

		var response = await _httpClient.GetAsync(uri);
		if (response.IsSuccessStatusCode)
		{
			string content = await response.Content.ReadAsStringAsync();
			contacts = JsonSerializer.Deserialize<List<Contact>>(content, _serializerOptions) ?? [];
		}

		return contacts;
	}

	public async Task UpdateContactAsync(int contactId, Contact contact)
	{
		string json = JsonSerializer.Serialize<Contact>(contact, _serializerOptions);
		StringContent stringContent = new(json, Encoding.UTF8, "application/json");

		Uri uri = new($"{Constants.WebApiBaseUrl}/contacts/{contactId}");
		await _httpClient.PutAsync(uri, stringContent);
	}
}
