using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TemplateServices.Core.Helpers.Constants;
using TemplateServices.Core.Models;
using TemplateServices.Core.Services.App;

namespace TemplateServices.Core.ViewModels
{
	public partial class PreferencePageViewModel(
		IFixedDialogService fixedDialogService,
		ILocalizationService localizationService,
		IPreferencesService preferencesService
	) : ObservableObject
	{
		private const string TEXT_KEY = "texts-key";

		[ObservableProperty]
		private string text = "Value 1";

		[ObservableProperty]
		private bool isEnabledDeleteButton = true;

		[ObservableProperty]
		private ObservableCollection<PreferenceModel> preferences = [];

		/**/

		[RelayCommand]
		private void Appearing()
		{
			Preferences = preferencesService.Get<ObservableCollection<PreferenceModel>>(
				TEXT_KEY
			);

			Preferences ??= [];
		}

		[RelayCommand]
		private void Add()
		{
			Preferences.Add(
				new PreferenceModel
				{
					Id = Guid.NewGuid(),
					Key = TEXT_KEY,
					Value = Text
				}
			);

			preferencesService.Set(TEXT_KEY, Preferences);
		}

		[RelayCommand]
		private void Clear()
		{
			Preferences.Clear();

			preferencesService.Clear();
		}

		[RelayCommand]
		private async Task RemoveAsync(PreferenceModel model)
		{
			if (model == null) return;

			string message = localizationService.GetString(
				LocalizationConstant.DELETE_QUESTION_CONFIRMATION
			);
			bool confirmed = await fixedDialogService.ConfirmAsync(message);

			if (confirmed)
			{
				Preferences.Remove(model);

				preferencesService.Remove(model.Key);
			}
		}
	}
}