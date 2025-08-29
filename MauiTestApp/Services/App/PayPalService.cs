using EssentialLayers.Helpers.Extension;
using EssentialLayers.Helpers.Result;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;
using TemplateServices.Domain.Models.Dtos.PayPal;
using TemplateServices.Domain.Services.App;
using Uri = System.Uri;

namespace MauiTestApp.Services.App
{
	public class PayPalService : IPayPalService
	{
		private readonly IErrorHandlerService _errorHandlerService;

		/**/

		private readonly HttpClient _httpClient;

		private readonly PayPalSettings _settings;

		private string? _accessToken;

		private DateTime? _tokenExpiryTime;

		private readonly JsonSerializerOptions _jsonSerializerOptions;

		/* PayPal fee structure (update these based on your region) */

		private const decimal DomesticFeePercentage = 0.0229m; // 2.29%

		private const decimal DomesticFeeFixed = 0.09m; // $0.09

		private const decimal InternationalFeePercentage = 0.0349m; // 3.49%

		private const decimal InternationalFeeFixed = 0.49m; // $0.49

		/**/

		public PayPalService(
			HttpClient httpClient,
			IErrorHandlerService errorHandlerService,
			IOptions<PayPalSettings> settings
		)
		{
			_errorHandlerService = errorHandlerService;
			_httpClient = httpClient;
			_settings = settings.Value;

			_httpClient.BaseAddress = new Uri(_settings.BaseUrl);

			_jsonSerializerOptions = new JsonSerializerOptions
			{
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase
			};
		}

		public async Task<ResultHelper<PayPalAuthResponseDto>> AuthenticateAsync()
		{
			try
			{
				if (_accessToken != null && _accessToken.NotEmpty() && DateTime.UtcNow < _tokenExpiryTime) return ResultHelper<PayPalAuthResponseDto>.Success(
					new PayPalAuthResponseDto
					{
						AccessToken = _accessToken
					}
				);

				string credentials = $"{_settings.ClientId}:{_settings.ClientSecret}".ToBase64();

				AddHeaders(false, credentials);

				Dictionary<string, string> request = new()
				{
					{ "grant_type", "client_credentials" }
				};

				ResultHelper<PayPalAuthResponseDto> result = await PostAsync<PayPalAuthResponseDto, Dictionary<string, string>>(
					request, "/v1/oauth2/token"
				);

				if (result.Ok.False()) return ResultHelper<PayPalAuthResponseDto>.Fail(
					result.Message
				);

				_accessToken = result.Data.AccessToken;

				_tokenExpiryTime = DateTime.UtcNow.AddSeconds(
					result.Data.ExpiresIn - 60
				);

				return result;
			}
			catch (Exception e)
			{
				return _errorHandlerService.HandleError<PayPalAuthResponseDto>(e);
			}
		}

		public PayPalFeeCalculationDto CalculateAmountToReceive(
			decimal desiredNetAmount,
			bool isInternational = false,
			string currencyCode = "USD"
		)
		{
			decimal feePercentage = isInternational ? InternationalFeePercentage : DomesticFeePercentage;
			decimal feeFixed = isInternational ? InternationalFeeFixed : DomesticFeeFixed;

			if (currencyCode != "USD")
			{
				feeFixed = ConvertFixedFeeTourrency(feeFixed, currencyCode);
			}

			// Fórmula: (Monto deseado + tarifa fija) / (1 - porcentaje de comisión)
			decimal grossAmount = (desiredNetAmount + feeFixed) / (1 - feePercentage);
			decimal totalFee = grossAmount - desiredNetAmount;
			decimal percentageFee = totalFee - feeFixed;

			return new PayPalFeeCalculationDto
			{
				OriginalAmount = desiredNetAmount,
				PercentageFee = percentageFee,
				FixedFee = feeFixed,
				TotalFee = totalFee,
				TotalWithFees = grossAmount,
				CurrencyCode = currencyCode
			};
		}

		public PayPalFeeCalculationDto CalculatePayPalFees(decimal originalAmount, bool isInternational = false, string currencyCode = "USD")
		{
			decimal feePercentage = isInternational ? InternationalFeePercentage : DomesticFeePercentage;
			decimal feeFixed = isInternational ? InternationalFeeFixed : DomesticFeeFixed;

			// Ajustar tarifa fija según la moneda
			if (currencyCode != "USD")
			{
				feeFixed = ConvertFixedFeeTourrency(feeFixed, currencyCode);
			}

			decimal percentageFee = originalAmount * feePercentage;
			decimal totalFee = percentageFee + feeFixed;
			decimal totalWithFees = originalAmount + totalFee;

			return new PayPalFeeCalculationDto
			{
				OriginalAmount = originalAmount,
				PercentageFee = percentageFee,
				FixedFee = feeFixed,
				TotalFee = totalFee,
				TotalWithFees = totalWithFees,
				CurrencyCode = currencyCode
			};
		}

