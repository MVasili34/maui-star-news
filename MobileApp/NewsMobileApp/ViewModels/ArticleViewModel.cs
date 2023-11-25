namespace NewsMobileApp.ViewModels;

public class ArticleViewModel : ViewModelBase
{
    private Guid _articleId;
    private string _title;
    private string _subtitle;
    private int _sectionId;
    private string _image;
    private int _views;
    private string _text;
    private DateTime? _publishTime = null;
    private Guid? _publisherId;

    public Guid ArticleId
    {
        get => _articleId;
        set => SetProperty(ref _articleId, value);
    }

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public string Subtitle
    {
        get => _subtitle;
        set => SetProperty(ref _subtitle, value);
    }

    public int SectionId
    {
        get => _sectionId;
        set => SetProperty(ref _sectionId, value);
    }

    public string Image
    {
        get => _image;
        set => SetProperty(ref _image, value);
    }

    public int Views
    {
        get => _views;
        set => SetProperty(ref _views, value);
    }

    public string Text
    {
        get => _text;
        set => SetProperty(ref _text, value);
    }

    public DateTime? PublishTime
    {
        get => _publishTime;
        set => SetProperty(ref _publishTime, value);
    }

    public Guid? PublisherId
    {
        get => _publisherId;
        set => _publisherId = value;
    }
}
