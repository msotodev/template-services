using MauiTestApp.Pages;
using TemplateServices.Core.Helpers.Constants;

namespace MauiTestApp
{
	public partial class AppShell : Shell
	{
		public AppShell()
		{
			InitializeComponent();

			RegisterRoutes();
		}

		private static void RegisterRoutes()
		{
			Routing.RegisterRoute(RoutesConstant.BLUETOOTH, typeof(BluetoothPage));
			Routing.RegisterRoute(RoutesConstant.CODE, typeof(CodePage));
			Routing.RegisterRoute(RoutesConstant.DIALOG, typeof(DialogPage));
			Routing.RegisterRoute(RoutesConstant.PERMISSION, typeof(PermissionPage));
		}
	}
}