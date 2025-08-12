using System.Globalization;

namespace MauiTestApp.Handlers.Converters
{
	public class BytesConverter : IValueConverter
	{
		public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			if (value is byte[] bytes) return ImageSource.FromStream(
				() => new MemoryStream(bytes)
			);

			return null;
		}

		public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			return null;
		}
	}
}