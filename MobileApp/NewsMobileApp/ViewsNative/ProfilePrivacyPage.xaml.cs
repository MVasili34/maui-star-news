using NewsMobileApp.ViewModels;

namespace NewsMobileApp.ViewsNative;

public partial class ProfilePrivacyPage : ContentPage
{
	public ProfilePrivacyPage()
	{
		InitializeComponent();

        UserName.Text = Preferences.Get("userName", "Error!");
        UserEmail.Text = Preferences.Get("emailAddress", "����������, ������� ������!");

        if (Preferences.Get("roleId", 1) == 3)
        {
            AdminSeparator.IsVisible = true;
            AdminPanel.IsVisible = true;
            AdmindButton.IsEnabled = true;
        }
	}

    private void Logout_Clicked(object sender, EventArgs e)
    {
        Preferences.Clear();
        Application.Current.MainPage = new MainPage(Handler
          .MauiContext.Services);
    }

    private async void Privacy_Clicked(object sender, EventArgs e) =>
        await Navigation.PushAsync(new PrivacyPolicyAgreePage());

    private async void TermsOfUse_Clicked(object sender, EventArgs e) =>
        await Navigation.PushAsync(new TermsOfUseAgreePage());

    private async void ProcessingData_Clicked(object sender, EventArgs e) =>
        await Navigation.PushAsync(new ProcessingDataAgreePage());

    private async void AccountSettings_Clicked(object sender, EventArgs e) =>
        await Navigation.PushAsync(new AccountSettingsPage(new UserViewModel()));

    private async void Support_Clicked(object sender, EventArgs e) => 
        await Navigation.PushAsync(new SupportPage(new EmailViewModel()));

    private async void AdmindButton_Clicked(object sender, EventArgs e) =>
        await Navigation.PushAsync(new AdminPage(new AdminViewModel()));

    private async void AppSettings_Clicked(object sender, EventArgs e) =>
        await Navigation.PushAsync(new AppSettingsPage());
}