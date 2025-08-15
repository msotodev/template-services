using EssentialLayers.Helpers.Result;
using TemplateServices.Core.Services.App;

namespace MauiTestApp.Services.App
{
	public class WebToolsService(
		IErrorHandlerService errorHandlerService
	) : IWebToolsService
	{
		public async Task OpenUriAsync(Uri uri)
		{
			bool canOpen = await Launcher.CanOpenAsync(uri);

			if (canOpen)
			{
				await Launcher.OpenAsync(uri);
			}
		}

		public async Task<Response> OpenFileAsync(string path, string title = "")
		{
			try
			{
				if (File.Exists(path)) throw new DirectoryNotFoundException();

				await Launcher.OpenAsync(
					new OpenFileRequest
					{
						File = new ReadOnlyFile(path),
						Title = title,
					}
				);
			}
			catch (Exception e)
			{
				return errorHandlerService.HandleError(e);
			}
		}
	}
}