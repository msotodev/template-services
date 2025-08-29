using System.Text.Json.Serialization;

namespace TemplateServices.Domain.Models.Dtos.PayPal
{
	public class PayPalOrderResponseDto
	{
		[JsonPropertyName("id")]
		public string Id { get; set; } = string.Empty;

		[JsonPropertyName("status")]
		public string Status { get; set; } = string.Empty;

		[JsonPropertyName("links")]
		public List<PayPalLinkDto> Links { get; set; } = [];

		[JsonPropertyName("purchase_units")]
		public List<PurchaseUnit> PurchaseUnits { get; set; } = [];
	}
}