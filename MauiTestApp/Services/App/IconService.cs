using MauiTestApp.Handlers.Icons;
using System.Reflection;
using TemplateServices.Core.Models.Dtos.Icon;
using TemplateServices.Core.Services.App;

namespace MauiTestApp.Services.App
{
	internal class IconService : IIconService
	{
		public IList<IconResultDto> All => GetAll();

		/**/

		private static IList<IconResultDto> GetAll()
		{
			Type obj = typeof(IconFont);

			if (obj == null) return [];

			return [.. obj.GetFields(
				BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy
			).Where(field => field.IsLiteral && !field.IsInitOnly).Select(
				field => {
					string value = field.GetRawConstantValue() is string raw && raw.Length > 0
						? $"\\u{(int)raw[0]:X4}".ToLower()
						: string.Empty;

					return new IconResultDto
					{
						Name = field.Name,
						Value = value
					};
				}
			)];
		}
	}
}