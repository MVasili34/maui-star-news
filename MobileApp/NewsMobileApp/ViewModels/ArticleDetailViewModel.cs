using NewsMobileApp.Models;
using NewsMobileApp.TempServices;
using System.Collections.ObjectModel;
using ImagesCloudTool;

namespace NewsMobileApp.ViewModels;

public class ArticleDetailViewModel : ViewModelBase
{
    private readonly IRequestsService _requestService;
    private readonly IImageCloudTool _imageTool;
    public ObservableCollection<Section> Sections { get; set; } = new();

    private Section _selectedSection = new();
    private ArticleViewModel _article = new();

    public ArticleViewModel Article
    {
        get => _article;
        set => SetProperty(ref _article, value);
    }

    public Section SelectedSection
    {
        get => _selectedSection;
        set => SetProperty(ref _selectedSection, value);
    }

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
        Article = await _requestService.GetArticleByIdAsync(articleId);
        SelectedSection = Sections.FirstOrDefault(s => s.SectionId == Article.SectionId);
    }

    public async Task InitializeArticle()
    {
        foreach (Section section in await _requestService.GetAllSectionsAsync())
        {
            Sections.Add(section);
        }
        SelectedSection = Sections.First();
        _article.ArticleId = Guid.NewGuid();
    }

    public async Task PublishArticle()
    {
        if (string.IsNullOrWhiteSpace(Article.Title) ||
            Article.Title.Length > 100)
        {
            throw new Exception("Либо слишком короткое название статьи либо слишком длинное!");
        }
        if (string.IsNullOrWhiteSpace(Article.Subtitle) ||
            Article.Subtitle.Length > 100)
        {
            throw new Exception("Либо слишком короткое описание статьи либо слишком длинное!");
        }
        if (string.IsNullOrWhiteSpace(Article.Text))
        {
            throw new Exception("Текст статьи пустой!");
        }
        if (Article.Image is null || !new FileInfo(Article.Image).Exists)
        {
            throw new FileNotFoundException();
        }
        if (Preferences.Get("userId", null) is null)
        {
            throw new Exception("Некорректный автор статьи!");
        }

        Article.PublisherId = new Guid(Preferences.Get("userId", null));
        Article.SectionId = SelectedSection.SectionId;
        Article.Image = await _imageTool.UploadImageAsync(Article.Image) ?? 
            throw new HttpRequestException("Ошибка запроса Imgur!");

        Article.PublishTime = DateTime.Now;
        if (!await _requestService.PublishArticleAsync(Article))
            throw new Exception("Произошла неизвестная ошибка сервера!");
    }

    public async Task ChangeArticle(bool changesRequired)
    {
        if (string.IsNullOrWhiteSpace(Article.Title) ||
            Article.Title.Length > 100)
        {
            throw new Exception("Либо слишком короткое название статьи либо слишком длинное!");
        }
        if (string.IsNullOrWhiteSpace(Article.Subtitle) ||
            Article.Subtitle.Length > 100)
        {
            throw new Exception("Либо слишком короткое описание статьи либо слишком длинное!");
        }
        if (string.IsNullOrWhiteSpace(Article.Text))
        {
            throw new Exception("Текст статьи пустой!");
        }
        if (Article.Image is null)
        {
            throw new FileNotFoundException();
        }
        if (changesRequired && !new FileInfo(Article.Image).Exists)
        {
            throw new FileNotFoundException();
        }

        Article.SectionId = SelectedSection.SectionId;
        if (changesRequired)
        {
            Article.Image = await _imageTool.UploadImageAsync(Article.Image) ??
                throw new HttpRequestException("Ошибка запроса Imgur!");
        }
        if (!await _requestService.UpdateArticleAsync(Article))
            throw new Exception("Произошла неизвестная ошибка сервера!");
    }
}
