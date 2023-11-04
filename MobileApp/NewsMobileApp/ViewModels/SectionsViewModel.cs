using NewsMobileApp.Models;
using NewsMobileApp.TempServices;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NewsMobileApp.ViewModels;

public class SectionsViewModel
{

    private readonly INewsService _newsService;
    public ObservableCollection<Section> Sections { get; set; } = new();

    public SectionsViewModel()
    {
        _newsService = Application.Current.Handler
            .MauiContext.Services.GetService<INewsService>();
        SetSections();
    }

    public async void SetSections()
    {
        try
        {
            await Task.Delay(2000);
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
