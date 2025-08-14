using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EssentialLayers.Helpers.Extension;
using EssentialLayers.Helpers.Result;
using TemplateServices.Core.Models.Dtos;
using TemplateServices.Core.Services.Api.Local;
using TemplateServices.Core.Services.App;

namespace TemplateServices.Core.ViewModels
{
	public partial class TodoItemCategoryFormPageViewModel(
		IFixedDialogService fixedDialogService,
		INavigationService navigationService,
		ITodoItemCategoryService todoItemCategoryService
	) : ObservableObject
	{
		[ObservableProperty]
		private string title = string.Empty;

		/**/

		[RelayCommand]
		private async Task SaveAsync()
		{
			Response response = todoItemCategoryService.New(
				new TodoItemCategoryRequestDto
				{
					Title = Title
				}
			);

			if (response.Ok.False())
			{
				await fixedDialogService.ErrorAsync(response.Message);
				return;
			}

			await navigationService.BackAsync();
		}
	}
}