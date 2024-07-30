using Contacts.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Contacts.WebApi
{
	public class ApplicationDbContext(
		DbContextOptions<ApplicationDbContext> options) : DbContext(options)
	{
		public DbSet<Contact> Contacts { get; set; }
	}
}
