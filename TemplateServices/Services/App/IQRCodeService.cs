using System.Drawing;
using static TemplateServices.Core.Models.Types.QRCodeTypes;

namespace TemplateServices.Core.Services.App
{
	public interface IQRCodeService
	{
		Task<byte[]> GenerateAsync(
			string text,
			int size,
			Color? backgroundColor = null,
			Color? foregroundColor = null,
			bool rounded = false,
			CorrectionLevelType correctionLevel = CorrectionLevelType.Low
		);
	}
}