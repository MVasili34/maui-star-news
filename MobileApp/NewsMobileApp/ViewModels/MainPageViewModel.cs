using NewsMobileApp.TempServices;
using System.Collections.ObjectModel;
using NewsMobileApp.Models;

namespace NewsMobileApp.ViewModels;

public class MainPageViewModel
{
    private readonly IRequestsService _requestService;

    public ObservableCollection<Article> NewestArticles { get; set; } = new();
    public ObservableCollection<Article> HystoricalArticles { get; set; } = new();
    public ObservableCollection<Section> Tags { get; set; } = new();

    public MainPageViewModel(IRequestsService requestService) => _requestService = requestService; 

    public async Task GetNewestArticles(bool update = false)
    {
        if (update) NewestArticles.Clear();

        foreach (var article in await _requestService.GetThrendArticlesAsync(0, 5))
        {
            NewestArticles.Add(article);
        }
    }

    public async Task GetHystoricalArticles(bool update = false)
    {
        if (update) HystoricalArticles.Clear();

        foreach (var article in await _requestService.GetArticlesBySectionAsync(2, 0, 5))
        {
            HystoricalArticles.Add(article);
        }
    }

    public async Task GetPopularTags(bool update = false)
    {
        if (update) Tags.Clear();

        foreach (var tag in await _requestService.GetAllSectionsAsync())
        {
            Tags.Add(tag);
        }
    }
}
