using static TemplateServices.Core.Models.Types.QRCodeTypes;

namespace TemplateServices.Core.Services.App
{
	public interface IQRCodeService
	{
		Task<byte[]> GenerateAsync(
			string text, int pixelsPerModule = 20, CorrectionLevelType correctionLevel = CorrectionLevelType.Low
		);

		Task<byte[]> GenerateRoundedAsync(
			string text, int pixelsPerModule = 20, CorrectionLevelType correctionLevel = CorrectionLevelType.Low
		);
	}
}