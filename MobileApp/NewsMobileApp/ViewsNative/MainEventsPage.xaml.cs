using NewsMobileApp.ViewModels;

namespace NewsMobileApp.ViewsNative;

public partial class MainEventsPage : ContentPage
{
	private readonly MainPageViewModel _viewModel;
    private bool loaded = false;

	public MainEventsPage(MainPageViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
        BindingContext = _viewModel;
        //LatestNews.BindingContext = _viewModel.NewestArticles;
        //RecommendedNews.BindingContext = _viewModel.HystoricalArticles;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        if (!loaded)
        {
            await _viewModel.GetNewestArticles();
            await _viewModel.GetHystoricalArticles();
            await _viewModel.GetPopularTags();
            loaded = true;
        }
    }

    private void Tag_Clicked(object sender, EventArgs e)
    {

    }

    private async void LatestNews_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.First() is not ArticlePreviewViewModel article) return;

        await Navigation.PushAsync(new ArticlePage(article.ArticleId));
    }
}