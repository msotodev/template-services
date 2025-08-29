﻿using System.Text.Json.Serialization;

namespace TemplateServices.Domain.Models.Dtos.PayPal
{
	public class AmountDto
	{
		[JsonPropertyName("currency_code")]
		public string CurrencyCode { get; set; } = string.Empty;

		[JsonPropertyName("value")]
		public string Value { get; set; } = string.Empty;
	}
}