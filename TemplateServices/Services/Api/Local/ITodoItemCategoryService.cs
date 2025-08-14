using EssentialLayers.Helpers.Result;
using TemplateServices.Core.Models.Dtos;

namespace TemplateServices.Core.Services.Api.Local
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