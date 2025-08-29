namespace TemplateServices.Domain.Models.Dtos.PayPal
{
	public class PayPalFeeCalculationDto
	{
		public decimal OriginalAmount { get; set; }

		public decimal PercentageFee { get; set; }

		public decimal FixedFee { get; set; }

		public decimal TotalFee { get; set; }

		public decimal TotalWithFees { get; set; }

		public string CurrencyCode { get; set; } = string.Empty;

		public PayPalFeeBreakdownDto GetBreakdown()
		{
			return new PayPalFeeBreakdownDto
			{
				Subtotal = OriginalAmount,
				PayPalFeePercentage = PercentageFee,
				PayPalFeeFixed = FixedFee,
				PayPalFeeTotal = TotalFee,
				Total = TotalWithFees,
				CurrencyCode = CurrencyCode
			};
		}
	}
}