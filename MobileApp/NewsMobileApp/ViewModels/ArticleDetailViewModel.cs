using NewsMobileApp.Models;
using NewsMobileApp.TempServices;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ImagesCloudTool;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using static SkiaSharp.HarfBuzz.SKShaper;

namespace NewsMobileApp.ViewModels;

public class ArticleDetailViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private readonly INewsService _newsService;
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
        _newsService = Application.Current.Handler
           .MauiContext.Services.GetService<INewsService>();
        _imageTool = Application.Current.Handler
           .MauiContext.Services.GetService<IImageCloudTool>();
    }

    public async Task InitializeArticle(Guid? articleId)
    {
        
        await Task.Delay(3000);

        foreach (Section section in _newsService.GetCategories())
        {
            Sections.Add(section);
        }
        Article = _newsService.GetThrendArticlesFull().FirstOrDefault(x => x.ArticleId == articleId);
    }

    public async Task InitializeArticle()
    {
        await Task.Delay(3000);
        foreach (Section section in _newsService.GetCategories())
        {
            Sections.Add(section);
        }
        _article.ArticleId = Guid.NewGuid();
    }

    public async Task PublishArticle()
    {
        //string urlImage = await _imageTool.UploadImageAsync(Article.Image);
        string urlImage = Article.Image;
        if (urlImage is null)
            throw new HttpRequestException("Ошибка запроса Imgur!");

        Article.PublishTime = DateTime.Now;

        string serialized = JsonConvert.SerializeObject(Article);
        await Shell.Current.DisplayAlert("Success", $"{serialized}", "OK");
    }

    public async Task ChangeArticle(bool changesRequired)
    {
        if (changesRequired)
        {
            //string urlImage = await _imageTool.UploadImageAsync(Article.Image);
            string urlImage = Article.Image;
            if (urlImage is null)
                throw new HttpRequestException("Ошибка запроса Imgur!");
        }

        string serialized = JsonConvert.SerializeObject(Article);
        await Shell.Current.DisplayAlert("Success", $"{serialized}", "OK");
    }
}
