using TemplateServices.Core.ViewModels;

namespace MauiTestApp.Pages
{
	public partial class IconsPage : BasePage<IconsPageViewModel>
	{
		private readonly IconsPageViewModel _viewModel;

		public IconsPage(IconsPageViewModel viewModel) : base(viewModel)
		{
			InitializeComponent();

			BindingContext = _viewModel = viewModel;
		}

		protected override void OnAppearing()
		{
			_viewModel.AppearingCommand.Execute(this);

			base.OnAppearing();
		}
	}
}