using NewsMobileApp.TempServices;
using NewsMobileApp.ViewModels;
using NewsMobileApp.Models;

namespace NewsMobileApp.ViewsNative;

public partial class ThrendsPage : ContentPage
{
    private readonly INewsService _newsService;

	public ThrendsPage(INewsService newsService)
	{
		InitializeComponent();
        _newsService = newsService;
        BindingContext = new ThrendsViewModel(_newsService);
    }

    private async void HotArticle_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.First() is not Article article) return;

        await Navigation.PushAsync(new ArticlePage(article.Id));
    }

    private async void SearchText_Completed(object sender, EventArgs e)
    {
        await DisplayAlert("Search", $"{SearchText.Text}", "OK");
        
    }
}