using TemplateServices.Domain.ViewModels;

namespace MauiTestApp.Pages
{
	public partial class BluetoothPage : BasePage<BluetoothPageViewModel>
	{
		public BluetoothPage(BluetoothPageViewModel viewModel) : base(viewModel)
		{
			InitializeComponent();
		}
	}
}