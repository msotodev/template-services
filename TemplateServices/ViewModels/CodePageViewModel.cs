using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TemplateServices.Domain.Services.App;

namespace TemplateServices.Domain.ViewModels
{
	public partial class CodePageViewModel(
		IBarcodeService barcodeService,
		IQRCodeService qRCodeService
	) : ObservableObject
	{
		[ObservableProperty]
		private string text = "This is an example of text to show in your codes";

		[ObservableProperty]
		private byte[] barcode = [];

		[ObservableProperty]
		private byte[] qrCode = [];

		[ObservableProperty]
		private bool rounded = false;

		/**/

		[RelayCommand]
		private async Task GenerateBarcodeAsync()
		{
			Barcode = await barcodeService.GenerateAsync(Text, 200, 100);
		}

		[RelayCommand]
		private async Task GenerateQRCodeAsync()
		{
			QrCode = await qRCodeService.GenerateAsync(
				Text, 200, rounded: Rounded
			);
		}
	}
}