using EssentialLayers.Helpers.Extension;
using Microsoft.Extensions.Logging;
using TemplateServices.Domain.Services.App;

namespace MauiTestApp.Services.App
{
	public class NavigationService(
		ILoadingService loadingService,
		ILogger<NavigationService> logger
	) : INavigationService
	{
		public Task BackAsync(IDictionary<string, object>? parameters = null)
		{
			parameters ??= new Dictionary<string, object>();

			logger.LogInformation(
				"Backing to back with params: {params}", parameters.Serialize()
			);

			return Shell.Current.GoToAsync("..", true, parameters);
		}

		public async Task NavigateToAsync(string route, IDictionary<string, object>? parameters = null)
		{
			logger.LogInformation(
				"Navigating to: {route} with params: {params}", route, parameters.Serialize()
			);

			parameters ??= new Dictionary<string, object>();

			loadingService.Show();

			await Shell.Current.GoToAsync(route, true, parameters);

			loadingService.Hide();
		}
	}
}