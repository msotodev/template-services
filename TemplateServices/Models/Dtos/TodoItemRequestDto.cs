namespace TemplateServices.Core.Models.Dtos
{
	public class TodoItemRequestDto
	{
		public int Id { get; set; }

		public string Title { get; set; } = string.Empty;

		public string Description { get; set; } = string.Empty;

		public int CategoryId { get; set; }
	}
}