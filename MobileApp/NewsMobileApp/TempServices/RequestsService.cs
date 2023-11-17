using NewsMobileApp.Models;
using NewsMobileApp.Models.ODataModels;
using System.Net.Http.Json;

namespace NewsMobileApp.TempServices;

public class RequestsService : IRequestsService
{
    private readonly IHttpClientFactory _clientFactory;

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
}
