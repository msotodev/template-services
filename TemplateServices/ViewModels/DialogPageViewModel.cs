using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TemplateServices.Domain.Services.App;

namespace TemplateServices.Domain.ViewModels
{
	public partial class DialogPageViewModel(
		IFixedDialogService fixedDialogService,
		ITransitiveDialogsService transitiveDialogsService
	) : ObservableObject
	{
		[RelayCommand]
		private Task FixedInfoAsync() => fixedDialogService.InfoAsync(
			"Information message"
		);

		[RelayCommand]
		private Task FixedErrorAsync() => fixedDialogService.ErrorAsync(
			"Error message"
		);

		[RelayCommand]
		private Task FixedWarningAsync() => fixedDialogService.WarningAsync(
			"Warning message"
		);

		[RelayCommand]
		private Task<bool> FixedConfirmAsync() => fixedDialogService.ConfirmAsync(
			"Are you sure continue"
		);

		[RelayCommand]
		private Task<string> FixedPromptAsync() => fixedDialogService.PromptAsync(
			"Input a value between 1 to 10", "Status validation"
		);

		/**/

		[RelayCommand]
		private Task TransitiveInfoAsync() => transitiveDialogsService.InfoAsync(
			"Information message"
		);

		[RelayCommand]
		private Task TransitiveErrorAsync() => transitiveDialogsService.ErrorAsync(
			"Error message"
		);

		[RelayCommand]
		private Task TransitiveWarningAsync() => transitiveDialogsService.WarningAsync(
			"Warning message"
		);
	}
}