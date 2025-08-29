using System.Text.Json.Serialization;

namespace TemplateServices.Domain.Models.Dtos.PayPal
{
	public class PayPalAuthResponseDto
	{
		[JsonPropertyName("access_token")]
		public string AccessToken { get; set; } = string.Empty;

		[JsonPropertyName("token_type")]
		public string TokenType { get; set; } = string.Empty;

		[JsonPropertyName("expires_in")]
		public int ExpiresIn { get; set; }

		[JsonPropertyName("scope")]
		public string Scope { get; set; } = string.Empty;
	}
}