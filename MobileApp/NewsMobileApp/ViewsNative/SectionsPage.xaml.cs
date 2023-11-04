using NewsMobileApp.TempServices;
using NewsMobileApp.ViewModels;
using NewsMobileApp.ViewsNative;

namespace NewsMobileApp.ViewsNative;

public partial class SectionsPage : ContentPage
{

	public SectionsPage()
	{
        InitializeComponent();
    }

    private async void Section_Tapped(object sender, TappedEventArgs e)
    {
        if(e.Parameter is not int)
        {
            await DisplayAlert("Ошибка!", "Произошла неизвестная ошибка.", "OK");
            return;
        }
        await Navigation.PushAsync(new ArticlesBySectionPage(
            (int)e.Parameter));
    }
}