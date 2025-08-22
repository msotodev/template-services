using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EssentialLayers.Helpers.Extension;
using System.Collections.ObjectModel;
using TemplateServices.Core.Models.Dtos.Icon;
using TemplateServices.Core.Services.App;

namespace TemplateServices.Core.ViewModels
{
	public partial class IconsPageViewModel(
		IIconService iconService
	) : ObservableObject
	{
		[ObservableProperty]
		private string search = string.Empty;

		[ObservableProperty]
		private ObservableCollection<IconResultDto> colors = [];

		/**/

		[RelayCommand]
		private void Appearing()
		{
			Colors = iconService.All.ToObservableCollection();
		}
		
		[RelayCommand]
		private void Clear()
		{
			Search = string.Empty;
		}
	}
}