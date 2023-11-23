using NewsMobileApp.Models;
using NewsMobileApp.ViewModels;
using NewsMobileApp.Models.ODataModels;

namespace NewsMobileApp.TempServices;

public interface IRequestsService
{
    Task<IEnumerable<Section>> GetAllSectionsAsync();
    Task<IEnumerable<Article>> GetThrendArticlesAsync(int? page, int? pageSize);
    Task<IEnumerable<Article>> GetArticlesBySectionAsync(int sectionId, int? page, int? pageSize);
    Task<IEnumerable<Article>> GetThrendArticlesSearchAsync(string searchText, int? page, int? pageSize);
    Task<IEnumerable<Article>> GetArticlesSectionSearchAsync(string searchText, int sectionId, int? page, int? pageSize);
    Task<ArticleViewModel> GetArticleById(Guid? guid);
    Task<bool> PublishArticleAsync(ArticleViewModel article);
    Task<bool> UpdateArticleAsync(ArticleViewModel article);
    Task<bool> DeleteArticleAsync(Guid articleId);
    Task<UserViewModel> RegisterUserAsync(RegisterModel model);
    Task<UserViewModel> LoginUserAsync(AuthorizeModel model);
    Task<bool> UpdateUserAsync(UserViewModel model);
    Task<IEnumerable<UserViewModel>> GetUsersAsync(int? page, int? pageSize);
    Task<IEnumerable<DiagramData>> GetDiagramAsync(DateTime start, DateTime end);
    Task<UserViewModel> GetUserByIdAsync(string id);
    Task<bool> ChangeUserPasswordAsync(ChangePasswordModel changedPass);
    Task<bool> DeleteUserAsync(Guid id);
}
