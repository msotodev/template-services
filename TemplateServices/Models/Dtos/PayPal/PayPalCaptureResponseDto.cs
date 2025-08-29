using System.Text.Json.Serialization;

namespace TemplateServices.Domain.Models.Dtos.PayPal
{
	public class PayPalCaptureResponseDto
	{
		[JsonPropertyName("id")]
		public string Id { get; set; } = string.Empty;

		[JsonPropertyName("status")]
		public string Status { get; set; } = string.Empty;

		[JsonPropertyName("purchase_units")]
		public List<PurchaseUnitCaptureDto> PurchaseUnits { get; set; } = [];
	}
}