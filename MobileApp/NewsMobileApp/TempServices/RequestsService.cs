using NewsMobileApp.Models;
using NewsMobileApp.Models.ODataModels;
using NewsMobileApp.ViewModels;
using System.Net.Http.Json;
using System.Net;

namespace NewsMobileApp.TempServices;

public class RequestsService : IRequestsService
{
    private readonly IHttpClientFactory _clientFactory;

    public RequestsService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    #region Articles Actions Section
    public async Task<IEnumerable<Section>> GetAllSectionsAsync()
    {
        HttpClient client = _clientFactory.CreateClient("NewsAPI");
        client.DefaultRequestHeaders.Authorization = new("Bearer", await SecureStorage.GetAsync("ApiKey"));

        using HttpRequestMessage request = new(method: HttpMethod.Get, requestUri: "api/sections");

        using HttpResponseMessage response = await client.SendAsync(request);

        var sections = await response.Content.ReadFromJsonAsync<ODataModel<Section>>();

        return sections.Value;
    }

    public async Task<IEnumerable<Article>> GetThrendArticlesAsync(int page = 0, int pageSize = 10)
    {
        HttpClient client = _clientFactory.CreateClient("NewsAPI");
        client.DefaultRequestHeaders.Authorization = new("Bearer", await SecureStorage.GetAsync("ApiKey"));

        string requestUri = "api/Articles?$select=ArticleId,Title,Subtitle,Image,PublishTime,PublisherId&$expand=Section($select=SectionId,Name)" +
            $"&$orderby=PublishTime%20desc&$skip={page*pageSize}&$top={pageSize}";

        using HttpRequestMessage request = new(method: HttpMethod.Get, requestUri: requestUri);

        using HttpResponseMessage response = await client.SendAsync(request);

        var articles = await response.Content.ReadFromJsonAsync<ODataModel<Article>>();

        return articles.Value;
    }

    public async Task<IEnumerable<Article>> GetArticlesBySectionAsync(int sectionId, int? page, int? pageSize)
    {
        if (page is null || pageSize is null)
            throw new ArgumentNullException("Недопутстимые номера страниц");

        HttpClient client = _clientFactory.CreateClient("NewsAPI");
        client.DefaultRequestHeaders.Authorization = new("Bearer", await SecureStorage.GetAsync("ApiKey"));

        string requestUri = $"api/Articles?$filter=(SectionId eq {sectionId})&$select=ArticleId,Title,Subtitle,Image,PublishTime,PublisherId&$expand=Section($select=SectionId,Name)" +
            $"&$orderby=PublishTime%20desc&$skip={page * pageSize}&$top={pageSize}";

        using HttpRequestMessage request = new(method: HttpMethod.Get, requestUri: requestUri);

        using HttpResponseMessage response = await client.SendAsync(request);

        var articles = await response.Content.ReadFromJsonAsync<ODataModel<Article>>();

        return articles.Value;
    }

    public async Task<IEnumerable<Article>> GetThrendArticlesSearchAsync(string searchText, int? page, int? pageSize)
    {
        if (page is null || pageSize is null)
            throw new ArgumentNullException("Недопутстимые номера страниц");

        HttpClient client = _clientFactory.CreateClient("NewsAPI");
        client.DefaultRequestHeaders.Authorization = new("Bearer", await SecureStorage.GetAsync("ApiKey"));

        string requestUri = $"api/Articles?$filter=contains(Title,%27{searchText}%27)&$select=ArticleId,Title,Subtitle,Image,PublishTime,PublisherId&$expand=Section($select=SectionId,Name)" +
            $"&$orderby=PublishTime%20desc&$skip={page * pageSize}&$top={pageSize}";

        using HttpRequestMessage request = new(method: HttpMethod.Get, requestUri: requestUri);

        using HttpResponseMessage response = await client.SendAsync(request);

        var articles = await response.Content.ReadFromJsonAsync<ODataModel<Article>>();

        return articles.Value;
    }

    public async Task<IEnumerable<Article>> GetArticlesSectionSearchAsync(string searchText, int sectionId, int? page, int? pageSize)
    {
        if (page is null || pageSize is null)
            throw new ArgumentNullException("Недопутстимые номера страниц");

        HttpClient client = _clientFactory.CreateClient("NewsAPI");
        client.DefaultRequestHeaders.Authorization = new("Bearer", await SecureStorage.GetAsync("ApiKey"));

        string requestUri = $"api/Articles?$filter=contains(Title,%27{searchText}%27)and(SectionId eq {sectionId})&$select=ArticleId,Title,Subtitle,Image,PublishTime,PublisherId&$expand=Section($select=SectionId,Name)" +
            $"&$orderby=PublishTime%20desc&$skip={page * pageSize}&$top={pageSize}";

        using HttpRequestMessage request = new(method: HttpMethod.Get, requestUri: requestUri);

        using HttpResponseMessage response = await client.SendAsync(request);

        var articles = await response.Content.ReadFromJsonAsync<ODataModel<Article>>();

        return articles.Value;
    }

