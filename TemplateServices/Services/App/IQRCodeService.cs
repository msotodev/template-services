using static TemplateServices.Domain.Models.Types.QRCodeTypes;

namespace TemplateServices.Domain.Services.App
{
	public interface IQRCodeService
	{
		Task<byte[]> GenerateAsync(
			string text,
			int size,
			System.Drawing.Color? backgroundColor = null,
			System.Drawing.Color? foregroundColor = null,
			bool rounded = false,
			CorrectionLevelType correctionLevel = CorrectionLevelType.Low
		);
	}
}