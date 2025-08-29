namespace TemplateServices.Domain.Models.Dtos
{
	public class TodoItemResultDto
	{
		public int Id { get; set; }

		public string Title { get; set; } = string.Empty;

		public string Description { get; set; } = string.Empty;

		public int CategoryId { get; set; }

		public string CategoryName { get; set; } = string.Empty;

		public DateTime Created { get; set; }
	}
}