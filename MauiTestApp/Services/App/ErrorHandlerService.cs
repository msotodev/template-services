using EssentialLayers.Helpers.Extension;
using EssentialLayers.Helpers.Result;
using MauiTestApp.Handlers.Error;
using MauiTestApp.Handlers.Error.Exceptions;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using TemplateServices.Core.Services.App;

namespace MauiTestApp.Services.App
{
	public class ErrorHandlerService(
		IFixedDialogService fixedDialogService,
		ILocalizationService localizationService,
		ILogger<ErrorHandlerService> logger
	) : IErrorHandlerService
	{
		private readonly ILocalizationService _localizationService = localizationService;

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
			string title = _localizationService.GetLocalizedString("error_title");
			string okButton = _localizationService.GetLocalizedString("ok_button");

			await fixedDialogService.ErrorAsync(title, userMessage, okButton);

			return Response.Fail(userMessage);
		}

		public async Task<bool> ShowErrorWithRetryAsync(
			Exception exception, Func<Task> retryAction
		)
		{
			LogError(exception);

			string userMessage = GetUserFriendlyMessage(exception);
			string title = _localizationService.GetLocalizedString("error_title");
			string retryButton = _localizationService.GetLocalizedString("retry_button");
			string cancelButton = _localizationService.GetLocalizedString("cancel_button");

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
				AppException appEx => _localizationService.GetLocalizedString(appEx.UserFriendlyKey),
				UnauthorizedAccessException => _localizationService.GetLocalizedString("permission_denied_error"),
				FileNotFoundException => _localizationService.GetLocalizedString("file_not_found_error"),
				HttpRequestException => _localizationService.GetLocalizedString("network_connection_error"),
				InvalidOperationException => _localizationService.GetLocalizedString("invalid_operation_error"),
				NoRecordWereAffectedException => _localizationService.GetLocalizedString("no_records_were_affected_error"),
				_ => _localizationService.GetLocalizedString("unknown_error")
			};
		}
	}
}