using EssentialLayers.Helpers.Extension;
using EssentialLayers.Helpers.Result;
using TemplateServices.Domain.Services.App;
using static Microsoft.Maui.ApplicationModel.Permissions;

namespace MauiTestApp.Services.App
{
	public class PermissionService(
		IErrorHandlerService errorHandlerService
	) : IPermissionsService
	{
		public Task<Response> BluetoothAsync() => CheckAsync<Bluetooth>();

		public Task<Response> CameraAsync() => CheckAsync<Camera>();

		public async Task<Response> StorageReadWriteAsync()
		{
			Response read = await CheckAsync<StorageRead>();

			if (read.Ok.False()) return read;

			Response write = await CheckAsync<StorageWrite>();

			return write;
		}

		public Task<Response> LocationAlwaysAsync() => CheckAsync<LocationAlways>();

		public Task<Response> LocationWhenInUseAsync() => CheckAsync<LocationWhenInUse>();

		/**/

		private async Task<Response> CheckAsync<T>() where T : BasePlatformPermission, new()
		{
			try
			{
				PermissionStatus permission = await CheckStatusAsync<T>();

				if (permission != PermissionStatus.Granted)
				{
					permission = await RequestAsync<T>();
				}

				bool granted = permission == PermissionStatus.Granted;

				if (granted.False()) return Response.Fail(
					$"{typeof(T).Name} permission not granted"
				);

				return Response.Success();
			}
			catch (Exception e)
			{
				return errorHandlerService.HandleError(e);
			}
		}
	}
}