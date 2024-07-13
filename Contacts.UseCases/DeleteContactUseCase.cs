using Contacts.UseCases.Interfaces;
using Contacts.UseCases.PluginInterfaces;

namespace Contacts.UseCases;

public class DeleteContactUseCase : IDeleteContactUseCase
{
	private readonly IContactRepository _contactRepository;

	public DeleteContactUseCase(IContactRepository contactRepository)
	{
		_contactRepository = contactRepository;
	}

	public async Task ExecuteAsync(int contactId)
	{
		await _contactRepository.DeleteContactAsync(contactId);
	}
}
