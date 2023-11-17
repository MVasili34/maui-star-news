using NewsMobileApp.Models;
using NewsMobileApp.Models.ODataModels;
using NewsMobileApp.ViewModels;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;
using System.Net;
using ImagesCloudTool;

namespace NewsMobileApp.TempServices;

public class RequestsService : IRequestsService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly IImageCloudTool _cloudTool;

    public RequestsService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<IEnumerable<Section>> GetAllSectionsAsync()
    {
        HttpClient client = _clientFactory.CreateClient("NewsAPI");

        using HttpRequestMessage request = new(method: HttpMethod.Get, requestUri: "api/sections");

        using HttpResponseMessage response = await client.SendAsync(request);

        var sections = await response.Content.ReadFromJsonAsync<ODataModel<Section>>();

        return sections.Value;
    }

    public async Task<IEnumerable<Article>> GetThrendArticlesAsync(int? page, int? pageSize)
    {
        if(page is null || pageSize is null)
            throw new ArgumentNullException("Недопутстимые номера страниц");

        HttpClient client = _clientFactory.CreateClient("NewsAPI");

        string requestUri = "api/Articles?$select=ArticleId,Title,Subtitle,Image,PublishTime&$expand=Section($select=SectionId,Name)" +
            $"&$orderby=PublishTime%20desc&$skip={page*pageSize}&$top={pageSize}";

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

        string requestUri = $"api/Articles?$filter=contains(Title,%27{searchText}%27)&$select=ArticleId,Title,Subtitle,Image,PublishTime&$expand=Section($select=SectionId,Name)" +
            $"&$orderby=PublishTime%20desc&$skip={page * pageSize}&$top={pageSize}";

        using HttpRequestMessage request = new(method: HttpMethod.Get, requestUri: requestUri);

        using HttpResponseMessage response = await client.SendAsync(request);

        var articles = await response.Content.ReadFromJsonAsync<ODataModel<Article>>();

        return articles.Value;
    }

    public async Task<ArticleViewModel> GetArticleById(Guid? guid)
    {
        HttpClient client = _clientFactory.CreateClient("NewsAPI");

        using HttpRequestMessage request = new(method: HttpMethod.Get, requestUri: $"api/Articles({guid})");

        using HttpResponseMessage response = await client.SendAsync(request);

        var article = await response.Content.ReadFromJsonAsync<ArticleViewModel>();

        return article;
    }

    public async Task<bool> PublishArticleAsync(ArticleViewModel article)
    {
        HttpClient client = _clientFactory.CreateClient("NewsAPI");

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

        using HttpResponseMessage response = await client.DeleteAsync($"api/Articles({articleId})");

        if (response.StatusCode == HttpStatusCode.NoContent)
            return true;
        if (response.StatusCode == HttpStatusCode.BadRequest)
            throw new HttpRequestException("При удалении статьи произошла ошибка! - 400 BadRequest");
        if (response.StatusCode == HttpStatusCode.NotFound)
            throw new HttpRequestException("При удалении статьи произошла ошибка! - 404 NotFound");
        return false;
    }
}
