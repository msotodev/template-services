namespace TemplateServices.Core.Services.App
{
	public interface IBarcodeService
	{
		Task<byte[]> GenerateAsync(
			string value, int width, int height, int quality = 100
		);
	}
}