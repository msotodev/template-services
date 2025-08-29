namespace TemplateServices.Domain.Models
{
	public class DeviceModel
	{
		public Guid Id { get; set; } = Guid.Empty;

		public string Name { get; set; } = string.Empty;

		public string MacAddress { get; set; } = string.Empty;
	}
}