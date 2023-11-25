using NewsMobileApp.ViewModels;
using NewsMobileApp.Models;
using NewsMobileApp.TempServices;

namespace NewsMobileApp.ViewsNative;

public partial class TrendsPage : ContentPage
{
    private ThrendsViewModel viewModel => BindingContext as ThrendsViewModel;
    private bool _loaded = false;
    private bool _scrolled = true;

    public TrendsPage(ThrendsViewModel viewmodel)
	{
		InitializeComponent();
        LoadProcessSimulation();
        BindingContext = viewmodel;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        if (Preferences.Get("roleId", 1) != 1)
        {
            AdmindButton.IsEnabled = true;
            AdmindButton.IsVisible = true;
        }
        if (this.AnimationIsRunning("TransitionAnimation"))
            return;

        var parentAnimation = AnimationCreator.SetAnimations(LoadBlock1, LoadBlock2, LoadBlock3, LoadBlock4);

        parentAnimation.Commit(this, "TransitionAnimation", length: 2000, repeat: () => true);
        try
        {
            await viewModel.AddArticles(true);
            _loaded = true;
            LoadProcessSimulation();
        }
        catch (Exception ex) 
        {
            await DisplayAlert("Îøèáêà!", ex.Message, "OK");
        }
    }

    private async void HotArticle_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection[0] is not Article article) return;

        await Navigation.PushAsync(new ArticlePage(article));
    }

    private async void SearchText_Completed(object sender, EventArgs e)
    {
        _loaded = false;
        LoadProcessSimulation();
        try
        {
            if (string.IsNullOrWhiteSpace(SearchText.Text))
            {
                viewModel.RefreshCollection();
                await viewModel.AddArticles(true);
                _loaded = true;
                LoadProcessSimulation();
                return;
            }
            viewModel.SearchArticle(SearchText.Text);
            await viewModel.AddArticles(true);
        }
        catch(Exception ex) 
        {
            await DisplayAlert("Îøèáêà!", ex.Message, "OK");
        }
        _loaded = true;
        LoadProcessSimulation();
    }

    private async void RefreshView_Refreshing(object sender, EventArgs e)
    {
        Refresher.IsRefreshing = true;
        try
        {
            _loaded = false;
            LoadProcessSimulation();
            viewModel.RefreshCollection();
            await viewModel.AddArticles(true);
            SearchText.Text = string.Empty;
            _loaded = true;
            LoadProcessSimulation();
        }
        catch(Exception ex) 
        {
            await DisplayAlert("Îøèáêà!", ex.Message, "OK");
        }
        Refresher.IsRefreshing = false;
    }

    private async void OnScrollViewScrolled(object sender, ScrolledEventArgs e)
    {
        if (sender is not ScrollView scrollView) return;

        double scrollViewHeight = scrollView.Height;
        double contentHeight = scrollView.ContentSize.Height;
        double currentScrollPosition = scrollView.ScrollY;

        if (currentScrollPosition + scrollViewHeight >= contentHeight - 100 && _scrolled)
        {
            _scrolled = false;
            await viewModel.AddArticles();
            _scrolled = true;
        }
    }

    private async void AdmindButton_Clicked(object sender, EventArgs e) =>
        await Navigation.PushAsync(new ArticleDetailPage());

    private void LoadProcessSimulation()
    {
        LoadingText1.IsVisible = _loaded;
        SearchText.IsEnabled = _loaded;
        HotArticle.IsVisible = _loaded;
        LoadingFrames.IsVisible = !_loaded;
    }
}