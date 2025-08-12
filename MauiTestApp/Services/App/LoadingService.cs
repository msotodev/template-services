using TemplateServices.Core.Services.App;

namespace MauiTestApp.Services.App
{
	public class LoadingService : ILoadingService
	{
		private bool _loading;

		private string _message = string.Empty;

		/**/

		public event EventHandler<bool>? Changed;

		public bool Loading => _loading;

		public string Message => _message;

		/**/

		public void Hide()
		{
			_loading = false;
			_message = string.Empty;

			Changed?.Invoke(this, false);
		}

		public void Show(string message = "")
		{
			_loading = true;
			_message = message;

			Changed?.Invoke(this, true);
		}

		public async Task ShowAsync(string message = "", int delay = 100)
		{
			await Task.Delay(delay);

			_loading = true;
			_message = message;

			Changed?.Invoke(this, true);
		}
	}
}