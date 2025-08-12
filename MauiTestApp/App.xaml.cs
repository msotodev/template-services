using TemplateServices.Core.Services.App;

namespace MauiTestApp
{
	public partial class App : Application
	{
		private readonly IErrorHandlerService _errorHandler;

		/**/

		public App(IErrorHandlerService errorHandler)
		{
			InitializeComponent();

			_errorHandler = errorHandler;

			MainPage = new AppShell();

			AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
			TaskScheduler.UnobservedTaskException += OnUnobservedTaskException;
		}

		private async void OnUnhandledException(
			object sender, UnhandledExceptionEventArgs e
		)
		{
			if (e.ExceptionObject is Exception exception)
			{
				await _errorHandler.ShowErrorAsync(exception);
			}
		}

		private async void OnUnobservedTaskException(
			object? sender, UnobservedTaskExceptionEventArgs e
		)
		{
			await _errorHandler.ShowErrorAsync(e.Exception);

			e.SetObserved();
		}
	}
}