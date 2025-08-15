using System.Text.Json.Serialization;

namespace TemplateServices.Core.Models.Dtos.PayPal
{
	public class ApplicationContextDto
	{
		[JsonPropertyName("return_url")]
		public string ReturnUrl { get; set; } = string.Empty;

		[JsonPropertyName("cancel_url")]
		public string CancelUrl { get; set; } = string.Empty;

		[JsonPropertyName("brand_name")]
		public string BrandName { get; set; } = string.Empty;

		[JsonPropertyName("user_action")]
		public string UserAction { get; set; } = "PAY_NOW";
	}
}