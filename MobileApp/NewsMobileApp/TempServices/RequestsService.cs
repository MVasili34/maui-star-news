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
}
