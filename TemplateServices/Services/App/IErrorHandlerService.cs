using EssentialLayers.Helpers.Result;

namespace TemplateServices.Core.Services.App
{
	public interface IErrorHandlerService
	{
		Response HandleError(Exception exception);

		Task<Response> ShowErrorAsync(Exception exception);

		Task<bool> ShowErrorWithRetryAsync(Exception exception, Func<Task> retryAction);

		void LogError(Exception exception);
	}
}