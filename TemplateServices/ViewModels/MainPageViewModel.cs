using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TemplateServices.Core.Helpers.Constants;
using TemplateServices.Core.Services.App;

namespace TemplateServices.Core.ViewModels
{
	public partial class MainPageViewModel(
		INavigationService navigationService	
	) : ObservableObject
	{
		[RelayCommand]
		private Task BluetoothAsync() => navigationService.NavigateToAsync(
			RoutesConstant.BLUETOOTH
		);

		[RelayCommand]
		private Task CodeAsync() => navigationService.NavigateToAsync(
			RoutesConstant.CODE
		);

		[RelayCommand]
		private Task DialogAsync() => navigationService.NavigateToAsync(
			RoutesConstant.DIALOG
		);

		[RelayCommand]
		private Task PermissionAsync() => navigationService.NavigateToAsync(
			RoutesConstant.PERMISSION
		);

		[RelayCommand]
		private Task PreferenceAsync() => navigationService.NavigateToAsync(
			RoutesConstant.PREFERENCE
		);

		[RelayCommand]
		private Task TodoItemAsync() => navigationService.NavigateToAsync(
			RoutesConstant.TODO_ITEM
		);

		[RelayCommand]
		private Task IconsAsync() => navigationService.NavigateToAsync(
			RoutesConstant.ICONS
		);
	}
}