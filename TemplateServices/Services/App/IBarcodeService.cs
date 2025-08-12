using System.Drawing;

namespace TemplateServices.Core.Services.App
{
	public interface IBarcodeService
	{
		Task<byte[]> GenerateAsync(
			string value,
			int width,
			int height,
			Color? backgroundColor = null,
			Color? foregroundColor = null
		);
	}
}