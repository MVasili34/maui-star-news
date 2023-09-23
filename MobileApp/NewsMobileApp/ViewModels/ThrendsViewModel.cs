using NewsMobileApp.Models;
using NewsMobileApp.TempServices;

namespace NewsMobileApp.ViewModels;

public class ThrendsViewModel
{
    public IEnumerable<Article> Articles { get; set; }

    public ThrendsViewModel(INewsService newsService)
    {
        Articles = newsService.GetThrendArticles();
    }
}
