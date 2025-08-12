using TemplateServices.Core.ViewModels;

namespace MauiTestApp.Pages
{
	public partial class DialogPage : BasePage<DialogPageViewModel>
	{
		public DialogPage(DialogPageViewModel viewModel) : base(viewModel)
		{
			InitializeComponent();
		}
	}
}