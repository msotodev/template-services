namespace TemplateServices.Domain.Services.App
{
	public interface ILocalizationService
	{
		string GetString(string key);

		string GetString(string key, string dynamicParameterKe);

		void SetCulture(string cultureCode);

		string CurrentCulture { get; }
	}
}