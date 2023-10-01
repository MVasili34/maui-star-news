using NewsMobileApp.ViewModels;
using NewsMobileApp.Models;
using System.Threading.Tasks;

namespace NewsMobileApp.ViewsNative;

public partial class ThrendsPage : ContentPage
{
    private readonly ThrendsViewModel _viewmodels;

    public ThrendsPage(ThrendsViewModel viewmodels)
	{
		InitializeComponent();
        _viewmodels = viewmodels;
        _viewmodels.AddArticles();
        BindingContext = _viewmodels;
    }

    private async void HotArticle_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.First() is not Article article) return;

        await Navigation.PushAsync(new ArticlePage(article.Id));
    }

    private async void SearchText_Completed(object sender, EventArgs e)
    {
        _viewmodels.SearchArticle(SearchText.Text);
    }

    private async void RefreshView_Refreshing(object sender, EventArgs e)
    {
        Refresher.IsRefreshing = true;
        _viewmodels.AddArticles(true);
        await Task.Delay(1500);
        Refresher.IsRefreshing = false;
    }

    private void OnScrollViewScrolled(object sender, ScrolledEventArgs e)
    {
        if (sender is not ScrollView scrollView) return;

        double scrollViewHeight = scrollView.Height;
        double contentHeight = scrollView.ContentSize.Height;
        double currentScrollPosition = scrollView.ScrollY;

        if (currentScrollPosition + scrollViewHeight >= contentHeight)
        {
            _viewmodels.AddArticles();
        }
    }
}