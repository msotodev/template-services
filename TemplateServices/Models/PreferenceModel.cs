namespace TemplateServices.Core.Models
{
	public class PreferenceModel
	{
		public Guid Id { get; set; } = Guid.Empty;

		public string Key { get; set; } = string.Empty;

		public string Value { get; set; } = string.Empty;
	}
}