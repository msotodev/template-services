using MauiTestApp.Pages;
using MauiTestApp.Services.Api.Local;
using MauiTestApp.Services.App;
using TemplateServices.Core.Services.Api.Local;
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
			services.AddSingleton<IOfflineDatabaseService, SQLitePlcDatabaseService>();

			services.AddSingleton<ITodoItemService, TodoItemService>();
			services.AddSingleton<ITodoItemCategoryService, TodoItemCategoryService>();
		}

		public static void RegisterViewModels(IServiceCollection services)
		{
			services.AddTransient<BluetoothPageViewModel>();
			services.AddTransient<CodePageViewModel>();
			services.AddTransient<DialogPageViewModel>();
			services.AddTransient<MainPageViewModel>();
			services.AddTransient<PermissionPageViewModel>();
			services.AddTransient<PreferencePageViewModel>();
			services.AddTransient<TodoItemPageViewModel>();
			services.AddTransient<TodoItemFormPageViewModel>();
			services.AddTransient<TodoItemCategoryFormPageViewModel>();
		}

		public static void RegisterPages(IServiceCollection services)
		{
			services.AddTransient<BluetoothPage>();
			services.AddTransient<CodePage>();
			services.AddTransient<DialogPage>();
			services.AddTransient<MainPage>();
			services.AddTransient<PermissionPage>();
			services.AddTransient<PreferencePage>();
			services.AddTransient<TodoItemPage>();
			services.AddTransient<TodoItemFormPage>();
			services.AddTransient<TodoItemCategoryFormPage>();
		}
	}
}