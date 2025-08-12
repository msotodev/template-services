using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using static MauiTestApp.Handlers.DependencyInjection;

namespace MauiTestApp
{
	public static class MauiProgram
	{
		public static MauiApp CreateMauiApp()
		{
			MauiAppBuilder builder = MauiApp.CreateBuilder();
			
			builder.UseMauiApp<App>().UseMauiCommunityToolkit().ConfigureFonts(
				fonts =>
				{
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
					fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
					fonts.AddFont("MaterialIcons-Regular.ttf", "MaterialIconsRegular");
				}
			);

#if DEBUG
			builder.Logging.AddDebug();
#endif

			RegisterServices(builder.Services);
			RegisterViewModels(builder.Services);
			RegisterPages(builder.Services);

			return builder.Build();
		}
	}
}