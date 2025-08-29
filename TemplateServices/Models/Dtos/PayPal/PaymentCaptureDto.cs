using System.Text.Json.Serialization;

namespace TemplateServices.Domain.Models.Dtos.PayPal
{
	public class PaymentCaptureDto
	{
		[JsonPropertyName("captures")]
		public List<CaptureDetailDto> Captures { get; set; } = [];
	}
}