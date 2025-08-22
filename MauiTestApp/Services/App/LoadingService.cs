using TemplateServices.Core.Services.App;

namespace MauiTestApp.Services.App
{
	public class LoadingService : ILoadingService
	{
		private bool _loading;

		private string _message = string.Empty;

		/**/

		public event Action<bool>? Changed;

		public bool Loading => _loading;

		public string Message => _message;

		/**/

		public void Hide()
		{
			_loading = false;
			_message = string.Empty;

			Changed?.Invoke(false);
		}

		public void Show(string message = "")
		{
			_loading = true;
			_message = message;

			Changed?.Invoke(true);
		}
	}
}