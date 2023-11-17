using NewsMobileApp.Models;

namespace NewsMobileApp.TempServices;

public interface IRequestsService
{
    public Task<IEnumerable<Section>> GetAllSectionsAsync();
    public Task<IEnumerable<Article>> GetThrendArticlesAsync(int? page, int? pageSize);
    public Task<IEnumerable<Article>> GetThrendArticlesSearchAsync(string searchText, int? page, int? pageSize);
}
