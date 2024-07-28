namespace Contacts.Plugins.DataStore.SQLite;

public class Constants
{
	public const string DatabaseFileName = "ContactsSQLite.db3";
	public static string DatabasePath =>
		Path.Combine(FileSystem.AppDataDirectory, DatabaseFileName);
}
