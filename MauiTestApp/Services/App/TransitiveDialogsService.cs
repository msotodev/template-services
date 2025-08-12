using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using TemplateServices.Core.Services.App;
using Font = Microsoft.Maui.Font;

namespace MauiTestApp.Services.App
{
	public class TransitiveDialogsService : ITransitiveDialogsService
	{
		private static Page MainPage => GetCurrentPage();

		/**/

		public async Task ErrorAsync(string message, string accept = "Accept")
		{
			SnackbarOptions options = DefaultSnackbarOptions(Color.FromArgb("#FF5733"), Color.FromArgb("#FFFFFF"));
			
			await MainPage.DisplaySnackbar(
				message, actionButtonText: accept, visualOptions: options
			);
		}

		public async Task InfoAsync(string message, string accept = "Accept")
		{
			SnackbarOptions options = DefaultSnackbarOptions(Color.FromArgb("#f0f8ff"), Color.FromArgb("#2f4f4f"));

			await MainPage.DisplaySnackbar(
				message, actionButtonText: accept, duration: TimeSpan.FromSeconds(2), visualOptions: options
			);
		}

		public async Task WarningAsync(string message, string accept = "Accept")
		{
			SnackbarOptions options = DefaultSnackbarOptions(Color.FromArgb("#ff8c00"), Color.FromArgb("#FFFFFF"));

			await MainPage.DisplaySnackbar(
				message, actionButtonText: accept, duration: TimeSpan.FromSeconds(2), visualOptions: options
			);
		}

		/**/

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