using System.Text.Json.Serialization;

namespace TemplateServices.Domain.Models.Dtos.PayPal
{
	public class PayPalLinkDto
	{
		[JsonPropertyName("href")]
		public string Href { get; set; } = string.Empty;

		[JsonPropertyName("rel")]
		public string Rel { get; set; } = string.Empty;

		[JsonPropertyName("method")]
		public string Method { get; set; } = string.Empty;
	}
}