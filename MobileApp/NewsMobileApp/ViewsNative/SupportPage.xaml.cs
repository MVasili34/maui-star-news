using NewsMobileApp.ViewModels;

namespace NewsMobileApp.ViewsNative;

public partial class SupportPage : ContentPage
{
    private readonly EmailViewModel _emailViewModel;
    private readonly string[] Themes = new[] {
            "Сообщить об ошибке",
            "Проблемы с учётной записью",
            "Вопросы сотруднечества",
            "Другое"
    };

    public SupportPage(EmailViewModel emailViewModel)
	{
		InitializeComponent();
        ComboBox1.ItemsSource = Themes;
        ComboBox1.SelectedIndex = 0;
        _emailViewModel = emailViewModel;
        BindingContext = _emailViewModel;
    }

    private async void Submit_Clicked(object sender, EventArgs e)
    {
        try
        {
            _emailViewModel.Subject = Themes[ComboBox1.SelectedIndex];
            if (string.IsNullOrEmpty(_emailViewModel.Body))
            {
                await DisplayAlert("Ошибка", $"Пустое сообщение письма", "OK");
                return;
            }

            EmailMessage message = new()
            {
                To = new List<string>(_emailViewModel.To.Split(";")),
                Subject = _emailViewModel.Subject,
                Body = _emailViewModel.Body ,
            };

            message.Body += $"\n\n--\nОбращение пользователя: {Preferences.Get("userId", Guid.NewGuid().ToString())}" +
                $"\n Идентификатор обращения: {Guid.NewGuid()}";


            //await DisplayAlert("Отправлено", $"Ваш запрос отправлен нашей службе поддержки. {_emailViewModel.Body}", "OK");
            await Email.Default.ComposeAsync(message);
        }
        catch (FeatureNotSupportedException ex)
        {
            await DisplayAlert("Ошибка", $"Разрешение не предоставлено: {ex.Message}", "OK");
        }
        catch (Exception ex) 
        {
            await DisplayAlert("Ошибка", $"Ошибка: {ex.Message}", "OK");
        }
    }
}