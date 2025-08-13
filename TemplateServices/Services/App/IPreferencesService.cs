namespace TemplateServices.Core.Services.App
{
	public interface IPreferencesService
	{
		void Clear();

		T Get<T>(string key);

		void Remove(string key);

		void Set<T>(string key, T value);
	}
}