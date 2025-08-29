namespace TemplateServices.Domain.Models.Dtos
{
	public class TodoItemCategoryRequestDto
	{
		public int Id { get; set; }

		public string Title { get; set; } = string.Empty;
	}
}