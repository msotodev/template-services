using TemplateServices.Domain.ViewModels;

namespace MauiTestApp.Pages
{
	public partial class MainPage : BasePage<MainPageViewModel>
	{
		public MainPage(MainPageViewModel viewModel) : base(viewModel)
		{
			InitializeComponent();
		}
	}
}