namespace TemplateServices.Core.Services.App
{
	public interface IPreferencesService
	{
		string Get(string key);

		void Remove(string key);

		void Set(string key, string value);

		void Set(string key, bool value);
	}
}