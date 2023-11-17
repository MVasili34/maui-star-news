using NewsMobileApp.Models;
using NewsMobileApp.TempServices;
using System.Collections.ObjectModel;

namespace NewsMobileApp.ViewModels;

public class SectionViewModel
{
   
    private readonly INewsService _newsService;
    public ObservableCollection<Section> Sections { get; set; } = new();

    public SectionViewModel(INewsService newsService) => _newsService = newsService;

    public async Task SetSections()
    {
        try
        {
            await Task.Delay(3000);
            foreach (var section in _newsService.GetCategories())
            {
                Sections.Add(section);
            }
        }
        catch (Exception ex) 
        {
            await Shell.Current.DisplayAlert("Alert", ex.Message, "OK");
        }
    }
    
    /*
    private readonly IRequestsService _newsService;
    public ObservableCollection<Section> Sections { get; set; } = new();

    public SectionViewModel(IRequestsService newsService) => _newsService = newsService;

    public async Task SetSections()
    {
        foreach (var section in await _newsService.GetAllSectionsAsync())
        {
            Sections.Add(section);
        }
    }
    */
}
