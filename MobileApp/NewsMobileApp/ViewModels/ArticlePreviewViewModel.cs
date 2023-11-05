using NewsMobileApp.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NewsMobileApp.ViewModels;

public class ArticlePreviewViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private Guid _articleId;
    private string _title;
    private string _subtitle;
    private Models.Section _section;
    private string _image;
    private DateTime _publishTime;

    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    public Guid ArticleId
    {
        get => _articleId;
        set => _articleId = value;
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

    public Models.Section Section
    {
        get => _section;
        set
        {
            _section = value;
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

    public DateTime PublishTime
    {
        get => _publishTime;
        set => _publishTime = value;
    }
}
