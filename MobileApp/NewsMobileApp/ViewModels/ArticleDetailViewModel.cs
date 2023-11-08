using NewsMobileApp.TempServices;

namespace NewsMobileApp.ViewModels;

public class ArticleDetailViewModel
{
    private readonly INewsService _newsService;
    public ArticleViewModel Article { get; set; } = new();

    public ArticleDetailViewModel(INewsService newsService) => _newsService = newsService;
    
    public async Task InitializeArticle(Guid articleId)
    {
        await Task.Delay(3000);
    }
}
