using EssentialLayers.Helpers.Result;
using TemplateServices.Domain.Models.Dtos;

namespace TemplateServices.Domain.Services.Api.Local
{
	public interface ITodoItemCategoryService
	{
		IList<TodoItemCategoryResultDto> All { get; }

		/**/

		Response Delete(TodoItemCategoryResultDto result);

		TodoItemCategoryResultDto Get(int id);

		Response New(TodoItemCategoryRequestDto request);

		Response Update(TodoItemCategoryRequestDto request);
	}
}