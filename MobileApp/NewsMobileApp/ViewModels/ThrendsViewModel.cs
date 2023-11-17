using NewsMobileApp.TempServices;
using NewsMobileApp.Models;
using System.Collections.ObjectModel;

namespace NewsMobileApp.ViewModels;

public class ThrendsViewModel
{
    private readonly INewsService _newsService;

    public ObservableCollection<Article> Articles { get; set; } = new();

    public ThrendsViewModel(INewsService newsService) => _newsService = newsService;

    public async Task AddArticles(bool clear = false, int limit = 20, int offset = 0)
    {
        if (clear) Articles.Clear();

        await Task.Delay(3000);

        foreach (var article in _newsService.GetThrendArticlesPreview())
        {
            Articles.Add(article);
        }
    }

    public async Task SearchArticle(string text, int limit = 20, int offset = 0)
    {
        await Task.Delay(3000);
        IEnumerable<Article> found = _newsService.GetThrendArticlesPreview()
                    .Where(x => x.Title.Contains(text))
                    .Skip(offset*limit)
                    .Take(limit);

        if (found.Any())
        {
            foreach (var article in found)
            {
                Articles.Add(article);
            }
        }
    }   
    /*
    private readonly IRequestsService _requestService;

    public ObservableCollection<Article> Articles { get; set; } = new();

    public ThrendsViewModel(IRequestsService requestService) => _requestService = requestService;

    public async Task AddArticles(bool clear = false, int limit = 20, int offset = 0)
    {
        if (clear) Articles.Clear();

        foreach (var article in await _requestService.GetThrendArticlesAsync(offset, limit))
        {
            Articles.Add(article);
        }
    }

    public async Task SearchArticle(string text, int limit = 20, int offset = 0)
    {
        IEnumerable<Article> found = await _requestService.GetThrendArticlesSearchAsync(text, offset, limit);

        if (found.Any())
        {
            foreach (var article in found)
            {
                Articles.Add(article);
            }
        }
    */
}
