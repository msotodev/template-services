using EssentialLayers.Helpers.Result;
using TemplateServices.Core.Models;

namespace TemplateServices.Core.Services.App
{
	public interface IBluetoothService
	{
		bool IsConnected { get; }

		IList<DeviceModel> Devices { get; }

		event EventHandler<byte[]> Received;

		event EventHandler<DeviceModel> DeviceDiscovered;

		/**/

		Task<Response> ConnectAsync(Guid guid, bool startListenValues = false);

		Task<Response> Disconnect(Guid guid);

		Task ScanAsync(TimeSpan? secondsTimeOut = null);

		Task<Response> WriteAsync(Guid guid, Guid serviceId, byte[] bytes);
	}
}