namespace NewsMobileApp.ViewsNative;

public partial class AppSettingsPage : ContentPage
{
	public AppSettingsPage()
	{
		InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        TextBox.Text = await SecureStorage.GetAsync("AIKey");
    }

    private async void Submit_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(TextBox.Text))
            {
                return;
            }
            await SecureStorage.SetAsync("AIKey", TextBox.Text);
            await DisplayAlert("Успех!", "Настройки успешно установлены", "OK");
            await Navigation.PopAsync(true);
        }
        catch (Exception ex) 
        {
            await DisplayAlert("Ошибка!", ex.Message, "OK");
        }
    }
}