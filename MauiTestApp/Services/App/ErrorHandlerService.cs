using EssentialLayers.Helpers.Extension;
using EssentialLayers.Helpers.Result;
using MauiTestApp.Handlers.Error;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using TemplateServices.Domain.Services.App;
using static TemplateServices.Domain.Helpers.Constants.LocalizationConstant;

namespace MauiTestApp.Services.App
{
	public class ErrorHandlerService(
		IFixedDialogService fixedDialogService,
		ILocalizationService localizationService,
		ILogger<ErrorHandlerService> logger
	) : IErrorHandlerService
	{
		private readonly ILocalizationService _localization = localizationService;

		private readonly ILogger<ErrorHandlerService> _logger = logger;

		/**/

		public Response HandleError(Exception exception)
		{
			string userMessage = GetUserFriendlyMessage(exception);

			return Response.Fail(userMessage);
		}

		public ResultHelper<T> HandleError<T>(Exception exception)
		{
			string userMessage = GetUserFriendlyMessage(exception);

			return ResultHelper<T>.Fail(userMessage);
		}

		public async Task<Response> ShowErrorAsync(Exception exception)
		{
			LogError(exception);

			string userMessage = GetUserFriendlyMessage(exception);
			string title = _localization.GetString("error_title");
			string okButton = _localization.GetString("ok_button");

			await fixedDialogService.ErrorAsync(title, userMessage, okButton);

			return Response.Fail(userMessage);
		}

		public async Task<bool> ShowErrorWithRetryAsync(
			Exception exception, Func<Task> retryAction
		)
		{
			LogError(exception);

			string userMessage = GetUserFriendlyMessage(exception);
			string title = _localization.GetString("error_title");
			string retryButton = _localization.GetString("retry_button");
			string cancelButton = _localization.GetString("cancel_button");

			string result = await fixedDialogService.PromptAsync(
				title, userMessage, retryButton, cancelButton
			);

			if (result.NotEmpty() && retryAction != null)
			{
				try
				{
					await retryAction();

					return true;
				}
				catch (Exception retryException)
				{
					return await ShowErrorWithRetryAsync(retryException, retryAction);
				}
			}

			return false;
		}

		public void LogError(Exception exception)
		{
			_logger?.LogError(
				exception, "Application error occurred: {ErrorMessage}", exception.Message
			);

			// You can also add additional logging mechanisms here
			// For example: crash analytics, file logging, etc.
			Debug.WriteLine($"ERROR: {exception}");
		}

		/**/

		private string GetUserFriendlyMessage(Exception exception)
		{
			return exception switch
			{
				UnauthorizedAccessException => _localization.GetString(PERMISSION_DENIED_ERROR),
				FileNotFoundException => _localization.GetString(FILE_NOT_FOUND_ERROR),
				DirectoryNotFoundException => _localization.GetString(FILE_NOT_FOUND_ERROR),
				HttpRequestException => _localization.GetString(NETWORK_CONNECTION_ERROR),
				InvalidOperationException => _localization.GetString(INVALID_OPERATION_ERROR),
				NoRecordWereAffectedException => _localization.GetString(NO_RECORDS_WERE_AFFECTED_ERROR),
				_ => _localization.GetString("unknown_error")
			};
		}
	}
}