using EssentialLayers.Helpers.Result;
using TemplateServices.Core.Models.Dtos.PayPal;

namespace TemplateServices.Core.Services.App
{
	public interface IPayPalService
	{
		Task<ResultHelper<PayPalAuthResponseDto>> AuthenticateAsync();

		Task<ResultHelper<PayPalOrderResponseDto>> CreateOrderAsync(
			PayPalOrderRequestDto request
		);
		
		Task<PayPalOrderResponseDto?> GetOrderAsync(
			string orderId
		);
		
		Task<ResultHelper<PayPalCaptureResponseDto>> CaptureOrderAsync(
			string orderId
		);
		
		Task<Response> RefundCaptureAsync(
			string captureId, RefundCaptureRequestDto request
		);
		
		PayPalFeeCalculationDto CalculatePayPalFees(
			decimal originalAmount, bool isInternational = false, string currencyCode = "USD"
		);
		
		PayPalFeeCalculationDto CalculateAmountToReceive(
			decimal desiredNetAmount, bool isInternational = false, string currencyCode = "USD"
		);
	}
}