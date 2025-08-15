using EssentialLayers.Helpers.Result;

namespace TemplateServices.Core.Services.App
{
	public interface IWebToolsService
	{
		Task<Response> OpenUriAsync(Uri uri);

		Task<Response> OpenFileAsync(string path, string title = "");
	}
}