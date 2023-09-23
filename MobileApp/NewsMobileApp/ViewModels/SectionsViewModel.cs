using NewsMobileApp.Models;
using NewsMobileApp.TempServices;

namespace NewsMobileApp.ViewModels;

public class SectionsViewModel
{
    public IEnumerable<Section> Sections { get; set; }

    public SectionsViewModel(INewsService newsService)
    {
        Sections = newsService.GetCategories();
    }
}
