using System.Text.Json.Serialization;

namespace TemplateServices.Domain.Models.Dtos.PayPal
{
	public class PayPalOrderRequestDto
	{
		[JsonPropertyName("intent")]
		public string Intent { get; set; } = "CAPTURE";

		[JsonPropertyName("purchase_units")]
		public List<PurchaseUnit> PurchaseUnits { get; set; } = [];

		[JsonPropertyName("application_context")]
		public ApplicationContextDto? ApplicationContext { get; set; }
	}
}