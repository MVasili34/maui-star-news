using System.Collections.ObjectModel;
using NewsMobileApp.TempServices;

namespace NewsMobileApp.ViewModels;

public class ArticlesBySectionViewModel
{
    private readonly INewsService _newsService;
    
    public ObservableCollection<ArticlePreviewViewModel> Articles { get; set; } = new();
    private readonly int _sectionId = 0;

    public ArticlesBySectionViewModel(int sectionId)
    {
        _newsService = Application.Current.Handler
           .MauiContext.Services.GetService<INewsService>();
        _sectionId = sectionId;
    }

    public async Task AddArticles(bool clear = false, int limit = 0, int offset = 0)
    {
        if (clear) Articles.Clear();

        await Task.Delay(3000);

        foreach (var article in _newsService.GetThrendArticlesPreview()
                                            .Where(a => a.Section.SectionId == _sectionId))
        {
            Articles.Add(article);
        }
    }

    public async Task SearchArticle(string text, int limit = 20, int offset = 0)
    {
        await Task.Delay(3000);
        IEnumerable<ArticlePreviewViewModel> found = _newsService.GetThrendArticlesPreview()
                    .Where(x => x.Title.Contains(text) 
                    && x.Section.SectionId == _sectionId)
                    .Skip(limit*offset)
                    .Take(limit);
        if (found.Any())
        {
            foreach (var article in found)
            {
                Articles.Add(article);
            }
        }
    }
}
