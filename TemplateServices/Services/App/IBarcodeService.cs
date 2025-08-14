namespace TemplateServices.Core.Services.App
{
	public interface IBarcodeService
	{
		Task<byte[]> GenerateAsync(
			string value,
			int width,
			int height,
			System.Drawing.Color? backgroundColor = null,
			System.Drawing.Color? foregroundColor = null
		);
	}
}