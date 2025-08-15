using EssentialLayers.Helpers.Extension;
using Microsoft.Extensions.Logging;
using TemplateServices.Core.Services.App;

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

#if DEBUG || SPCHOLULADEBUG || TEZIUTLANDEBUG || TLATLAUQUITEPECDEBUG || ZACAPOAXTLADEBUG
			logger.LogInformation(
				"Backing to back with params: {params}", parameters.Serialize()
			);
#endif

			return Shell.Current.GoToAsync("..", true, parameters);
		}

		public async Task NavigateToAsync(string route, IDictionary<string, object>? parameters = null)
		{
#if DEBUG || SPCHOLULADEBUG || TEZIUTLANDEBUG || TLATLAUQUITEPECDEBUG || ZACAPOAXTLADEBUG
			logger.LogInformation(
				"Navigating to: {route} with params: {params}", route, parameters.Serialize()
			);
#endif

			parameters ??= new Dictionary<string, object>();

			loadingService.Show();

			await Shell.Current.GoToAsync(route, true, parameters);

			loadingService.Hide();
		}
	}
}