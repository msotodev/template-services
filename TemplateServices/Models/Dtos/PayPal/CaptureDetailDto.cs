using System.Text.Json.Serialization;

namespace TemplateServices.Core.Models.Dtos.PayPal
{
	public class CaptureDetailDto
	{
		[JsonPropertyName("id")]
		public string Id { get; set; } = string.Empty;

		[JsonPropertyName("status")]
		public string Status { get; set; } = string.Empty;

		[JsonPropertyName("amount")]
		public AmountDto? Amount { get; set; }
	}
}