namespace TemplateServices.Core.Services.App
{
	public interface INavigationService
	{
		Task BackAsync(IDictionary<string, object>? parameters = null);

		Task NavigateToAsync(string route, IDictionary<string, object>? parameters = null);
	}
}