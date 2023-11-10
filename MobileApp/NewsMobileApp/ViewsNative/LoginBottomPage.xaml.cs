using System.Net.Mail;

namespace NewsMobileApp.ViewsNative;

public partial class LoginBottomPage
{
	public LoginBottomPage()
	{
		InitializeComponent();
        EmailSend.Text = Preferences.Get("emailAddress", string.Empty);
        PasswordShow.Text = Preferences.Get("password", string.Empty);
    }

    private async void Login_Clicked(object sender, EventArgs e)
    {
        try
        {
            //MailAddress mailAddress = new(EmailSend.Text);
            await DismissAsync();
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

    private async void RegisterClicked_Tapped(object sender, TappedEventArgs e)
    {
        await DismissAsync();
        await Navigation.PushModalAsync(new RegisterPage());
    }

    private void ShowPassword_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (showPassword.IsChecked)
        {
            PasswordShow.IsPassword = false;
            return;
        }
        PasswordShow.IsPassword = true;
    }

    private void PasswordShow_TextChanged(object sender, TextChangedEventArgs e)
    {
        EmailSend.TextColor = Color.Parse("#ffffff");
    }
}