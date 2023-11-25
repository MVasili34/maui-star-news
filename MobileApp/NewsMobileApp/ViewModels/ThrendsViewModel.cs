using NewsMobileApp.TempServices;
using NewsMobileApp.Models;
using System.Collections.ObjectModel;

namespace NewsMobileApp.ViewModels;

public class ThrendsViewModel
{
    private readonly IRequestsService _requestService;
    private bool _searching = false;
    private string _currentSearch = string.Empty;
    private const int _limit = 10;
    private int _offset = 0;

    public ObservableCollection<Article> Articles { get; set; } = new();

    public ThrendsViewModel(IRequestsService requestService) => _requestService = requestService;

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
                current = await _requestService.GetThrendArticlesAsync(_offset, _limit);
            }
            else
            {
                current = await _requestService.GetThrendArticlesSearchAsync(_currentSearch, _offset, _limit);
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
