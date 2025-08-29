using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EssentialLayers.Helpers.Extension;
using EssentialLayers.Helpers.Result;
using TemplateServices.Domain.Models.Dtos.PayPal;
using TemplateServices.Domain.Services.App;

namespace TemplateServices.Domain.ViewModels
{
	public partial class PayPalPageViewModel(
		IPayPalService payPalService
	) : ObservableObject
	{
		[ObservableProperty]
		private PayPalFeeCalculationDto? feeCalculationDto;

		[ObservableProperty]
		private string ammount = string.Empty;

		[ObservableProperty]
		private string fees = string.Empty;

		/**/

		partial void OnAmmountChanged(string value)
		{
			PayPalFeeCalculationDto calculated = payPalService.CalculatePayPalFees(
				Ammount.ToDecimal()
			);

			FeeCalculationDto = calculated != null ? calculated : new();
		}

		/**/

		[RelayCommand]
		private async Task PayAsync()
		{
			ResultHelper<PayPalAuthResponseDto> authenticated = await payPalService.AuthenticateAsync();

			if (authenticated.Ok)
			{
				ResultHelper<PayPalOrderResponseDto> result = await payPalService.CreateOrderAsync(
					new PayPalOrderRequestDto
					{
					}
				);

				if (result.Ok)
				{
					//result.Data.
				}
			}
		}
	}
}