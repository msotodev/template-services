using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EssentialLayers.Helpers.Extension;
using EssentialLayers.Helpers.Result;
using System.Collections.ObjectModel;
using TemplateServices.Core.Helpers.Constants;
using TemplateServices.Core.Models.Dtos;
using TemplateServices.Core.Services.Api.Local;
using TemplateServices.Core.Services.App;
using static TemplateServices.Core.Helpers.Constants.LocalizationConstant;

namespace TemplateServices.Core.ViewModels
{
	[QueryProperty(nameof(TodoItemId), "todoItemId")]
	public partial class TodoItemFormPageViewModel(
		IFixedDialogService fixedDialogService,
		ILocalizationService localizationService,
		INavigationService navigationService,
		ITodoItemService todoItemsService,
		ITodoItemCategoryService todoItemCategoryService
	) : ObservableObject
	{
		[ObservableProperty]
		private int todoItemId;

		[ObservableProperty]
		private string title = string.Empty;

		[ObservableProperty]
		private string description = string.Empty;

		[ObservableProperty]
		private int categoryId;

		[ObservableProperty]
		private ObservableCollection<TodoItemCategoryResultDto> categories = [];

		[ObservableProperty]
		private TodoItemCategoryResultDto? selectedCategory;

		/**/

		partial void OnTodoItemIdChanged(int value)
		{
			ResultHelper<TodoItemResultDto> get = todoItemsService.Get(value);

			if (get.Ok)
			{
				Title = get.Data.Title;
				CategoryId = get.Data.CategoryId;
				Description = get.Data.Description;
			}
		}

		/**/

		[RelayCommand]
		private void Appearing()
		{
			Categories = todoItemCategoryService.All.ToObservableCollection();
			
			if (CategoryId > 0)
			{
				SelectedCategory = Categories.FirstOrDefault(
					c => c.Id == CategoryId
				);
			}
		}

		[RelayCommand]
		private Task AddCatalog() => navigationService.NavigateToAsync(
			RoutesConstant.TODO_ITEM_CATEGORY_FORM
		);

		[RelayCommand]
		private async Task SaveAsync()
		{
			if (SelectedCategory == null)
			{
				string message = localizationService.GetString(
					REQUIRED_DYNAMIC_MESSAGE, CATEGORY
				);

				await fixedDialogService.WarningAsync(message);
				return;
			}

			Response response = todoItemsService.New(
				new TodoItemRequestDto
				{
					Description = Description,
					Title = Title,
					CategoryId = SelectedCategory.Id
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