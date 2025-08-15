using TemplateServices.Core.ViewModels;

namespace MauiTestApp.Pages
{
	public partial class PayPalPage : BasePage<PayPalPageViewModel>
	{
		public PayPalPage(PayPalPageViewModel viewModel) : base(viewModel)
		{
			InitializeComponent();

			BindingContext = viewModel;
		}
	}
}