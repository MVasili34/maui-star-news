namespace NewsMobileApp.ViewsNative;

public partial class ProfilePrivacyPage : ContentPage
{
	public ProfilePrivacyPage()
	{
		InitializeComponent();
	}

    private void Logout_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new MainPage();
    }

    private async void Privacy_Clicked(object sender, EventArgs e) =>
        await Navigation.PushAsync(new PrivacyPolicyAgreePage());
}