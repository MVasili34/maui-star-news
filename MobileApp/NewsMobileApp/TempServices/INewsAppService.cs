using NewsMobileApp.Models;
using NewsMobileApp.ViewModels;

namespace NewsMobileApp.TempServices;

public interface INewsService
{
    public IEnumerable<string> GetTags();
    public IEnumerable<Section> GetCategories();
    public IEnumerable<ArticlePreviewViewModel> GetLatestArticlesPreview();
    public IEnumerable<ArticleViewModel> GetLatestArticlesFull();
    public IEnumerable<ArticlePreviewViewModel> GetRecommendedArticlesPreview();
    public IEnumerable<ArticleViewModel> GetRecommendedArticlesFull();
    public IEnumerable<ArticlePreviewViewModel> GetThrendArticlesPreview();
    public IEnumerable<ArticleViewModel> GetThrendArticlesFull();
    public IEnumerable<UserViewModel> GetUsers();
}
