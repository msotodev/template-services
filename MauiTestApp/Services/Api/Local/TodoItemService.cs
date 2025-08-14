using EssentialLayers.Helpers.Result;
using MauiTestApp.Helpers;
using MauiTestApp.Models.Entities;
using System.Diagnostics;
using TemplateServices.Core.Models.Dtos;
using TemplateServices.Core.Services.Api.Local;
using TemplateServices.Core.Services.App;

namespace MauiTestApp.Services.Api.Local
{
	public class TodoItemService(
		IOfflineDatabaseService offlineDatabaseService
	) : ITodoItemService
	{
		public IList<TodoItemResultDto> All
		{
			get
			{
				string query = $"SELECT T.Id, T.Title, T.Description,\n" +
					$"\tT.CategoryId, C.Title [CategoryName], T.Created\n" +
					$"FROM {nameof(TodoItem)} T\n" +
					$"\tINNER JOIN {nameof(TodoItemCategory)} C ON C.Id = T.CategoryId;";

				IList<TodoItemResultDto> result = offlineDatabaseService.Query<TodoItemResultDto>(
					query
				);

				Debug.WriteLine(query);

				return result;
			}
		}

		/**/

		public Response Delete(
			TodoItemResultDto result
		)
		{
			TodoItem get = offlineDatabaseService.Get<TodoItem>(result.Id);

			return offlineDatabaseService.Delete(get);
		}

		public ResultHelper<TodoItemResultDto> Get(int id)
		{
			string query = $"SELECT T.Id, T.Title, T.Description,\n" +
					$"\tT.CategoryId, C.Title [CategoryName], T.Created\n" +
					$"FROM {nameof(TodoItem)} T\n" +
					$"\tINNER JOIN {nameof(TodoItemCategory)} C ON C.Id = T.CategoryId\n" +
					$"WHERE T.Id = {id};";

			ResultHelper<TodoItemResultDto> result = offlineDatabaseService.FindFirst<TodoItemResultDto>(
				query
			);

			return result;
		}

		public Response New(
			TodoItemRequestDto request
		) => offlineDatabaseService.New(MappginHelper.ConvertTo(request));

		public Response Update(
			TodoItemRequestDto request
		) => offlineDatabaseService.Update(MappginHelper.ConvertTo(request));
	}
}