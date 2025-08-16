using EssentialLayers.Helpers.Extension;
using EssentialLayers.Helpers.Result;
using MauiTestApp.Handlers.Error.Exceptions;
using MauiTestApp.Models.Entities;
using Microsoft.Extensions.Logging;
using SQLite;
using TemplateServices.Core.Services.App;

namespace MauiTestApp.Services.Api.Local
{
	public class SQLitePlcDatabaseService : IOfflineDatabaseService
	{
		private readonly IErrorHandlerService _errorHandlerService;

		private readonly ILogger _logger;

		private readonly IPermissionsService _permissionsService;

		/**/

		private readonly SQLiteConnection Connection;

		/**/

		public SQLitePlcDatabaseService(
			IErrorHandlerService errorHandlerService,
			ILogger<SQLitePlcDatabaseService> logger,
			IPermissionsService permissionsService
		)
		{
			_errorHandlerService = errorHandlerService;
			_logger = logger;
			_permissionsService = permissionsService;

			Connection = GetConnection();

			CreateEntities();
		}

		public Response Delete<T>(T data)
		{
			try
			{
				int deleted = Connection.Delete(data);

				if (deleted == 0) throw new NoRecordWereAffectedException();

				return Response.Success();
			}
			catch (Exception e)
			{
				return _errorHandlerService.HandleError(e);
			}
		}

		public Response DeleteAll<T>()
		{
			try
			{
				string tableName = typeof(T).Name;

				int deleted = Connection.Execute($"DELETE FROM {tableName};");
				int deletedSequence = Connection.Execute($"DELETE FROM sqlite_sequence WHERE name='{tableName}';");

				if (deleted == 0 || deletedSequence == 0) throw new NoRecordWereAffectedException();

				return Response.Success();
			}
			catch (Exception e)
			{
				return _errorHandlerService.HandleError(e);
			}
		}

		public ResultHelper<IList<T>> FindAll<T>(
			string query, params object[] args
		) where T : new()
		{
			try
			{
				IEnumerable<T>? deferred = Connection.DeferredQuery<T>(
					query, args
				);

				_logger.LogInformation("Query => {query}", query);

				return ResultHelper<IList<T>>.Success([.. deferred]);
			}
			catch (Exception e)
			{
				return _errorHandlerService.HandleError<IList<T>>(e);
			}
		}

		public ResultHelper<T> FindFirst<T>(
			string query, params object[] args
		) where T : new()
		{
			try
			{
				T first = Connection.FindWithQuery<T>(query, args);

				_logger.LogInformation("Query => {query}", query);

				return ResultHelper<T>.Success(first);
			}
			catch (Exception e)
			{
				return _errorHandlerService.HandleError<T>(e);
			}
		}

		public T Get<T>(int id) where T : new()
		{
			try
			{
				string tableName = typeof(T).Name;

				List<T> query = Connection.Query<T>(
					$"SELECT * FROM {tableName} WHERE Id = ?;", id
				);

				if (query.NotEmpty() && query.SingleOne())
				{
					return query.FirstOrDefault()!;
				}

				return new();
			}
			catch (Exception e)
			{
				_logger.LogError(e, "Error => {Message}", e.Message);

				return new();
			}
		}

		public IList<T> GetAll<T>() where T : new()
		{
			try
			{
				string tableName = typeof(T).Name;

				List<T> query = Connection.Query<T>(
					$"SELECT * FROM {tableName};"
				);

				return query;
			}
			catch (Exception e)
			{
				_logger.LogError(e, "Error => {Message}", e.Message);

				return [];
			}
		}

		public Response New<T>(params T[] data)
		{
			try
			{
				string tableName = typeof(T).Name;

				int inserted = Connection.InsertAll(data);

				if (inserted == 0) throw new NoRecordWereAffectedException();

				return Response.Success();
			}
			catch (Exception e)
			{
				return _errorHandlerService.HandleError(e);
			}
		}

		public IList<T> Query<T>(
			string query, params object[] args
		) where T : new()
		{
			try
			{
				string tableName = typeof(T).Name;

				List<T> queries = Connection.Query<T>(query, args);

				_logger.LogInformation("Query => {query}", query);

				return queries;
			}
			catch (Exception e)
			{
				_logger.LogError(e, "Error => {Message}", e.Message);

				return [];
			}
		}

		public T QueryFirst<T>(
			string query, params object[] args
		) where T : new()
		{
			try
			{
				string tableName = typeof(T).Name;

				_logger.LogInformation("Query => {query}", query);

				List<T> queries = Connection.Query<T>(query, args);

				return queries.FirstOrDefault()!;
			}
			catch (Exception e)
			{
				_logger.LogError(e, "Error => {Message}", e.Message);

				return new T();
			}
		}

		public Response Reset()
		{
			try
			{
				Connection.DropTable<TodoItem>();
				Connection.DropTable<TodoItemCategory>();

				return CreateEntities();
			}
			catch (Exception e)
			{
				return _errorHandlerService.HandleError(e);
			}
		}

		public Response Update<T>(T data)
		{
			try
			{
				int updated = Connection.Update(data);

				if (updated == 0) throw new NoRecordWereAffectedException();

				return Response.Success();
			}
			catch (Exception e)
			{
				return _errorHandlerService.HandleError(e);
			}
		}

		/**/

		private SQLiteConnection GetConnection()
		{
			_permissionsService.StorageReadWriteAsync();

			string databaseFilename = "app_data.db3";
			string folderPath = GetFolderPath();
			string databasePath = Path.Combine(folderPath, databaseFilename);

			return new SQLiteConnection(databasePath);
		}

		private static string GetFolderPath()
		{
			if (DeviceInfo.Platform == DevicePlatform.Android) return FileSystem.AppDataDirectory;
			else if (DeviceInfo.Platform == DevicePlatform.iOS) return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			else if (DeviceInfo.Platform == DevicePlatform.WinUI) return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
			else if (DeviceInfo.Platform == DevicePlatform.MacCatalyst) return Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			else return string.Empty;
		}

		private Response CreateEntities()
		{
			try
			{
				Connection.CreateTable<TodoItem>();
				Connection.CreateTable<TodoItemCategory>();

				return Response.Success();
			}
			catch (Exception e)
			{
				return _errorHandlerService.HandleError(e);
			}
		}
	}
}