		public async Task<ResultHelper<PayPalCaptureResponseDto>> CaptureOrderAsync(
			string orderId
		)
		{
			try
			{
				await AuthenticateAsync();

				AddHeaders(true);

				ResultHelper<PayPalCaptureResponseDto> result = await PostAsync<PayPalCaptureResponseDto, object>(
					new { }, $"/v2/checkout/orders/{orderId}/capture"
				);

				return result;
			}
			catch (Exception e)
			{
				return _errorHandlerService.HandleError<PayPalCaptureResponseDto>(e);
			}
		}

		public async Task<ResultHelper<PayPalOrderResponseDto>> CreateOrderAsync(
			PayPalOrderRequestDto request
		)
		{
			try
			{
				await AuthenticateAsync();

				AddHeaders(true);

				ResultHelper<PayPalOrderResponseDto> result = await PostAsync<PayPalOrderResponseDto, PayPalOrderRequestDto>(
					request, "/v2/checkout/orders"
				);

				return result;
			}
			catch (Exception e)
			{
				return _errorHandlerService.HandleError<PayPalOrderResponseDto>(e);
			}
		}

		public async Task<PayPalOrderResponseDto?> GetOrderAsync(string orderId)
		{
			try
			{
				await AuthenticateAsync();

				AddHeaders(false);

				ResultHelper<PayPalOrderResponseDto> result = await GetAsync<PayPalOrderResponseDto>(
					$"/v2/checkout/orders/{orderId}"
				);

				return result.Ok ? result.Data : null;
			}
			catch (Exception)
			{
				return null;
			}
		}

		public async Task<Response> RefundCaptureAsync(
			string captureId,
			RefundCaptureRequestDto request
		)
		{
			try
			{
				await AuthenticateAsync();

				AddHeaders(true);

				ResultHelper<object> result = await PostAsync<object, RefundCaptureRequestDto>(
					request, $"/v2/payments/captures/{captureId}/refund"
				);

				if (result.Ok.False()) return Response.Fail(result.Message);

				return Response.Success();
			}
			catch (Exception e)
			{
				return Response.Fail(e.Message);
			}
		}

		/**/

		private void AddHeaders(
			bool addPrefer = false, string? credentials = null
		)
		{
			_httpClient.DefaultRequestHeaders.Clear();

			if (credentials != null)
			{
				_httpClient.DefaultRequestHeaders.Add(
					"Authorization", $"Basic {credentials}"
				);
			}
			else
			{
				_httpClient.DefaultRequestHeaders.Add(
					"Authorization", $"Bearer {_accessToken}"
				);
			}

			_httpClient.DefaultRequestHeaders.Add(
				"Accept", "application/json"
			);

			if (addPrefer)
			{
				_httpClient.DefaultRequestHeaders.Add(
					"Prefer", "return=representation"
				);
			}
		}

		private static decimal ConvertFixedFeeTourrency(
			decimal usdFee, string currencyCode
		)
		{
			return currencyCode switch
			{
				"EUR" => usdFee * 0.85m,
				"MXN" => usdFee * 18.5m,
				"GBP" => usdFee * 0.75m,
				_ => usdFee
			};
		}

		private async Task<ResultHelper<TResult>> PostAsync<TResult, TRequest>(
			TRequest request, string url, bool isUrlEncoded = false
		)
		{
			string jsonContent = JsonSerializer.Serialize(
				request, _jsonSerializerOptions
			);

			MultipartFormDataContent multipartContent = [];

			if (isUrlEncoded)
			{
				IDictionary<string, string>? items = request as Dictionary<string, string>;

				if (items != null)
				{
					IEnumerable<KeyValuePair<string, string>> keys = items.Select(x => x);

					multipartContent.Add(new FormUrlEncodedContent(keys));
				}
			}
			else
			{
				multipartContent.Add(
					new StringContent(
						jsonContent, Encoding.UTF8, "application/json"
					)
				);
			}

			HttpResponseMessage response = await _httpClient.PostAsync(
				url, multipartContent
			);

			if (response.IsSuccessStatusCode)
			{
				string jsonResponse = await response.Content.ReadAsStringAsync();

				TResult? deserialized = jsonResponse.Deserialize<TResult>();

				return ResultHelper<TResult>.Success(deserialized);
			}
			else
			{
				string error = await response.Content.ReadAsStringAsync();

				return ResultHelper<TResult>.Fail(error);
			}
		}

		private async Task<ResultHelper<TResult>> GetAsync<TResult>(
			string url
		)
		{
			HttpResponseMessage response = await _httpClient.GetAsync(
				url
			);

			if (response.IsSuccessStatusCode)
			{
				string jsonResponse = await response.Content.ReadAsStringAsync();

				TResult? deserialized = jsonResponse.Deserialize<TResult>();

				return ResultHelper<TResult>.Success(deserialized);
			}
			else
			{
				string error = await response.Content.ReadAsStringAsync();

				return ResultHelper<TResult>.Fail(error);
			}
		}
	}
}