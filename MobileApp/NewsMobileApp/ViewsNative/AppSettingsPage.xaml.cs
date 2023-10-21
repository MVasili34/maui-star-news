namespace NewsMobileApp.ViewsNative;

public partial class AppSettingsPage : ContentPage
{
	public AppSettingsPage()
	{
		InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        TextBox.Text = Preferences.Get("apiKey", string.Empty);
    }

    private async void Submit_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(TextBox.Text))
            {
                return;
            }
            Preferences.Set("apiKey", TextBox.Text);
            await DisplayAlert("Успех!", "Настройки успешно установлены", "OK");
            await Navigation.PopAsync(true);
        }
        catch (Exception ex) 
        {
            await DisplayAlert("Ошибка!", ex.Message, "OK");
        }
    }
}