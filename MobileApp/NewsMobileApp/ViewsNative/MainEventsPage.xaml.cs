using NewsMobileApp.Models;
using NewsMobileApp.TempServices;
using NewsMobileApp.ViewModels;

namespace NewsMobileApp.ViewsNative;

public partial class MainEventsPage : ContentPage
{
    private MainPageViewModel viewModel => BindingContext as MainPageViewModel;
    private bool _loaded = false;

	public MainEventsPage(MainPageViewModel viewModel)
	{
		InitializeComponent();
        LoadProcessSimulation();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (this.AnimationIsRunning("TransitionAnimation"))
            return;

        var parentAnimation = AnimationCreator.SetAnimations(LoadBlock1, LoadBlock2, LoadBlock3, LoadBlock4, 
            LoadBlock5, Display1, Display2, LoadSimHeader1, LoadSimHeader2, LoadSimHeader3);

        parentAnimation.Commit(this, "TransitionAnimation", length: 2000, repeat: () => true);

        if (_loaded) return;

        try
        {
            await Task.WhenAll(viewModel.GetNewestArticles(),
                                   viewModel.GetHystoricalArticles(),
                                   viewModel.GetPopularTags());
            _loaded = true;
            LoadProcessSimulation();
        }
        catch (Exception ex) 
        {
            await DisplayAlert("Ошибка!", ex.Message, "OK");
        }
    }

    private async void Tag_Clicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        if(button.CommandParameter is not Section section)
        {
            await DisplayAlert("Ошибка!", "Произошла неизвестная ошибка.", "OK");
            return;
        }
        await Navigation.PushAsync(new ArticlesBySectionPage(section));
    }

    private async void LatestNews_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection[0] is not Article article) return;

        await Navigation.PushAsync(new ArticlePage(article));
    }

    private async void ShowAllThrebds_Tapped(object sender, TappedEventArgs e) => await Shell.Current
        .GoToAsync("//thrend");

    private async void ToSection_Tapped(object sender, TappedEventArgs e) => await Navigation
        .PushAsync(new ArticlesBySectionPage(viewModel.Tags.First(x => x.SectionId == 2)));

    private void LoadProcessSimulation()
    {
        LatestNews.IsVisible = _loaded;
        RecommendedNews.IsVisible = _loaded;
        ShowAll1.IsVisible = _loaded;
        ShowAll2.IsVisible = _loaded;
        Header1.IsVisible = _loaded;
        Header2.IsVisible = _loaded;
        Header3.IsVisible = _loaded;
        LoadSimHeader1.IsVisible = !_loaded;
        LoadSimHeader2.IsVisible = !_loaded;
        LoadSimHeader3.IsVisible = !_loaded;
        Display1.IsVisible = !_loaded;
        Display2.IsVisible = !_loaded;
    }
}