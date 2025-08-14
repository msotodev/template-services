using SQLite;

namespace MauiTestApp.Models.Entities
{
	public class Entity
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }

		public DateTime Created { get; set; }

		public DateTime Updated { get; set; }
	}
}