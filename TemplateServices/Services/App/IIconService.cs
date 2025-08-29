using TemplateServices.Domain.Models.Dtos.Icon;

namespace TemplateServices.Domain.Services.App
{
	public interface IIconService
	{
		IList<IconResultDto> All { get; }
	}
}