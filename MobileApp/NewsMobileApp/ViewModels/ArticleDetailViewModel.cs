using NewsMobileApp.Models;
using NewsMobileApp.TempServices;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ImagesCloudTool;
using System.Runtime.CompilerServices;

namespace NewsMobileApp.ViewModels;

public class ArticleDetailViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private readonly IRequestsService _requestService;
    private readonly IImageCloudTool _imageTool;

    private ArticleViewModel _article = new();
    public ArticleViewModel Article
    {
        get => _article;
        set
        {
            _article = value;
            NotifyPropertyChanged(nameof(Article));
        }
    }
    public ObservableCollection<Section> Sections { get; set; } = new();

    public ArticleDetailViewModel()
    {
        _requestService = Application.Current.Handler
           .MauiContext.Services.GetService<IRequestsService>();
        _imageTool = Application.Current.Handler
           .MauiContext.Services.GetService<IImageCloudTool>();
    }

    public async Task InitializeArticle(Guid? articleId)
    {
        foreach (Section section in await _requestService.GetAllSectionsAsync())
        {
            Sections.Add(section);
        }
        Article = await _requestService.GetArticleById(articleId);
    }

    public async Task InitializeArticle()
    {
        foreach (Section section in await _requestService.GetAllSectionsAsync())
        {
            Sections.Add(section);
        }
        _article.ArticleId = Guid.NewGuid();
    }

    public async Task PublishArticle()
    {
        Article.Image = await _imageTool.UploadImageAsync(Article.Image) ?? 
            throw new HttpRequestException("Ошибка запроса Imgur!");

        Article.PublishTime = DateTime.Now;
        if (!await _requestService.PublishArticleAsync(Article))
            throw new Exception("Произошла неизвестная ошибка сервера!");
    }

    public async Task ChangeArticle(bool changesRequired)
    {
        if (changesRequired)
        {
            Article.Image = await _imageTool.UploadImageAsync(Article.Image) ??
                throw new HttpRequestException("Ошибка запроса Imgur!");
        }
        if (!await _requestService.UpdateArticleAsync(Article))
            throw new Exception("Произошла неизвестная ошибка сервера!");
    }
}
