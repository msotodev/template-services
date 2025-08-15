namespace TemplateServices.Core.Models.Dtos.PayPal
{
	public class PayPalFeeBreakdownDto
	{
		public decimal Subtotal { get; set; }

		public decimal PayPalFeePercentage { get; set; }

		public decimal PayPalFeeFixed { get; set; }

		public decimal PayPalFeeTotal { get; set; }

		public decimal Total { get; set; }

		public string CurrencyCode { get; set; } = string.Empty;
	}
}