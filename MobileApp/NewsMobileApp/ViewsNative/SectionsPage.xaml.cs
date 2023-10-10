using NewsMobileApp.TempServices;
using NewsMobileApp.ViewModels;
using NewsMobileApp.ViewsNative;

namespace NewsMobileApp.ViewsNative;

public partial class SectionsPage : ContentPage
{
    private readonly INewsService _newsService;

	public SectionsPage(INewsService newsService)
	{
		InitializeComponent();
        _newsService = newsService;
        BindingContext = new SectionsViewModel(newsService);
    }

    private async void Section_Tapped(object sender, TappedEventArgs e)
    {
        if(e.Parameter is not int)
        {
            await DisplayAlert("Ошибка!", "Произошла неизвестная ошибка.", "OK");
            return;
        }
        await Navigation.PushAsync(new ArticlesBySectionPage(new ArticlesBySectionViewModel(_newsService),
            (int)e.Parameter));
    }
}