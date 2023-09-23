using NewsMobileApp.TempServices;
using NewsMobileApp.ViewModels;
using NewsMobileApp.Models;

namespace NewsMobileApp.ViewsNative;

public partial class ThrendsPage : ContentPage
{
	public ThrendsPage(INewsService newsService)
	{
		InitializeComponent();
        BindingContext = new ThrendsViewModel(newsService);
    }

    private async void HotArticle_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.First() is not Article article) return;

        await Navigation.PushAsync(new ArticlePage(article.Id));
    }
}