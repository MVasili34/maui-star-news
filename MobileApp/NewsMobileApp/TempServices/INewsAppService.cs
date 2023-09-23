using NewsMobileApp.Models;

namespace NewsMobileApp.TempServices;

public interface INewsService
{
    public IEnumerable<string> GetTags();
    public IEnumerable<Section> GetCategories();
    public IEnumerable<Article> GetLatestArticles();
    public IEnumerable<Article> GetRecommendedArticles();
    public IEnumerable<Article> GetPopularArticles();
    public IEnumerable<Article> GetThrendArticles();
}
