using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NewsMobileApp.ViewModels;

public class EmailViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private string _to { get; set; } = "vasili.dubov4@mail.ru";
    private string _subject { get; set; }
    private string _body { get; set; }

    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    public string To => _to;

    public string Subject
    {
        get => _subject;
        set
        {
            _subject = value;
            NotifyPropertyChanged(nameof(Subject));
        }
    }

    public string Body
    {
        get => _body;
        set
        {
            _body = value;
            NotifyPropertyChanged(nameof(Body));
        }
    }
}
