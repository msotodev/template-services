using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TemplateServices.Core.Services.App;

namespace TemplateServices.Core.ViewModels
{
	public partial class CodePageViewModel(
		IBarcodeService barcodeService,
		IQRCodeService qRCodeService
	) : ObservableObject
	{
		[ObservableProperty]
		private string text = string.Empty;

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
			if (Rounded)
			{
				QrCode = await qRCodeService.GenerateRoundedAsync(Text, 200);
			}
			else
			{
				QrCode = await qRCodeService.GenerateAsync(Text);
			}
		}
	}
}