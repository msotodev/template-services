using TemplateServices.Domain.ViewModels;

namespace MauiTestApp.Pages
{
	public partial class TodoItemCategoryFormPage : BasePage<TodoItemCategoryFormPageViewModel>
	{
		public TodoItemCategoryFormPage(
			TodoItemCategoryFormPageViewModel viewModel
		) : base(viewModel)
		{
			InitializeComponent();
		}
	}
}