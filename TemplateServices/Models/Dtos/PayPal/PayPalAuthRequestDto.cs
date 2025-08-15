using System.Text.Json.Serialization;

namespace TemplateServices.Core.Models.Dtos.PayPal
{
	public class PayPalAuthRequestDto
	{
		[JsonPropertyName("grant_type")]
		public string GrantType { get; set; } = "client_credentials";
	}
}