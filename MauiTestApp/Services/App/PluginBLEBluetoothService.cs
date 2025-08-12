using EssentialLayers.Helpers.Extension;
using EssentialLayers.Helpers.Result;
using Plugin.BLE;
using Plugin.BLE.Abstractions;
using Plugin.BLE.Abstractions.Contracts;
using TemplateServices.Core.Models;
using TemplateServices.Core.Services.App;

namespace MauiTestApp.Services.App
{
	public class PluginBLEBluetoothService(
		IPermissionsService permissionsService,
		IErrorHandlerService errorHandlerService
	) : IBluetoothService
	{
		private readonly IPermissionsService _permissionsService = permissionsService;

		private readonly IAdapter _adapter = CrossBluetoothLE.Current.Adapter;

		private IDevice? _connectedDevice;

		private readonly IList<DeviceModel> _devices = [];

		private bool _isListening;

		/**/

		public bool IsConnected => _connectedDevice != null;

		public IList<DeviceModel> Devices => _devices;

		public event EventHandler<DeviceModel>? DeviceDiscovered;

		public event EventHandler<byte[]>? Received;

		public async Task<Response> ConnectAsync(Guid guid, bool startListenValues = false)
		{
			try
			{
				Response bluetooth = await RequestPermissions();

				if (bluetooth.Ok.False()) return bluetooth;

				IDevice device = _adapter.DiscoveredDevices.Single(d => d.Id == guid);

				ConnectParameters parameters = new(
					autoConnect: false, forceBleTransport: true
				);

				await _adapter.ConnectToDeviceAsync(device, parameters);

				_connectedDevice = device;

				if (startListenValues) await StartListenValueChanges(_connectedDevice);

				return Response.Success();
			}
			catch (Exception e)
			{
				return errorHandlerService.HandleError(e);
			}
		}

		public async Task<Response> Disconnect(Guid guid)
		{
			try
			{
				IDevice? device = _adapter.ConnectedDevices.FirstOrDefault(x => x.Id == guid);

				if (device.NotNull()) return Response.Fail("El dispositivo no esta conectado");

				await _adapter.DisconnectDeviceAsync(device);

				device = null;

				return Response.Success();
			}
			catch (Exception e)
			{
				return errorHandlerService.HandleError(e);
			}
		}

		public async Task ScanAsync(TimeSpan? secondsTimeOut = null)
		{
			await RequestPermissions();

			_adapter.DeviceDiscovered += (sender, device) =>
			{
				string macAddress = device.Device.NativeDevice.NotNull() ? $"{device.Device.NativeDevice}" : string.Empty;

				if (_devices.Any(x => x.MacAddress == macAddress).False())
				{
					_devices.Add(ConvertTo(device.Device));

					DeviceDiscovered?.Invoke(this, ConvertTo(device.Device));
				}
			};

			_adapter.ScanTimeout = secondsTimeOut != null ? secondsTimeOut.Value.Milliseconds : 5000;

			await _adapter.StartScanningForDevicesAsync();
		}

		public async Task<Response> WriteAsync(Guid guid, Guid serviceId, byte[] bytes)
		{
			try
			{
				Response bluetooth = await _permissionsService.BluetoothAsync();

				if (bluetooth.Ok.False()) return bluetooth;

				_connectedDevice = await _adapter.ConnectToKnownDeviceAsync(guid);

				IReadOnlyList<IService> services = await _connectedDevice.GetServicesAsync();

				IService? service = services.FirstOrDefault(s => s.Id == serviceId);

				if (service == null) return Response.Fail("Service was not founded");

				IReadOnlyList<ICharacteristic> characteristics = await service.GetCharacteristicsAsync();

				ICharacteristic? characteristic = characteristics.FirstOrDefault(c => c.CanWrite);

				if (characteristic == null) return Response.Fail("Characteristic was not founded");

				await characteristic.WriteAsync(bytes);

				return Response.Success();
			}
			catch (Exception e)
			{
				return errorHandlerService.HandleError(e);
			}
		}

		/**/

		private async Task<ResultHelper<bool>> StartListenValueChanges(IDevice device)
		{
			if (_isListening) return ResultHelper<bool>.Success(true);

			_isListening = true;

			try
			{
				IReadOnlyList<IService> services = await device.GetServicesAsync();
				IReadOnlyList<ICharacteristic> characteristics = await services[2].GetCharacteristicsAsync();

				foreach (ICharacteristic characteristic in characteristics)
				{
					if (characteristic.Properties.HasFlag(flag: CharacteristicPropertyType.Notify | CharacteristicPropertyType.Indicate))
					{
						characteristic.ValueUpdated += (s, e) =>
						{
							byte[] bytes = e.Characteristic.Value;

							if (bytes != null && bytes.NotEmpty())
							{
								Received?.Invoke(
									this, bytes
								);
							}
						};

						await characteristic.StartUpdatesAsync();
					}
				}

				return ResultHelper<bool>.Success(true);
			}
			catch (Exception e)
			{
				_isListening = false;
				return ResultHelper<bool>.Fail(e);
			}
		}

		private static DeviceModel ConvertTo(IDevice device)
		{
			Guid id = device.Id.NotNull() ? device.Id : Guid.Empty;
			string name = device.Name.NotEmpty() ? device.Name! : string.Empty;
			string macAddress = device.NativeDevice.NotNull() ? $"{device.NativeDevice}" : string.Empty;

			return new DeviceModel
			{
				Id = id,
				Name = name,
				MacAddress = macAddress
			};
		}

		private async Task<Response> RequestPermissions()
		{
			Response response = await _permissionsService.BluetoothAsync();

			if (response.Ok.False()) return response;

			return await _permissionsService.LocationWhenInUseAsync();
		}
	}
}