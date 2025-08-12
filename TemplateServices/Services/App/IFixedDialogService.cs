using static TemplateServices.Core.Helpers.Constants.ButtonsConstant;
using static TemplateServices.Core.Models.Types.DialogTypes;

namespace TemplateServices.Core.Services.App
{
	public interface IFixedDialogService
	{
		Task<bool> ConfirmAsync(
			string message,
			string title = "Confirmation",
			string accept = ACCEPT,
			string cancel = CANCEL,
			CancellationToken? cancelToken = null
		);

		Task ErrorAsync(string message, string title = "Error", string accept = "Ok");

		Task InfoAsync(string message, string title = "Information", string accept = ACCEPT);

		Task<string> PromptAsync(
			string message = "",
			string title = "",
			string accept = ACCEPT,
			string cancel = CANCEL,
			string placeholder = "",
			string initialValue = "",
			int maxLength = 45,
			KeyboardType keyboard = KeyboardType.Default
		);

		Task WarningAsync(string message, string title = "Warning", string accept = ACCEPT);
	}
}