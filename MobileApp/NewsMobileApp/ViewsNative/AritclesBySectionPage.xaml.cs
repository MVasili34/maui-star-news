using NewsMobileApp.ViewModels;
using NewsMobileApp.TempServices;

namespace NewsMobileApp.ViewsNative;

public partial class ArticlesBySectionPage : ContentPage
{
    private readonly ArticlesBySectionViewModel _viewmodels;
    private readonly int _sectionId;

    public ArticlesBySectionPage(ArticlesBySectionViewModel viewmodels, int sectionId)
    {
        InitializeComponent();
        _sectionId = sectionId;
        //AdmindButton.IsVisible = false;
        _viewmodels = viewmodels;
        _viewmodels.AddArticles(_sectionId);
        BindingContext = _viewmodels;
    }

    private async void SectionArticle_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.First() is not ArticlePreviewViewModel article) return;

        await Navigation.PushAsync(new ArticlePage(article.ArticleId));
    }

    private async void SearchText_Completed(object sender, EventArgs e)
    {
        _viewmodels.SearchArticle(_sectionId, SearchText.Text);
    }

    private async void RefreshView_Refreshing(object sender, EventArgs e)
    {
        Refresher.IsRefreshing = true;
        _viewmodels.AddArticles(_sectionId, true);
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
            _viewmodels.AddArticles(_sectionId);
        }
    }

    private async void AdmindButton_Clicked(object sender, EventArgs e) =>
        await Navigation.PushAsync(new ArticleDetailPage(new NewsService()));
}