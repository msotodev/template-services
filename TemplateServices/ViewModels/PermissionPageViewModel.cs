using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TemplateServices.Core.Services.App;

namespace TemplateServices.Core.ViewModels
{
	public partial class PermissionPageViewModel(
		IPermissionsService permissionsService
	) : ObservableObject
	{
		[RelayCommand]
		private Task BlutoothAsync() => permissionsService.BluetoothAsync();

		[RelayCommand]
		private Task CameraAsync() => permissionsService.CameraAsync();

		[RelayCommand]
		private Task LocationAlwaysAsync() => permissionsService.LocationAlwaysAsync();

		[RelayCommand]
		private Task LocationWhenInUseAsync() => permissionsService.LocationWhenInUseAsync();

		[RelayCommand]
		private Task StorageReadWriteAsync() => permissionsService.StorageReadWriteAsync();
	}
}