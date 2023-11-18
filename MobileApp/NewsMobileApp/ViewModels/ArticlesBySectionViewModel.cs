using System.Collections.ObjectModel;
using NewsMobileApp.Models;
using NewsMobileApp.TempServices;

namespace NewsMobileApp.ViewModels;

public class ArticlesBySectionViewModel
{
    private readonly IRequestsService _requestService;

    public ObservableCollection<Article> Articles { get; set; } = new();
    private readonly int _sectionId = 0;

    public ArticlesBySectionViewModel(int sectionId)
    {
        _requestService = Application.Current.Handler
           .MauiContext.Services.GetService<IRequestsService>();
        _sectionId = sectionId;
    }

    public async Task AddArticles(bool clear = false, int limit = 20, int offset = 0)
    {
        if (clear) Articles.Clear();

        foreach (var article in await _requestService.GetArticlesBySectionAsync(_sectionId, offset, limit))
        {
            Articles.Add(article);
        }
    }

    public async Task SearchArticle(string text, int limit = 20, int offset = 0)
    {
        IEnumerable<Article> found = await _requestService.GetArticlesSectionSearchAsync(text, _sectionId, offset, limit);
        if (found.Any())
        {
            foreach (var article in found)
            {
                Articles.Add(article);
            }
        }
    }
}
