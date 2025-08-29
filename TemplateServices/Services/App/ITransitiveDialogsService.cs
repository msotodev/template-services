namespace TemplateServices.Domain.Services.App
{
	public interface ITransitiveDialogsService
	{
		Task ErrorAsync(string message, string accept = "Accept");

		Task InfoAsync(string message, string accept = "Accept");

		Task WarningAsync(string message, string accept = "Accept");
	}
}