    public async Task<ArticleViewModel> GetArticleById(Guid? guid)
    {
        HttpClient client = _clientFactory.CreateClient("NewsAPI");
        client.DefaultRequestHeaders.Authorization = new("Bearer", await SecureStorage.GetAsync("ApiKey"));

        using HttpRequestMessage request = new(method: HttpMethod.Get, requestUri: $"api/Articles({guid})");

        using HttpResponseMessage response = await client.SendAsync(request);

        var article = await response.Content.ReadFromJsonAsync<ArticleViewModel>();

        return article;
    }

    public async Task<bool> PublishArticleAsync(ArticleViewModel article)
    {
        HttpClient client = _clientFactory.CreateClient("NewsAPI");
        client.DefaultRequestHeaders.Authorization = new("Bearer", await SecureStorage.GetAsync("ApiKey"));

        using HttpResponseMessage response = await client.PostAsJsonAsync($"api/Articles", article);

        if (response.StatusCode == HttpStatusCode.Created)
            return true;
        if (response.StatusCode == HttpStatusCode.BadRequest)
            throw new HttpRequestException("При публикации статьи произошла ошибка! - 400 BadRequest");
        return false;
    }

    public async Task<bool> UpdateArticleAsync(ArticleViewModel article)
    {
        HttpClient client = _clientFactory.CreateClient("NewsAPI");
        client.DefaultRequestHeaders.Authorization = new("Bearer", await SecureStorage.GetAsync("ApiKey"));

        using HttpResponseMessage response = await client.PutAsJsonAsync($"api/Articles({article.ArticleId})", article);

        if (response.StatusCode == HttpStatusCode.NoContent)
            return true;
        if (response.StatusCode == HttpStatusCode.BadRequest)
            throw new HttpRequestException("При обновлении статьи произошла ошибка! - 400 BadRequest");
        if (response.StatusCode == HttpStatusCode.NotFound)
            throw new HttpRequestException("При обновлении статьи произошла ошибка! - 404 NotFound");
        return false;
    }

    public async Task<bool> DeleteArticleAsync(Guid articleId)
    {
        HttpClient client = _clientFactory.CreateClient("NewsAPI");
        client.DefaultRequestHeaders.Authorization = new("Bearer", await SecureStorage.GetAsync("ApiKey"));

        using HttpResponseMessage response = await client.DeleteAsync($"api/Articles({articleId})");

        if (response.StatusCode == HttpStatusCode.NoContent)
            return true;
        if (response.StatusCode == HttpStatusCode.BadRequest)
            throw new HttpRequestException("При удалении статьи произошла ошибка! - 400 BadRequest");
        if (response.StatusCode == HttpStatusCode.NotFound)
            throw new HttpRequestException("При удалении статьи произошла ошибка! - 404 NotFound");
        return false;
    }

    #endregion

    #region Manage User Data Section
    public async Task<UserViewModel> RegisterUserAsync(RegisterModel model)
    {
        HttpClient client = _clientFactory.CreateClient("NewsAPI");

        using HttpResponseMessage response = await client.PostAsJsonAsync("api/Auth/register", model);

        if (response.StatusCode == HttpStatusCode.Created)
            return await response.Content.ReadFromJsonAsync<UserViewModel>();
        if (response.StatusCode == HttpStatusCode.BadRequest)
            throw new HttpRequestException("Пользователь с таким аккаунтом уже существует! - 400 BadRequest");
        return null;
    }

    public async Task<UserViewModel> LoginUserAsync(AuthorizeViewModel model)
    {
        HttpClient client = _clientFactory.CreateClient("NewsAPI");

        using HttpResponseMessage response = await client.PostAsJsonAsync("api/Auth/login", model);

        if (response.IsSuccessStatusCode)
        {
            UserViewModel user = await response.Content.ReadFromJsonAsync<UserViewModel>();
            await SecureStorage.SetAsync("ApiKey", user.PasswordHash);
            return user;
        }
        if (response.StatusCode == HttpStatusCode.Unauthorized)
            throw new HttpRequestException("Неверный логин или пароль!");
        if (response.StatusCode == HttpStatusCode.BadRequest)
            throw new HttpRequestException("Ошибка авторизации! - 400 BadRequest");
        return null;
    }

    public async Task<bool> ChangeUserPasswordAsync(ChangePasswordViewModel changedPass)
    {
        HttpClient client = _clientFactory.CreateClient("NewsAPI");

        using HttpResponseMessage response = await client.PostAsJsonAsync($"api/Auth/changepswd", changedPass);

        if (response.IsSuccessStatusCode)
            return true;
        if (response.StatusCode == HttpStatusCode.NotFound)
            throw new HttpRequestException("Пользователь при обновлении пароля не найден либо неверный пароль!");
        if (response.StatusCode == HttpStatusCode.BadRequest)
            throw new HttpRequestException("При изменении пароля пользователя произошла ошибка! - 400 BadRequest");
        return false;
    }

