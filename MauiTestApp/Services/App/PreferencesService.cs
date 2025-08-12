using TemplateServices.Core.Services.App;

namespace MauiTestApp.Services.App
{
	public class PreferencesService : IPreferencesService
	{
		public string Get(string key) => Preferences.Get(key, string.Empty);

		public void Remove(string key) => Preferences.Remove(key);

		public void Set(string key, string value) => Preferences.Set(key, value);

		public void Set(string key, bool value) => Preferences.Set(key, value);
	}
}