namespace MauiTestApp.Models.Entities
{
    public class TodoItem : Entity
    {
		public string Title { get; set; } = string.Empty;

		public string Description { get; set; } = string.Empty;

		public int CategoryId { get; set; }
	}
}