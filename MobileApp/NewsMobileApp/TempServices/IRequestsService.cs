using NewsMobileApp.Models;

namespace NewsMobileApp.TempServices;

public interface IRequestsService
{
    public Task<IEnumerable<Section>> GetAllSectionsAsync();
}
