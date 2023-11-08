using NewsMobileApp.TempServices;
using NewsMobileApp.ViewModels;
using NewsMobileApp.Models;

namespace NewsMobileApp.ViewsNative;

public partial class ArticlesBySectionPage : ContentPage
{
    private ArticlesBySectionViewModel viewModel => BindingContext as ArticlesBySectionViewModel;
    private bool _loaded = false;
    private bool scrolled = true;
    private bool searching = false;
    private string currentSearch = string.Empty;
    private const int limit = 20;
    private int offset = 0;

    public ArticlesBySectionPage(Section section)
    {
        InitializeComponent();
        PageTitle.Text = section.Name;
        //AdmindButton.IsVisible = false;
        BindingContext = new ArticlesBySectionViewModel(section.SectionId);
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        if (this.AnimationIsRunning("TransitionAnimation"))
            return;

        var parentAnimation = AnimationCreator.SetAnimations(LoadBlock1, LoadBlock2, LoadBlock3, LoadBlock4);

        parentAnimation.Commit(this, "TransitionAnimation", length: 2000, repeat: () => true);
        try
        {
            await viewModel.AddArticles(limit: limit, offset: offset);
            offset++;
            _loaded = true;
            LoadProcessSimulation();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Îøèáêà!", ex.Message, "OK");
        }
    }

    private async void SectionArticle_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection[0] is not Article article) return;

        await Navigation.PushAsync(new ArticlePage(article.ArticleId));
    }

    private async void SearchText_Completed(object sender, EventArgs e)
    {
        _loaded = false;
        LoadProcessSimulation();
        try
        {
            if (string.IsNullOrEmpty(SearchText.Text) || string.IsNullOrWhiteSpace(SearchText.Text))
            {
                await viewModel.AddArticles(true);
                offset = 1;
                searching = false;
                _loaded = true;
                LoadProcessSimulation();
                return;
            }
            currentSearch = SearchText.Text;
            viewModel.Articles.Clear();
            searching = true;
            offset = 0;
            await viewModel.SearchArticle(currentSearch, limit: limit, offset: offset++);

        }
        catch (Exception ex)
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
            await viewModel.AddArticles(true);
            offset = 1;
            SearchText.Text = string.Empty;
            searching = false;
            _loaded = true;
            LoadProcessSimulation();
        }
        catch (Exception ex)
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

        if (currentScrollPosition + scrollViewHeight >= contentHeight - 100 && scrolled)
        {
            scrolled = false;
            try
            {
                if (!searching)
                {
                    await viewModel.AddArticles(limit: limit, offset: offset);
                }
                else
                {
                    await viewModel.SearchArticle(currentSearch, limit: limit, offset: offset);
                }
                offset++;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Îøèáêà!", ex.Message, "OK");
            }
            scrolled = true;
        }
    }

    private async void AdmindButton_Clicked(object sender, EventArgs e) =>
        await Navigation.PushAsync(new ArticleDetailPage());

    private void LoadProcessSimulation()
    {
        LoadingText1.IsVisible = _loaded;
        SearchText.IsEnabled = _loaded;
        HotArticle.IsVisible = _loaded;
        LoadFrames.IsVisible = !_loaded;
    }
}