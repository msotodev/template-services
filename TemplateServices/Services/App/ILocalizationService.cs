namespace TemplateServices.Core.Services.App
{
	public interface ILocalizationService
	{
		string GetLocalizedString(string key);

		void SetCulture(string cultureCode);

		string CurrentCulture { get; }
	}
}