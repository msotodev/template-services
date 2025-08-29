namespace TemplateServices.Domain.Models.Dtos.PayPal
{
	public class PayPalSettings
	{
		public string ClientId { get; set; } = string.Empty;

		public string ClientSecret { get; set; } = string.Empty;

		public string BaseUrl { get; set; } = string.Empty;

		public bool IsSandbox { get; set; }

		public string ReturnUrl { get; set; } = string.Empty;

		public string CancelUrl { get; set; } = string.Empty;

		public string BrandName { get; set; } = string.Empty;
	}
}