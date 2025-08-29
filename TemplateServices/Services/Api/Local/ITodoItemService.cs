using EssentialLayers.Helpers.Result;
using TemplateServices.Domain.Models.Dtos;

namespace TemplateServices.Domain.Services.Api.Local
{
	public interface ITodoItemService
	{
		IList<TodoItemResultDto> All { get; }

		/**/

		Response Delete(TodoItemResultDto result);

		ResultHelper<TodoItemResultDto> Get(int id);

		Response New(TodoItemRequestDto request);

		Response Update(TodoItemRequestDto request);
	}
}