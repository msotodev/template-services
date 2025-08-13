using TemplateServices.Core.ViewModels;

namespace MauiTestApp.Pages
{
	public partial class PreferencePage : BasePage<PreferencePageViewModel>
	{
		public PreferencePage(
			PreferencePageViewModel viewModel
		) : base(viewModel)
		{
			InitializeComponent();
		}
	}
}