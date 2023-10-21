using NewsMobileApp.Views;
using NewsMobileApp.ViewModels;

namespace NewsMobileApp.ViewsNative;

public partial class ProfilePrivacyPage : ContentPage
{
	public ProfilePrivacyPage()
	{
		InitializeComponent();
        AdminSeparator.IsVisible = true;
        AdminPanel.IsVisible = true;
	}

    private void Logout_Clicked(object sender, EventArgs e)
    {
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