using System.Collections.ObjectModel;
using NewsMobileApp.TempServices;

namespace NewsMobileApp.ViewModels;

public class ArticlesBySectionViewModel: ObservableCollection<ArticlePreviewViewModel>
{
    private readonly INewsService _newsService;

    public ArticlesBySectionViewModel() => _newsService = Application.Current.Handler.MauiContext.Services.GetService<INewsService>();

    public void AddArticles(int _sectionId, bool clear = false, int limit = 0, int offset = 0)
    {
        if (clear) Clear();

        foreach (var article in _newsService.GetThrendArticlesPreview().Where(a => a.Section.SectionId == _sectionId))
        {
            Add(article);
        }
    }

    public void SearchArticle(int _sectionId, string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            AddArticles(_sectionId, true);
            return;
        }

        Clear();
        IEnumerable<ArticlePreviewViewModel> found = _newsService.GetThrendArticlesPreview()
                                                                 .Where(x => x.Title.Contains(text) 
                                                                 && x.Section.SectionId == _sectionId)
                                                                 .Take(10);

        if (found.Any())
        {
            foreach (var article in found)
            {
                Add(article);
            }
        }
    }
}
