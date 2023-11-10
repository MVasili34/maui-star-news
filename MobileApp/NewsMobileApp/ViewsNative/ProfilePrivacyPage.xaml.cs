using NewsMobileApp.ViewModels;

namespace NewsMobileApp.ViewsNative;

public partial class ProfilePrivacyPage : ContentPage
{
	public ProfilePrivacyPage()
	{
		InitializeComponent();

        UserName.Text = Preferences.Get("userName", "Error!");
        UserEmail.Text = Preferences.Get("emailAddress", "Пожалуйста, войдите заново!");

      //if (Preferences.Get("roleId", 1) == 3)
        if (Preferences.Get("roleId", 3) == 3)
        {
            AdminSeparator.IsVisible = true;
            AdminPanel.IsVisible = true;
            AdmindButton.IsEnabled = true;
        }
	}

    private void Logout_Clicked(object sender, EventArgs e)
    {
        Preferences.Set("userId", null);
        Preferences.Set("userName", null);
        Preferences.Set("emailAddress", null);
        Preferences.Set("phone", null);
        Preferences.Set("dateOfBirth", null);
        Preferences.Set("password", null);
        Preferences.Set("roleId", 3);
        Preferences.Set("registered", null);
        Application.Current.MainPage = new MainPage();
    }

    private async void Privacy_Clicked(object sender, EventArgs e) =>
        await Navigation.PushAsync(new PrivacyPolicyAgreePage());

    private async void TermsOfUse_Clicked(object sender, EventArgs e) =>
        await Navigation.PushAsync(new TermsOfUseAgreePage());

    private async void ProcessingData_Clicked(object sender, EventArgs e) =>
        await Navigation.PushAsync(new ProcessingDataAgreePage());

    private async void AccountSettings_Clicked(object sender, EventArgs e) =>
        await Navigation.PushAsync(new AccountSettingsPage());

    private async void Support_Clicked(object sender, EventArgs e) => 
        await Navigation.PushAsync(new SupportPage(new EmailViewModel()));

    private async void AdmindButton_Clicked(object sender, EventArgs e) =>
        await Navigation.PushAsync(new AdminPage(new AdminViewModel()));

    private async void AppSettings_Clicked(object sender, EventArgs e) =>
        await Navigation.PushAsync(new AppSettingsPage());
}