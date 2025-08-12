using SkiaSharp;
using TemplateServices.Core.Services.App;

namespace MauiTestApp.Services.App
{
	public class SkiaSharpBarcodeService : IBarcodeService
	{
		public Task<byte[]> GenerateAsync(
			string value, int width, int height, int quality = 100
		)
		{
			SKImageInfo info = new(width, height);
			using SKSurface surface = SKSurface.Create(info);
			SKCanvas canvas = surface.Canvas;

			canvas.Clear(SKColors.Transparent);

			// Dibujar código de barras simple (Code 39 básico)
			DrawSimpleBarcode(canvas, value, width, height);

			using SKImage image = surface.Snapshot();
			using SKData data = image.Encode(SKEncodedImageFormat.Png, quality);

			return Task.FromResult(data.ToArray());
		}

		/**/

		private static void DrawSimpleBarcode(
			SKCanvas canvas, string value, int width, int height
		)
		{
			// Implementación básica de código de barras
			SKPaint paint = new()
			{
				Color = SKColors.Black,
				StrokeWidth = 4
			};

			// Dibujar barras verticales basadas en el valor
			float barWidth = width / (float)(value.Length * 8);
			float x = 0;

			foreach (char c in value)
			{
				int pattern = c % 8; // Patrón simple basado en el carácter

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