namespace NewsMobileApp.ViewsNative;

public partial class SupportPage : ContentPage
{

	public SupportPage()
	{
		InitializeComponent();
        ComboBox1.ItemsSource = new[] {
            "Сообщить об ошибке",
            "Проблемы с учётной записью",
            "Вопросы сотруднечества",
            "Другое"
        };
        ComboBox1.SelectedIndex = 0;
    }

    private async void Submit_Clicked(object sender, EventArgs e) =>
        await DisplayAlert("Отправлено", "Ваш запрос отправлен нашей службе поддержки. Ожидайте ответа!", "OK");
}