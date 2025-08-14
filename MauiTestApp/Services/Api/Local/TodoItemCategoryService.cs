using EssentialLayers.Helpers.Result;
using MauiTestApp.Helpers;
using MauiTestApp.Models.Entities;
using TemplateServices.Core.Models.Dtos;
using TemplateServices.Core.Services.Api.Local;
using TemplateServices.Core.Services.App;

namespace MauiTestApp.Services.Api.Local
{
	public class TodoItemCategoryService(
		IOfflineDatabaseService offlineDatabaseService
	) : ITodoItemCategoryService
	{
		public IList<TodoItemCategoryResultDto> All
		{
			get
			{
				IList<TodoItemCategory> result = offlineDatabaseService.GetAll<TodoItemCategory>();

				return result.ToList().ConvertAll(MappginHelper.ConvertTo);
			}
		}

		public Response Delete(TodoItemCategoryResultDto result)
		{
			TodoItemCategory get = offlineDatabaseService.Get<TodoItemCategory>(result.Id);

			return offlineDatabaseService.Delete(get);
		}

		public TodoItemCategoryResultDto Get(int id) => MappginHelper.ConvertTo(
			offlineDatabaseService.Get<TodoItemCategory>(id)
		);

		public Response New(
			TodoItemCategoryRequestDto request
		) => offlineDatabaseService.New(MappginHelper.ConvertTo(request));

		public Response Update(
			TodoItemCategoryRequestDto request
		) => offlineDatabaseService.Update(MappginHelper.ConvertTo(request));
	}
}