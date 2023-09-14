using System.Net.Mail;

namespace NewsMobileApp.ViewsNative;

public partial class LoginBottomPage
{
	public LoginBottomPage()
	{
		InitializeComponent();
	}

    private async void Login_Clicked(object sender, EventArgs e)
    {
        try
        {
            MailAddress mailAddress = new(EmailSend.Text);
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
        //RegisterPage registerSheet = new();
        //registerSheet.HandleColor = Color.Parse("#FFFFFF");
        //registerSheet.HasHandle = true;
        //await registerSheet.ShowAsync();
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