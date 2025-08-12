using QRCoder;
using SkiaSharp;
using TemplateServices.Core.Services.App;
using ZXing.SkiaSharp;
using static QRCoder.QRCodeGenerator;
using static TemplateServices.Core.Models.Types.QRCodeTypes;

namespace MauiTestApp.Services.App
{
	public class QRCoderService : IQRCodeService
	{
		public Task<byte[]> GenerateAsync(
			string text, int pixelsPerModule = 20, CorrectionLevelType correctionLevel = CorrectionLevelType.Low
		)
		{
			ECCLevel eCCLevel = GetECCLevel(correctionLevel);

			using QRCodeGenerator qRCodeGenerator = new();
			using QRCodeData data = qRCodeGenerator.CreateQrCode(text, eCCLevel);
			using PngByteQRCode pngByteQRCode = new(data);

			byte[] bytes = pngByteQRCode.GetGraphic(pixelsPerModule);

			return Task.FromResult(bytes);
		}

		public Task<byte[]> GenerateRoundedAsync(
			string text, int size = 200, CorrectionLevelType correctionLevel = CorrectionLevelType.Low
		)
		{
			return Task.Run(() =>
			{
				var writer = new BarcodeWriter
				{
					Format = ZXing.BarcodeFormat.QR_CODE,
					Options = new ZXing.QrCode.QrCodeEncodingOptions
					{
						Width = size,
						Height = size,
						Margin = 0,
						ErrorCorrection = ZXing.QrCode.Internal.ErrorCorrectionLevel.M
					}
				};

				using SKBitmap qrBitmap = writer.Write(text);

				return CreateRoundedVersion(qrBitmap, size);
			});
		}

		/**/

		private static ECCLevel GetECCLevel(
			CorrectionLevelType correctionLevel
		) => correctionLevel switch
		{
			CorrectionLevelType.Low => ECCLevel.L,
			CorrectionLevelType.Medium => ECCLevel.M,
			CorrectionLevelType.Quartile => ECCLevel.Q,
			CorrectionLevelType.High => ECCLevel.H,
			_ => ECCLevel.L
		};

		private static byte[] CreateRoundedVersion(
			SKBitmap originalBitmap, int size
		)
		{
			SKImageInfo info = new(size, size);
			using SKSurface surface = SKSurface.Create(info);
			SKCanvas canvas = surface.Canvas;

			canvas.Clear(SKColors.Red);

			// Analizar el bitmap original y dibujar módulos redondeados
			int moduleSize = size / originalBitmap.Width;
			float cornerRadius = moduleSize * 0.3f;

			using SKPaint paint = new()
			{
				Color = SKColors.Black,
				IsAntialias = true
			};

			for (int x = 0; x < originalBitmap.Width; x++)
			{
				for (int y = 0; y < originalBitmap.Height; y++)
				{
					SKColor pixel = originalBitmap.GetPixel(x, y);
					
					if (pixel.Red < 128) // Es negro (módulo activo)
					{
						SKRect rect = new(
							x * moduleSize,
							y * moduleSize,
							(x + 1) * moduleSize,
							(y + 1) * moduleSize
						);
						
						canvas.DrawRoundRect(
							rect, cornerRadius, cornerRadius, paint
						);
					}
				}
			}

			using SKImage image = surface.Snapshot();
			using SKData data = image.Encode(
				SKEncodedImageFormat.Png, 100
			);
			
			return data.ToArray();
		}
	}
}