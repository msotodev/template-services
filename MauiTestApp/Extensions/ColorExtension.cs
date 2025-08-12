using SkiaSharp;
using Color = System.Drawing.Color;

namespace MauiTestApp.Extensions
{
	public static class ColorExtension
	{
		public static SKColor ToSKColor(this Color color) => new (
			color.R, color.G, color.B, color.A
		);
	}
}