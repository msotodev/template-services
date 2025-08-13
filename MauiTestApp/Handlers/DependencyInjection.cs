using MauiTestApp.Pages;
using MauiTestApp.Services.App;
using TemplateServices.Core.Services.App;
using TemplateServices.Core.ViewModels;

namespace MauiTestApp.Handlers
{
	public static class DependencyInjection
	{
		public static void RegisterServices(IServiceCollection services)
		{
			services.AddSingleton<ILocalizationService, LocalizationService>();
			services.AddSingleton<IErrorHandlerService, ErrorHandlerService>();
			services.AddSingleton<IPreferencesService, PreferencesService>();

			services.AddSingleton<IFixedDialogService, FixedDialogService>();
			services.AddSingleton<ITransitiveDialogsService, TransitiveDialogsService>();
			services.AddSingleton<IBluetoothService, PluginBLEBluetoothService>();
			services.AddSingleton<IPermissionsService, PermissionService>();
			services.AddSingleton<ILoadingService, LoadingService>();
			services.AddSingleton<INavigationService, NavigationService>();
			services.AddSingleton<IBarcodeService, SkiaSharpBarcodeService>();
			services.AddSingleton<IQRCodeService, QRCoderService>();
		}

		public static void RegisterViewModels(IServiceCollection services)
		{
			services.AddTransient<BluetoothPageViewModel>();
			services.AddTransient<CodePageViewModel>();
			services.AddTransient<DialogPageViewModel>();
			services.AddTransient<MainPageViewModel>();
			services.AddTransient<PermissionPageViewModel>();
			services.AddTransient<PreferencePageViewModel>();
		}

		public static void RegisterPages(IServiceCollection services)
		{
			services.AddTransient<BluetoothPage>();
			services.AddTransient<CodePage>();
			services.AddTransient<DialogPage>();
			services.AddTransient<MainPage>();
			services.AddTransient<PermissionPage>();
			services.AddTransient<PreferencePage>();
		}
	}
}