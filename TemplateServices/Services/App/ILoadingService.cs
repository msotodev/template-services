namespace TemplateServices.Domain.Services.App
{
	public interface ILoadingService
	{
		event Action<bool> Changed;

		bool Loading { get; }

		string Message { get; }

		void Show(string message = "");

		void Hide();
	}
}