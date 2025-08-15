using EssentialLayers.Helpers.Extension;
using EssentialLayers.Helpers.Result;
using MauiTestApp.Handlers.Error;
using MauiTestApp.Handlers.Error.Exceptions;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using TemplateServices.Core.Helpers.Constants;
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
			string title = _localizationService.GetString("error_title");
			string okButton = _localizationService.GetString("ok_button");

			await fixedDialogService.ErrorAsync(title, userMessage, okButton);

			return Response.Fail(userMessage);
		}

		public async Task<bool> ShowErrorWithRetryAsync(
			Exception exception, Func<Task> retryAction
		)
		{
			LogError(exception);

			string userMessage = GetUserFriendlyMessage(exception);
			string title = _localizationService.GetString("error_title");
			string retryButton = _localizationService.GetString("retry_button");
			string cancelButton = _localizationService.GetString("cancel_button");

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
				AppException appEx => _localizationService.GetString(appEx.UserFriendlyKey),
				UnauthorizedAccessException => _localizationService.GetString("permission_denied_error"),
				FileNotFoundException => _localizationService.GetString("file_not_found_error"),
				DirectoryNotFoundException => _localizationService.GetString("file_not_found_error"),
				HttpRequestException => _localizationService.GetString("network_connection_error"),
				InvalidOperationException => _localizationService.GetString("invalid_operation_error"),
				NoRecordWereAffectedException => _localizationService.GetString(
					LocalizationConstant.NO_RECORDS_WERE_AFFECTED_ERROR
				),
				_ => _localizationService.GetString("unknown_error")
			};
		}
	}
}