using EssentialLayers.Helpers.Result;

namespace TemplateServices.Core.Services.App
{
	public interface IOfflineDatabaseService
	{
		Response Delete<T>(T data);

		Response DeleteAll<T>();

		ResultHelper<IList<T>> FindAll<T>(
			string query, params object[] args
		) where T : new();

		ResultHelper<T> FindFirst<T>(
			string query, params object[] args
		) where T : new();

		T Get<T>(int id) where T : new();

		IList<T> GetAll<T>() where T : new();

		Response New<T>(params T[] data);

		IList<T> Query<T>(
			string query, params object[] args
		) where T : new();

		T QueryFirst<T>(
			string query, params object[] args
		) where T :	new();
		
		Response Reset();

		Response Update<T>(T data);
	}
}