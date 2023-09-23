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
}