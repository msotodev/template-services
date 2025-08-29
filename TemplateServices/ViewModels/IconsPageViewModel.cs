using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EssentialLayers.Helpers.Extension;
using System.Collections.ObjectModel;
using TemplateServices.Domain.Models.Dtos.Icon;
using TemplateServices.Domain.Services.App;

namespace TemplateServices.Domain.ViewModels
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