using System.Text.Json.Serialization;

namespace TemplateServices.Core.Models.Dtos.PayPal
{
	public class PaymentCaptureDto
	{
		[JsonPropertyName("captures")]
		public List<CaptureDetailDto> Captures { get; set; } = [];
	}
}