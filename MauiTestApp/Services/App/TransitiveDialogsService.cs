using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Microsoft.Extensions.Logging;
using TemplateServices.Core.Services.App;
using static TemplateServices.Core.Helpers.Constants.ButtonsConstant;
using Font = Microsoft.Maui.Font;

namespace MauiTestApp.Services.App
{
	public class TransitiveDialogsService(
		ILogger<TransitiveDialogsService> logger
	) : ITransitiveDialogsService
	{
		private readonly TimeSpan Duration = TimeSpan.FromSeconds(4);

		private static Page MainPage => GetCurrentPage();

		/**/

		public async Task ErrorAsync(string message, string accept = ACCEPT)
		{
			SnackbarOptions options = DefaultSnackbarOptions(
				Color.FromArgb("#FF5733"), Color.FromArgb("#FFFFFF")
			);

			await DisplaySnackbarAsync(message, accept, options);
		}

		public async Task InfoAsync(string message, string accept = ACCEPT)
		{
			SnackbarOptions options = DefaultSnackbarOptions(
				Color.FromArgb("#f0f8ff"), Color.FromArgb("#2f4f4f")
			);

			await DisplaySnackbarAsync(message, accept, options);
		}

		public async Task WarningAsync(string message, string accept = ACCEPT)
		{
			SnackbarOptions options = DefaultSnackbarOptions(
				Color.FromArgb("#ff8c00"), Color.FromArgb("#FFFFFF")
			);

			await DisplaySnackbarAsync(message, accept, options);
		}

		/**/

		private async Task DisplaySnackbarAsync(
			string message, string accept, SnackbarOptions options
		)
		{
			try
			{
				await MainPage.DisplaySnackbar(
					message, actionButtonText: accept, duration: Duration, visualOptions: options
				);
			}
			catch (Exception e)
			{
				logger.LogError(e, "Error => {Message}", e.Message);
			}
		}

		private static SnackbarOptions DefaultSnackbarOptions(
			Color backgroundColor, Color textColor
		)
		{
			return new SnackbarOptions()
			{
				BackgroundColor = backgroundColor,
				Font = Font.SystemFontOfSize(14),
				TextColor = textColor,
				CornerRadius = new CornerRadius(4)
			};
		}

		private static Page GetCurrentPage()
		{
			Application? current = Application.Current;

			Shell? shell = current!.MainPage! as Shell;

			return shell is not null ? shell!.CurrentPage : current.MainPage!;
		}
	}
}