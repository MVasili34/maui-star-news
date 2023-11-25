using System.Collections.ObjectModel;
using NewsMobileApp.Models;
using NewsMobileApp.TempServices;

namespace NewsMobileApp.ViewModels;

public class ArticlesBySectionViewModel
{
    private readonly IRequestsService _requestService;

    public ObservableCollection<Article> Articles { get; set; } = new();
    private readonly int _sectionId = 0;
    private bool _searching = false;
    private string _currentSearch = string.Empty;
    private const int _limit = 10;
    private int _offset = 0;

    public ArticlesBySectionViewModel(int sectionId)
    {
        _requestService = Application.Current.Handler
           .MauiContext.Services.GetService<IRequestsService>();
        _sectionId = sectionId;
    }

    public async Task AddArticles(bool clear = false)
    {
        if (clear)
        {
            Articles.Clear();
            _offset = 0;
        }

        try
        {
            IEnumerable<Article> current;
            if (!_searching)
            {
                current = await _requestService.GetArticlesBySectionAsync(_sectionId, _offset, _limit);
            }
            else
            {
                current = await _requestService.GetArticlesSectionSearchAsync(_currentSearch, _sectionId, _offset, _limit);
            }
            _offset++;
            foreach (var article in current)
            {
                Articles.Add(article);
            }
        }
        catch (Exception ex)
        {
            await App.Current.MainPage.DisplayAlert("Ошибка!", ex.Message, "OK");
        }
    }

    public void SearchArticle(string text)
    {
        _searching = true;
        _offset = 0;
        _currentSearch = text;
    }

    public void RefreshCollection()
    {
        _searching = false;
        _offset = 0;
        _currentSearch = string.Empty;
    }
}
