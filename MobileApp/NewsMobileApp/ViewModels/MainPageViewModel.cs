using NewsMobileApp.TempServices;
using System.Collections.ObjectModel;
using NewsMobileApp.Models;

namespace NewsMobileApp.ViewModels;

public class MainPageViewModel
{
    private readonly INewsService _newsService;

    public ObservableCollection<ArticlePreviewViewModel> NewestArticles { get; set; } = new();
    public ObservableCollection<ArticlePreviewViewModel> HystoricalArticles { get; set; } = new();
    public ObservableCollection<Section> Tags { get; set; } = new();

    public MainPageViewModel(INewsService newsService) => _newsService = newsService; 

    public async Task GetNewestArticles()
    {
        await Task.Delay(3000);
        foreach (var article in _newsService.GetLatestArticlesPreview())
        {
            NewestArticles.Add(article);
        }
    }

    public async Task GetHystoricalArticles()
    {
        await Task.Delay(3000);
        foreach (var article in _newsService.GetThrendArticlesPreview().Where(x=> x.Section.SectionId == 8))
        {
            HystoricalArticles.Add(article);
        }
    }

    public async Task GetPopularTags()
    {
        await Task.Delay(3000);
        foreach (var tag in _newsService.GetCategories())
        {
            Tags.Add(tag);
        }
    }
}
