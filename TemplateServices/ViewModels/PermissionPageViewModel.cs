using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TemplateServices.Domain.Services.App;

namespace TemplateServices.Domain.ViewModels
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