using EssentialLayers.Helpers.Extension;
using TemplateServices.Core.Services.App;

namespace MauiTestApp.Services.App
{
	public class PreferencesService : IPreferencesService
	{
		public void Clear() => Preferences.Clear();

		public T Get<T>(string key)
		{
			string get = Preferences.Get(key, string.Empty);

			return get.Deserialize<T>();
		}

		public void Remove(string key) => Preferences.Remove(key);

		public void Set<T>(string key, T value)
		{
			string serialized = value.Serialize();
			
			Preferences.Set(key, serialized);
		}
	}
}