using TemplateServices.Core.Models.Dtos.Icon;

namespace TemplateServices.Core.Services.App
{
	public interface IIconService
	{
		IList<IconResultDto> All { get; }
	}
}