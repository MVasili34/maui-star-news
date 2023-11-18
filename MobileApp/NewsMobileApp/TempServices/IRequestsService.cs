using NewsMobileApp.Models;
using NewsMobileApp.ViewModels;
using NewsMobileApp.Models.ODataModels;

namespace NewsMobileApp.TempServices;

public interface IRequestsService
{
    public Task<IEnumerable<Section>> GetAllSectionsAsync();
    public Task<IEnumerable<Article>> GetThrendArticlesAsync(int? page, int? pageSize);
    public Task<IEnumerable<Article>> GetArticlesBySectionAsync(int sectionId, int? page, int? pageSize);
    public Task<IEnumerable<Article>> GetThrendArticlesSearchAsync(string searchText, int? page, int? pageSize);
    public Task<IEnumerable<Article>> GetArticlesSectionSearchAsync(string searchText, int sectionId, int? page, int? pageSize);
    public Task<ArticleViewModel> GetArticleById(Guid? guid);
    public Task<bool> PublishArticleAsync(ArticleViewModel article);
    public Task<bool> UpdateArticleAsync(ArticleViewModel article);
    public Task<bool> DeleteArticleAsync(Guid articleId);
    public Task<UserViewModel> RegisterUserAsync(RegisterModel model);
    public Task<UserViewModel> LoginUserAsync(AuthorizeModel model);
    public Task<IEnumerable<UserViewModel>> GetUsersAsync(int? page, int? pageSize);
    public Task<IEnumerable<DiagramData>> GetDiagramAsync(DateTime start, DateTime end);
    public Task<UserViewModel> GetUserByIdAsync(string id);
}
