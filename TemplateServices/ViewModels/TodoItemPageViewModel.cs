using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EssentialLayers.Helpers.Extension;
using System.Collections.ObjectModel;
using TemplateServices.Core.Helpers.Constants;
using TemplateServices.Core.Models.Dtos;
using TemplateServices.Core.Services.Api.Local;
using TemplateServices.Core.Services.App;

namespace TemplateServices.Core.ViewModels
{
	public partial class TodoItemPageViewModel(
		IFixedDialogService fixedDialogService,
		ILocalizationService localizationService,
		INavigationService navigationService,
		ITodoItemService todoItemsService
	) : ObservableObject
	{
		[ObservableProperty]
		private ObservableCollection<TodoItemResultDto> todoItems = [];

		[ObservableProperty]
		private bool isRefreshing = false;

		/**/

		[RelayCommand]
		private void Appearing() => Refresh();

		[RelayCommand]
		private Task Add() => navigationService.NavigateToAsync(
			RoutesConstant.TODO_ITEM_FORM
		);

		[RelayCommand]
		private async Task EditAsync(TodoItemResultDto dto)
		{
			Dictionary<string, object> parameters = new()
			{
				{ "todoItemId", dto.Id }
			};

			await navigationService.NavigateToAsync(
				RoutesConstant.TODO_ITEM_FORM, parameters
			);
		}

		[RelayCommand]
		private async Task RemoveAsync(TodoItemResultDto dto)
		{
			if (dto == null) return;

			string message = localizationService.GetString(
				LocalizationConstant.DELETE_QUESTION_CONFIRMATION
			);
			bool confirmed = await fixedDialogService.ConfirmAsync(message);

			if (confirmed)
			{
				TodoItems.Remove(dto);

				todoItemsService.Delete(dto);
			}
		}

		[RelayCommand]
		public void Refresh()
		{
			IsRefreshing = true;

			TodoItems = todoItemsService.All.ToObservableCollection();

			IsRefreshing = false;
		}
	}
}