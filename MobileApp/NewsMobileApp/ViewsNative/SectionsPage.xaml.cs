using NewsMobileApp.TempServices;
using NewsMobileApp.ViewModels;

namespace NewsMobileApp.ViewsNative;

public partial class SectionsPage : ContentPage
{
	public SectionsPage(INewsService newsService)
	{
		InitializeComponent();
        BindingContext = new SectionsViewModel(newsService);
    }

    private async void Section_Tapped(object sender, TappedEventArgs e)
    {
        await DisplayAlert("Test", e.Parameter.ToString(), "OK");
    }
}