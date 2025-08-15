using System.Text.Json.Serialization;

namespace TemplateServices.Core.Models.Dtos.PayPal
{
	public class PurchaseUnit
	{
		[JsonPropertyName("reference_id")]
		public string ReferenceId { get; set; } = string.Empty;

		[JsonPropertyName("amount")]
		public AmountDto? Amount { get; set; }

		[JsonPropertyName("description")]
		public string Description { get; set; } = string.Empty;
	}
}