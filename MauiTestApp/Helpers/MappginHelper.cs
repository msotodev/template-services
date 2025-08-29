using MauiTestApp.Models.Entities;
using TemplateServices.Domain.Models.Dtos;

namespace MauiTestApp.Helpers
{
	public static class MappginHelper
	{
		public static TodoItemResultDto ConvertTo(TodoItem value) => new()
		{
			CategoryId = value.CategoryId,
			Created = value.Created,
			Description = value.Description,
			Id = value.Id,
			Title = value.Title
		};

		public static TodoItem ConvertTo(TodoItemRequestDto value) => new()
		{
			CategoryId = value.CategoryId,
			Created = DateTime.Now,
			Description = value.Description,
			Id = value.Id,
			Title = value.Title,
			Updated = DateTime.Now
		};

		public static TodoItem ConvertTo(TodoItemResultDto value) => new()
		{
			CategoryId = value.CategoryId,
			Created = DateTime.Now,
			Description = value.Description,
			Id = value.Id,
			Title = value.Title,
			Updated = DateTime.Now
		};

		/**/

		public static TodoItemCategoryResultDto ConvertTo(TodoItemCategory value) => new()
		{
			Title = value.Title,
			Id = value.Id
		};

		public static TodoItemCategory ConvertTo(TodoItemCategoryRequestDto value) => new()
		{
			Created = DateTime.Now,
			Id = value.Id,
			Title = value.Title,
			Updated = DateTime.Now
		};

		public static TodoItemCategory ConvertTo(TodoItemCategoryResultDto value) => new()
		{
			Created = DateTime.Now,
			Id = value.Id,
			Title = value.Title,
			Updated = DateTime.Now,
		};
	}
}