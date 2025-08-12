using TemplateServices.Core.ViewModels;

namespace MauiTestApp.Pages
{
	public partial class PermissionPage : BasePage<PermissionPageViewModel>
	{
		public PermissionPage(PermissionPageViewModel viewModel) : base(viewModel)
		{
			InitializeComponent();
		}
	}
}