    public async Task<bool> UpdateUserAsync(UserViewModel model)
    {
        HttpClient client = _clientFactory.CreateClient("NewsAPI");
        client.DefaultRequestHeaders.Authorization = new("Bearer", await SecureStorage.GetAsync("ApiKey"));

        using HttpResponseMessage response = await client.PutAsJsonAsync($"api/Users/{model.UserId}", model);

        if (response.IsSuccessStatusCode)
            return true;
        if (response.StatusCode == HttpStatusCode.NotFound)
            throw new HttpRequestException("Пользователя не существует!");
        if (response.StatusCode == HttpStatusCode.BadRequest)
            throw new HttpRequestException("Ошибка авторизации или обновления данных! - 400 BadRequest");
        return false;
    }

    public async Task<IEnumerable<UserViewModel>> GetUsersAsync(int? page, int? pageSize)
    {
        if (page is null || pageSize is null)
            throw new ArgumentNullException("Недопутстимые номера страниц");

        HttpClient client = _clientFactory.CreateClient("NewsAPI");
        client.DefaultRequestHeaders.Authorization = new("Bearer", await SecureStorage.GetAsync("ApiKey"));

        string requestUri = $"api/Users?offset={page}&limit={pageSize}";

        using HttpRequestMessage request = new(method: HttpMethod.Get, requestUri: requestUri);

        using HttpResponseMessage response = await client.SendAsync(request);

        return await response.Content.ReadFromJsonAsync<IEnumerable<UserViewModel>>();
    }

    public async Task<IEnumerable<DiagramData>> GetDiagramAsync(DateTime start, DateTime end)
    {
        HttpClient client = _clientFactory.CreateClient("NewsAPI");
        client.DefaultRequestHeaders.Authorization = new("Bearer", await SecureStorage.GetAsync("ApiKey"));

        string requestUri = $"api/Articles?$filter=(PublishTime ge {start:yyyy-MM-ddTHH:mm:ssZ}) and (PublishTime le {end:yyyy-MM-ddTHH:mm:ssZ})" +
            $"&$apply=groupby((PublishTime),aggregate(ArticleId with countdistinct as total))";

        using HttpRequestMessage request = new(method: HttpMethod.Get, requestUri: requestUri);

        using HttpResponseMessage response = await client.SendAsync(request);

        var result = await response.Content.ReadFromJsonAsync<ODataModel<DiagramData>>();
        return result.Value;
    }

    public async Task<UserViewModel> GetUserByIdAsync(string id)
    {
        HttpClient client = _clientFactory.CreateClient("NewsAPI");
        client.DefaultRequestHeaders.Authorization = new("Bearer", await SecureStorage.GetAsync("ApiKey"));

        using HttpRequestMessage request = new(method: HttpMethod.Get, requestUri: $"api/Users/{id}");

        using HttpResponseMessage response = await client.SendAsync(request);

        return await response.Content.ReadFromJsonAsync<UserViewModel>();
    }

    public async Task<bool> UpdateUserAdminAsync(UserViewModel model)
    {
        HttpClient client = _clientFactory.CreateClient("NewsAPI");
        client.DefaultRequestHeaders.Authorization = new("Bearer", await SecureStorage.GetAsync("ApiKey"));

        using HttpResponseMessage response = await client.PutAsJsonAsync($"api/Users/updadm/{model.UserId}", model);

        if (response.StatusCode == HttpStatusCode.NoContent)
            return true;
        if (response.StatusCode == HttpStatusCode.NotFound)
            throw new HttpRequestException("Пользователь для обновления данных не найден!");
        if (response.StatusCode == HttpStatusCode.BadRequest)
            throw new HttpRequestException("При обновлении пользователя произошла ошибка! - 400 BadRequest");
        return false;
    }

    public async Task<bool> UpdateUserPswByAdminAsync(AuthorizeViewModel model)
    {
        HttpClient client = _clientFactory.CreateClient("NewsAPI");
        client.DefaultRequestHeaders.Authorization = new("Bearer", await SecureStorage.GetAsync("ApiKey"));

        using HttpResponseMessage response = await client.PostAsJsonAsync($"api/Users/adminsetpswd", model);

        if (response.IsSuccessStatusCode)
            return true;
        if (response.StatusCode == HttpStatusCode.NotFound)
            throw new HttpRequestException("Пользователь при обновлении пароля не найден!");
        if (response.StatusCode == HttpStatusCode.BadRequest)
            throw new HttpRequestException("При изменении пароля пользователя произошла ошибка! - 400 BadRequest");
        return false;
    }

    public async Task<bool> DeleteUserAsync(Guid id)
    {
        HttpClient client = _clientFactory.CreateClient("NewsAPI");
        client.DefaultRequestHeaders.Authorization = new("Bearer", await SecureStorage.GetAsync("ApiKey"));

        using HttpRequestMessage request = new(method: HttpMethod.Delete, requestUri: $"api/Users/{id}");

        using HttpResponseMessage response = await client.SendAsync(request);

        if (response.StatusCode == HttpStatusCode.NoContent)
            return true;
        if (response.StatusCode == HttpStatusCode.NotFound)
            throw new HttpRequestException("Пользователь для удаления не найден!");
        if (response.StatusCode == HttpStatusCode.BadRequest)
            throw new HttpRequestException("При удалении пользователя произошла ошибка! - 400 BadRequest");
        return false;
    }

    #endregion
}
