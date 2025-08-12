using EssentialLayers.Helpers.Result;

namespace TemplateServices.Core.Services.App
{
	public interface IPermissionsService
	{
		Task<Response> BluetoothAsync();

		Task<Response> CameraAsync();

		Task<Response> LocationAlwaysAsync();

		Task<Response> LocationWhenInUseAsync();

		Task<Response> StorageReadWriteAsync();
	}
}