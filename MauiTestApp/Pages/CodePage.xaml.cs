using TemplateServices.Domain.ViewModels;

namespace MauiTestApp.Pages
{
	public partial class CodePage : BasePage<CodePageViewModel>
	{
		public CodePage(CodePageViewModel viewModel) : base(viewModel)
		{
			InitializeComponent();
		}
	}
}