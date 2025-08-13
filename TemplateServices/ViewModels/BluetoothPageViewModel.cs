using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EssentialLayers.Helpers.Extension;
using EssentialLayers.Helpers.Result;
using System.Collections.ObjectModel;
using TemplateServices.Core.Models;
using TemplateServices.Core.Services.App;

namespace TemplateServices.Core.ViewModels
{
	public partial class BluetoothPageViewModel(
		IBluetoothService bluetoothService,
		ITransitiveDialogsService transitiveDialogsService
	) : ObservableObject
	{
		[ObservableProperty]
		private ObservableCollection<DeviceModel> devices = [];

		[ObservableProperty]
		private DeviceModel? selectedDevice;

		[ObservableProperty]
		private bool isRefreshing = false;

		/**/

		[RelayCommand]
		private void Appearing()
		{
			bluetoothService.DeviceDiscovered -= OnDeviceDiscovered;
			bluetoothService.DeviceDiscovered += OnDeviceDiscovered;

			Refresh();

			bluetoothService.ScanAsync();
		}

		[RelayCommand]
		private async Task ConnectAsync(DeviceModel device)
		{
			Response response = await bluetoothService.ConnectAsync(device.Id);

			if (response.Ok.False()) await transitiveDialogsService.ErrorAsync(response.Message);
			else await transitiveDialogsService.InfoAsync($"Connected to {device.MacAddress}");
		}

		[RelayCommand]
		public void Refresh()
		{
			IsRefreshing = true;

			Devices = bluetoothService.Devices.ToObservableCollection();

			IsRefreshing = false;
		}

		private void OnDeviceDiscovered(object? sender, DeviceModel device)
		{
			Devices.Add(device);
		}
	}
}