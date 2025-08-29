using TemplateServices.Domain.ViewModels;

namespace MauiTestApp.Pages
{
	public partial class TodoItemFormPage : BasePage<TodoItemFormPageViewModel>
	{
		public TodoItemFormPage(
			TodoItemFormPageViewModel viewModel
		) : base (viewModel)
		{
			InitializeComponent();
		}
	}
}