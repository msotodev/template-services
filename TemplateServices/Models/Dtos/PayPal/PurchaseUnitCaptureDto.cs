using System.Text.Json.Serialization;

namespace TemplateServices.Domain.Models.Dtos.PayPal
{
	public class PurchaseUnitCaptureDto
	{
		[JsonPropertyName("payments")]
		public PaymentCaptureDto? Payments { get; set; }
	}
}