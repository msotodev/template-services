using System.Text.Json.Serialization;

namespace TemplateServices.Domain.Models.Dtos.PayPal
{
	public class RefundCaptureRequestDto
	{
		[JsonPropertyName("amount")]
		public AmountDto? Amount { get; set; }
	}
}