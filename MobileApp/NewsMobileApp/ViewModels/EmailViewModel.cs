namespace NewsMobileApp.ViewModels;

public class EmailViewModel : ViewModelBase
{
    private readonly string _to = "vasili.dubov4@mail.ru";
    private string _subject;
    private string _body;

    public string To => _to;

    public string Subject
    {
        get => _subject;
        set => SetProperty(ref _subject, value);
    }

    public string Body
    {
        get => _body;
        set => SetProperty(ref _body, value);
    }
}
