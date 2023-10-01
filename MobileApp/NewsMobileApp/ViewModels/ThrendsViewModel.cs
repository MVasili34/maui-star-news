using NewsMobileApp.Models;
using NewsMobileApp.TempServices;
using System.Collections.ObjectModel;

namespace NewsMobileApp.ViewModels;

public class ThrendsViewModel : ObservableCollection<Article>
{
    private readonly INewsService _newsService;

    public ThrendsViewModel(INewsService newsService) => _newsService = newsService;

    public void AddArticles(bool clear = false, int limit = 0, int offset = 0)
    {
        if (clear) Clear();

        foreach (var article in _newsService.GetThrendArticles())
        {
            Add(article);
        }
    }

    public void SearchArticle(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            AddArticles(true);
            return;
        }

        Clear();
        IEnumerable<Article> found = _newsService.GetThrendArticles()
                                                 .Where(x => x.Title.Contains(text))
                                                 .Take(10);

        if(found.Any())
        {
            foreach (var article in found)
            {
                Add(article);
            }
        }
    }
}
