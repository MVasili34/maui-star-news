using NewsMobileApp.Models;
using NewsMobileApp.ViewModels;

namespace NewsMobileApp.TempServices;

public interface INewsService
{
    public IEnumerable<string> GetTags();
    public IEnumerable<Section> GetCategories();
    public IEnumerable<Article> GetLatestArticlesPreview();
    public IEnumerable<ArticleViewModel> GetLatestArticlesFull();
    public IEnumerable<Article> GetRecommendedArticlesPreview();
    public IEnumerable<ArticleViewModel> GetRecommendedArticlesFull();
    public IEnumerable<Article> GetThrendArticlesPreview();
    public IEnumerable<ArticleViewModel> GetThrendArticlesFull();
    public IEnumerable<UserViewModel> GetUsers();
}
