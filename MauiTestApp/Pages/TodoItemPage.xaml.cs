using TemplateServices.Domain.ViewModels;

namespace MauiTestApp.Pages
{
	public partial class TodoItemPage : BasePage<TodoItemPageViewModel>
	{
		public TodoItemPage(
			TodoItemPageViewModel viewModel
		) : base(viewModel)
		{
			InitializeComponent();
		}
	}
}