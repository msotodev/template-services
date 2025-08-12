using CommunityToolkit.Mvvm.ComponentModel;

namespace MauiTestApp.Pages
{
	public abstract partial class BasePage<TViewModel> : ContentPage where TViewModel : ObservableObject
	{
		protected TViewModel ViewModel => (TViewModel)BindingContext;
		
		protected BasePage(TViewModel viewModel)
		{
			BindingContext = viewModel;
		}
	}
}