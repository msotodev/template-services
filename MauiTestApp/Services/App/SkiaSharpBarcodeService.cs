using MauiTestApp.Extensions;
using SkiaSharp;
using TemplateServices.Core.Services.App;
using Color = System.Drawing.Color;

namespace MauiTestApp.Services.App
{
	public class SkiaSharpBarcodeService : IBarcodeService
	{
		public Task<byte[]> GenerateAsync(
			string value,
			int width,
			int height,
			Color? backgroundColor = null,
			Color? foregroundColor = null
		)
		{
			SKImageInfo info = new(width, height);
			using SKSurface surface = SKSurface.Create(info);
			SKCanvas canvas = surface.Canvas;

			canvas.Clear(foregroundColor?.ToSKColor() ?? SKColors.White);

			// Drawing simple barcode (Code 39)
			DrawSimpleBarcode(canvas, value, width, height);

			using SKImage image = surface.Snapshot();
			using SKData data = image.Encode(SKEncodedImageFormat.Png, 100);

			return Task.FromResult(data.ToArray());
		}

		/**/

		private static void DrawSimpleBarcode(
			SKCanvas canvas,
			string value,
			int width,
			int height,
			Color? foregroundColor = null
		)
		{
			SKPaint paint = new()
			{
				Color = foregroundColor?.ToSKColor() ?? SKColors.Black,
				StrokeWidth = 4
			};

			// Drawing vertical bars based in the value
			float barWidth = width / (float)(value.Length * 8);
			float x = 0;

			foreach (char c in value)
			{
				int pattern = c % 8; // Simple pattern based in the character

				for (int i = 0; i < 8; i++)
				{
					if ((pattern & (1 << i)) != 0)
					{
						canvas.DrawRect(x, 0, barWidth, height, paint);
					}

					x += barWidth;
				}
			}
		}
	}
}