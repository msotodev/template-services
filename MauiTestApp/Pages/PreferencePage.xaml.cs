using TemplateServices.Domain.ViewModels;

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