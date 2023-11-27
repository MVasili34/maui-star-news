using NewsMobileApp.Models;
using NewsMobileApp.ViewModels;
using NewsMobileApp.Models.ODataModels;

namespace NewsMobileApp.TempServices;

public interface IRequestsService
{
    /// <summary>
    /// Gets collection of sections asynchronously.
    /// </summary>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation. 
    /// The <see cref="Task"/> result contains a collection of <see cref="Section"/> objects.
    /// </returns>
    Task<IEnumerable<Section>> GetAllSectionsAsync();

    /// <summary>
    /// Gets paginated collection of newest articles asynchronously.
    /// </summary>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Amount of objects in the page</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation. 
    /// The <see cref="Task"/> result contains a paginated collection of <see cref="Article"/> objects.
    /// </returns>
    Task<IEnumerable<Article>> GetThrendArticlesAsync(int page, int pageSize);

    /// <summary>
    /// Gets paginated collection of newest articles by section asynchronously.
    /// </summary>
    /// <param name="sectionId">Section id</param>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Amount of objects in the page</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation.<br/>
    /// The <see cref="Task"/> result contains a paginated collection of <see cref="Article"/> objects
    /// by required section.
    /// </returns>
    Task<IEnumerable<Article>> GetArticlesBySectionAsync(int sectionId, int? page, int? pageSize);

    /// <summary>
    /// Gets paginated collection of articles by search title asynchronously.
    /// </summary>
    /// <param name="searchText">Search text</param>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Amount of objects in the page</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation.<br/>
    /// The <see cref="Task"/> result contains a paginated collection of <see cref="Article"/> objects
    /// with required title.
    /// </returns>
    Task<IEnumerable<Article>> GetThrendArticlesSearchAsync(string searchText, int? page, int? pageSize);

    /// <summary>
    /// Gets paginated collection of articles by search title an section asynchronously.
    /// </summary>
    /// <param name="searchText">Search text</param>
    /// <param name="sectionId">Section id</param>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Amount of objects in the page</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation.<br/>
    /// The <see cref="Task"/> result contains a paginated collection of <see cref="Article"/> objects
    /// with required title and section.
    /// </returns>
    Task<IEnumerable<Article>> GetArticlesSectionSearchAsync(string searchText, int sectionId, int? page, int? pageSize);

    /// <summary>
    /// Gets required article by id asynchronously.
    /// </summary>
    /// <param name="id">Article id</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation. 
    /// The <see cref="Task"/> result contains found <see cref="Article"/> object.
    /// </returns>
    Task<ArticleViewModel> GetArticleByIdAsync(Guid? id);

    /// <summary>
    /// Publishes an article asynchronously.
    /// </summary>
    /// <param name="article"><see cref="ArticleViewModel"/> object.</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation.<br/>
    /// The <see cref="Task"/> result is <see href="true"/> or <see href="false"/> 
    /// indicating success or failure respectively.
    /// </returns>
    Task<bool> PublishArticleAsync(ArticleViewModel article);

    /// <summary>
    /// Updates an article asynchronously.
    /// </summary>
    /// <param name="article"><see cref="ArticleViewModel"/> object.</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation.<br/>
    /// The <see cref="Task"/> result is <see href="true"/> or <see href="false"/> 
    /// indicating success or failure respectively.
    /// </returns>
    Task<bool> UpdateArticleAsync(ArticleViewModel article);

    /// <summary>
    /// Deletes an article asynchronously.
    /// </summary>
    /// <param name="articleId">Article id</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation.<br/>
    /// The <see cref="Task"/> result is <see href="true"/> or <see href="false"/> 
    /// indicating success or failure respectively.
    /// </returns>
    Task<bool> DeleteArticleAsync(Guid articleId);

    /// <summary>
    /// Registers a user asynchronously.
    /// </summary>
    /// <param name="model"><see cref="RegisterViewModel"/> instance</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation. <br/>
    /// The <see cref="Task"/> result contains registered <see cref="UserViewModel"/> 
    /// object, <see href="null"/> otherwise.
    /// </returns>
    Task<UserViewModel> RegisterUserAsync(RegisterViewModel model);

    /// <summary>
    /// Logs in a user asynchronously.
    /// </summary>
    /// <param name="model"><see cref="AuthorizeViewModel"/> instance</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation. <br/>
    /// The <see cref="Task"/> result contains authorized <see cref="UserViewModel"/> 
    /// object, <see href="null"/> otherwise.
    /// </returns>
    Task<UserViewModel> LoginUserAsync(AuthorizeViewModel model);

    /// <summary>
    /// Updates a user asynchronously.
    /// </summary>
    /// <param name="model"><see cref="AuthorizeViewModel"/> instance</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation.<br/>
    /// The <see cref="Task"/> result is <see href="true"/> or <see href="false"/> 
    /// indicating success or failure respectively.
    /// </returns>
    Task<bool> UpdateUserAsync(UserViewModel model);

    /// <summary>
    /// Gets paginated collection of registered users asynchronously.
    /// </summary>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Amount of objects in the page</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation. 
    /// The <see cref="Task"/> result contains a paginated collection of <see cref="UserViewModel"/> objects.
    /// </returns>
    Task<IEnumerable<UserViewModel>> GetUsersAsync(int? page, int? pageSize);

    /// <summary>
    /// Gets diagram data between two dates for admin panel asynchronously.
    /// </summary>
    /// <param name="start">Start date of monitoring</param>
    /// <param name="end">End date of monitoring</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation. 
    /// The <see cref="Task"/> result contains a collection of <see cref="DiagramData"/> objects.
    /// </returns>
    Task<IEnumerable<DiagramData>> GetDiagramAsync(DateTime start, DateTime end);

    /// <summary>
    /// Search a user by ID asynchronously.
    /// </summary>
    /// <param name="id">Search id text</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation. 
    /// The <see cref="Task"/> result contains found <see cref="UserViewModel"/> object.
    /// </returns>
    Task<UserViewModel> GetUserByIdAsync(string id);

    /// <summary>
    /// Changes a user's password asynchronously.
    /// </summary>
    /// <param name="changedPass"><see cref="ChangePasswordViewModel"/> instance</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation.<br/>
    /// The <see cref="Task"/> result is <see href="true"/> or <see href="false"/> 
    /// indicating success or failure respectively.
    /// </returns>
    Task<bool> ChangeUserPasswordAsync(ChangePasswordViewModel changedPass);

    /// <summary>
    /// Updates a user's by admin claims asynchronously.
    /// </summary>
    /// <param name="model"><see cref="UserViewModel"/> instance</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation.<br/>
    /// The <see cref="Task"/> result is <see href="true"/> or <see href="false"/> 
    /// indicating success or failure respectively.
    /// </returns>
    Task<bool> UpdateUserAdminAsync(UserViewModel model);

    /// <summary>
    /// Updates a user's password by admin asynchronously.
    /// </summary>
    /// <param name="model"><see cref="AuthorizeViewModel"/> instance</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation.<br/>
    /// The <see cref="Task"/> result is <see href="true"/> or <see href="false"/> 
    /// indicating success or failure respectively.
    /// </returns>
    Task<bool> UpdateUserPswByAdminAsync(AuthorizeViewModel model);

    /// <summary>
    /// Deletes a user asynchronously.
    /// </summary>
    /// <param name="id">User id</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation.<br/>
    /// The <see cref="Task"/> result is <see href="true"/> or <see href="false"/> 
    /// indicating success or failure respectively.
    /// </returns>
    Task<bool> DeleteUserAsync(Guid id);
}
