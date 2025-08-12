using MauiTestApp.Extensions;
using QRCoder;
using SkiaSharp;
using System.Collections;
using TemplateServices.Core.Services.App;
using static QRCoder.QRCodeGenerator;
using static TemplateServices.Core.Models.Types.QRCodeTypes;
using Color = System.Drawing.Color;

namespace MauiTestApp.Services.App
{
	public class QRCoderService : IQRCodeService
	{
		public Task<byte[]> GenerateAsync(
			string text,
			int size,
			Color? backgroundColor = null,
			Color? foregroundColor = null,
			bool rounded = false,
			CorrectionLevelType correctionLevel = CorrectionLevelType.Low
		)
		{
			ECCLevel eCCLevel = GetECCLevel(correctionLevel);

			if (!rounded)
			{
				using QRCodeGenerator qRCodeGenerator = new();
				using QRCodeData data = qRCodeGenerator.CreateQrCode(text, eCCLevel);
				using PngByteQRCode pngByteQRCode = new(data);

				byte[] bytes = pngByteQRCode.GetGraphic(
					pixelsPerModule: 10,
					darkColor: foregroundColor ?? Color.Black,
					lightColor: backgroundColor ?? Color.White
				);

				return ResizeIfNeeded(bytes, size);
			}
			else
			{
				using QRCodeGenerator qrGenerator = new();
				using QRCodeData qrCodeData = qrGenerator.CreateQrCode(
					text, eCCLevel
				);

				byte[] result = RenderRoundedQr(
					qrCodeData.ModuleMatrix, size, backgroundColor, foregroundColor
				);

				return Task.FromResult(result);
			}
		}

		/**/

		private static byte[] RenderRoundedQr(
			List<BitArray> moduleMatrix,
			int size,
			Color? backgroundColor,
			Color? foregroundColor
		)
		{
			int modules = moduleMatrix.Count;
			float moduleSize = (float)size / modules;
			float cornerRadius = moduleSize * 0.3f;

			SKImageInfo info = new(size, size);
			using SKSurface surface = SKSurface.Create(info);
			SKCanvas canvas = surface.Canvas;
			SKColor sKColor = backgroundColor?.ToSKColor() ?? SKColors.White;

			canvas.Clear(sKColor);

			using SKPaint paint = new()
			{
				Color = foregroundColor?.ToSKColor() ?? SKColors.Black,
				IsAntialias = true,
				StrokeWidth = 4
			};

			for (int row = 0; row < modules; row++)
			{
				for (int col = 0; col < modules; col++)
				{
					if (moduleMatrix[row][col])
					{
						SKRect rect = new(
							col * moduleSize,
							row * moduleSize,
							(col + 1) * moduleSize,
							(row + 1) * moduleSize
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

		private static Task<byte[]> ResizeIfNeeded(
			byte[] originalBytes, int targetSize
		)
		{
			return Task.Run(
				() =>
				{
					using SKBitmap originalBitmap = SKBitmap.Decode(
						originalBytes
					);

					if (originalBitmap.Width == targetSize &&
						originalBitmap.Height == targetSize
					) return originalBytes;

					SKImageInfo info = new(targetSize, targetSize);
					using SKSurface surface = SKSurface.Create(info);
					SKCanvas canvas = surface.Canvas;

					SKRect destRect = new(0, 0, targetSize, targetSize);

					canvas.DrawBitmap(originalBitmap, destRect);

					using SKImage image = surface.Snapshot();
					using SKData data = image.Encode(
						SKEncodedImageFormat.Png, 100
					);

					return data.ToArray();
				}
			);
		}

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
	}
}