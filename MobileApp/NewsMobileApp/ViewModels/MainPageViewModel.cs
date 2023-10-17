using NewsMobileApp.TempServices;
using System.Collections.ObjectModel;

namespace NewsMobileApp.ViewModels;

public class MainPageViewModel
{
    private readonly INewsService _newsService;
    public ObservableCollection<ArticlePreviewViewModel> NewestArticles { get; set; } = new();
    public ObservableCollection<ArticlePreviewViewModel> HystoricalArticles { get; set; } = new();
    public ObservableCollection<string> Tags { get; set; } = new();

    public MainPageViewModel() => _newsService = Application.Current.Handler
           .MauiContext.Services.GetService<INewsService>();

    public async Task GetNewestArticles()
    {
        try
        {
            foreach(var article in _newsService.GetLatestArticlesPreview())
            {
                NewestArticles.Add(article);
            }
        }
        catch (Exception ex) 
        {
            await Shell.Current.DisplayAlert("OK", $"{ex.Message}", "OK");
        }
    }

    public async Task GetHystoricalArticles()
    {
        try
        {
            foreach (var article in _newsService.GetThrendArticlesPreview().Where(x=> x.Section.SectionId == 8))
            {
                HystoricalArticles.Add(article);
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("OK", $"{ex.Message}", "OK");
        }
    }

    public async Task GetPopularTags()
    {
        try
        {
            foreach (var tag in _newsService.GetTags().Take(6))
            {
                Tags.Add(tag);
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("OK", $"{ex.Message}", "OK");
        }
    }
}
