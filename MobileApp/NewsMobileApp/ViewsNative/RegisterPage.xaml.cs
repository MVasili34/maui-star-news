using System.Net.Mail;

namespace NewsMobileApp.ViewsNative;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
	}
    private void Register_Clicked(object sender, EventArgs e)
    {
        try
        {
            MailAddress mailAddress = new(EmailSend.Text);
            Navigation.PopModalAsync();
            Application.Current.MainPage = new AppShell();
            
        }
        catch (FormatException)
        {
            EmailSend.TextColor = Color.Parse("#ED1C24");
        }
        catch (Exception)
        {

        }
    }

    private async void UsersAgreement_Tapped(object sender, TappedEventArgs e) => await
       Navigation.PushModalAsync(new TermsOfUseAgreePage(true));

    private void ShowPassword_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (showPassword.IsChecked)
        {
            PasswordShow1.IsPassword = false;
            PasswordShow2.IsPassword = false;
            return;
        }
        PasswordShow1.IsPassword = true;
        PasswordShow2.IsPassword = true;
    }

    private void EmailSend_TextChanged(object sender, TextChangedEventArgs e) =>
        EmailSend.TextColor = Color.Parse("#ffffff");

    private async void BackButton_Clicked(object sender, EventArgs e) => await Navigation.PopModalAsync();

}