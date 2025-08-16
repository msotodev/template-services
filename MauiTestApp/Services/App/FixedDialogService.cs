using Microsoft.Extensions.Logging;
using TemplateServices.Core.Services.App;
using static TemplateServices.Core.Helpers.Constants.ButtonsConstant;
using static TemplateServices.Core.Models.Types.DialogTypes;

namespace MauiTestApp.Services.App
{
	public class FixedDialogService(
		ILogger<FixedDialogService>	logger
	) : IFixedDialogService
	{
		private static Page MainPage => GetCurrentPage();

		/**/

		public Task<bool> ConfirmAsync(
			string message,
			string title = "Confirmation",
			string accept = ACCEPT,
			string cancel = CANCEL,
			CancellationToken? cancelToken = null
		) => DisplayAlertAsync(title, message, accept, cancel);

		public Task ErrorAsync(
			string message, string title = "Error", string cancel = CANCEL
		) => DisplayAlertAsync(title, message, cancel);

		public Task InfoAsync(
			string message, string title = "Information", string cancel = CANCEL
		) => DisplayAlertAsync(title, message, cancel);

		public Task<string> PromptAsync(
			string message = "",
			string title = "",
			string accept = ACCEPT,
			string cancel = CANCEL,
			string placeholder = "",
			string initialValue = "",
			int maxLength = 45,
			KeyboardType keyboard = KeyboardType.Default
		) => DisplayPromptAsync(
			message, title, accept, cancel,
			placeholder, initialValue, maxLength, keyboard
		);

		public Task WarningAsync(
			string message, string title = "Warning", string cancel = CANCEL
		) => DisplayAlertAsync(title, message, cancel);

		/**/

		private async Task<bool> DisplayAlertAsync(
			string message,
			string title = "Warning",
			string cancel = CANCEL,
			string accept = ACCEPT
		)
		{
			try
			{
				return await MainPage.DisplayAlert(title, message, accept, cancel);
			}
			catch (Exception e)
			{
				logger.LogError(e, "Error => {Message}", e.Message);

				return false;
			}
		}

		private async Task<string> DisplayPromptAsync(
			string message = "",
			string title = "",
			string accept = ACCEPT,
			string cancel = CANCEL,
			string placeholder = "",
			string initialValue = "",
			int maxLength = 45,
			KeyboardType keyboard = KeyboardType.Default
		)
		{
			try
			{
				return await MainPage.DisplayPromptAsync(
					title, message, accept, cancel,
					placeholder, maxLength, GetKeyboard(keyboard), initialValue
				);
			}
			catch (Exception e)
			{
				logger.LogError(e, "Error => {Message}", e.Message);

				return string.Empty;
			}
		}

		private static Page GetCurrentPage()
		{
			Application? current = Application.Current;

			Shell? shell = current!.MainPage! as Shell;

			return shell is not null ? shell!.CurrentPage : current.MainPage!;
		}

		private static Keyboard GetKeyboard(KeyboardType type) => type switch
		{
			KeyboardType.Chat => Keyboard.Chat,
			KeyboardType.Default => Keyboard.Default,
			KeyboardType.Email => Keyboard.Email,
			KeyboardType.Numeric => Keyboard.Numeric,
			KeyboardType.Plain => Keyboard.Plain,
			KeyboardType.Telephone => Keyboard.Telephone,
			KeyboardType.Text => Keyboard.Text,
			KeyboardType.Url => Keyboard.Url,
			_ => Keyboard.Default
		};
	}
}