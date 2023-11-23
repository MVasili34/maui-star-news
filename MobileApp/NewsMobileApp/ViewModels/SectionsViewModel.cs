using NewsMobileApp.Models;
using NewsMobileApp.TempServices;
using System.Collections.ObjectModel;

namespace NewsMobileApp.ViewModels;

public class SectionViewModel
{   
    private readonly IRequestsService _newsService;
    public ObservableCollection<Section> Sections { get; set; } = new();

    public SectionViewModel(IRequestsService newsService) => _newsService = newsService;

    public async Task SetSections(bool update = false)
    {
        if (update) Sections.Clear();

        foreach (var section in await _newsService.GetAllSectionsAsync())
        {
            Sections.Add(section);
        }
    }
}
