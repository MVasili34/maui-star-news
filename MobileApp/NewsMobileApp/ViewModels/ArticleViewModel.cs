using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NewsMobileApp.ViewModels;

public class ArticleViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private Guid _articleId;
    private string _title;
    private string _subtitle;
    private int _sectionId;
    private string _image;
    private int _views;
    private string _text;
    private DateTime? _publishTime = null;
    private Guid? _publisherId;

    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    public Guid ArticleId
    {
        get => _articleId;
        set
        {
            _articleId = value;
            NotifyPropertyChanged(nameof(ArticleId));
        }
    }

    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            NotifyPropertyChanged();
        }
    }

    public string Subtitle
    {
        get => _subtitle;
        set
        {
            _subtitle = value;
            NotifyPropertyChanged();
        }
    }

    public int SectionId
    {
        get => _sectionId;
        set
        {
            _sectionId = value;
            NotifyPropertyChanged();
        }
    }

    public string Image
    {
        get => _image;
        set
        {
            _image = value;
            NotifyPropertyChanged();
        }
    }

    public int Views
    {
        get => _views;
        set
        {
            _views = value;
            NotifyPropertyChanged();
        }
    }

    public string Text
    {
        get => _text;
        set
        {
            _text = value;
            NotifyPropertyChanged();
        }
    }

    public DateTime? PublishTime
    {
        get => _publishTime;
        set
        {
            _publishTime = value;
            NotifyPropertyChanged();
        }

    }

    public Guid? PublisherId
    {
        get => _publisherId;
        init => _publisherId = value;
    }
}
