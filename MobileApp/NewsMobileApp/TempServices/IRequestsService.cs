using NewsMobileApp.Models;
using NewsMobileApp.ViewModels; 

namespace NewsMobileApp.TempServices;

public interface IRequestsService
{
    public Task<IEnumerable<Section>> GetAllSectionsAsync();
    public Task<IEnumerable<Article>> GetThrendArticlesAsync(int? page, int? pageSize);
    public Task<IEnumerable<Article>> GetThrendArticlesSearchAsync(string searchText, int? page, int? pageSize);
    public Task<ArticleViewModel> GetArticleById(Guid? guid);
    public Task<bool> PublishArticleAsync(ArticleViewModel article);
    public Task<bool> UpdateArticleAsync(ArticleViewModel article);
    public Task<bool> DeleteArticleAsync(Guid articleId);
}
