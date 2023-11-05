using NewsMobileApp.Models;
using NewsMobileApp.TempServices;
using System.Collections.ObjectModel;

namespace NewsMobileApp.ViewModels;

public class SectionViewModel
{

    private readonly INewsService _newsService;
    public ObservableCollection<Section> Sections { get; set; } = new();

    public SectionViewModel()
    {
        _newsService = Application.Current.Handler
            .MauiContext.Services.GetService<INewsService>();
        SetSections();
    }

    public async void SetSections()
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
}
