namespace TemplateServices.Core.Services.App
{
	public interface ILoadingService
	{
		event EventHandler<bool> Changed;

		bool Loading { get; }

		string Message { get; }

		void Show(string message = "");

		void Hide();
	}
}