using NewsMobileApp.TempServices;
using System.Net.Mail;
using CryptographyTool;
using NewsMobileApp.ViewModels;

namespace NewsMobileApp.ViewsNative;

public partial class LoginBottomPage
{
    private AuthorizeViewModel viewModel => BindingContext as AuthorizeViewModel;
    private readonly IRequestsService _requestService;

	public LoginBottomPage(IRequestsService requestService)
	{
		InitializeComponent();
  
        _requestService = requestService;
        BindingContext = new AuthorizeViewModel()
        {
            EmailAddress = Preferences.Get("emailAddress", string.Empty),
            Password = Preferences.Get("password", string.Empty)
        };
    }

    private async void Login_Clicked(object sender, EventArgs e)
    {
        //await DismissAsync();
        //Application.Current.MainPage = new AppShell();
        try
        {
            MailAddress mailAddress = new(viewModel.EmailAddress);
            if (!StrongPasswordChecker.PasswordCheck(viewModel.Password))
                throw new InvalidDataException("Слишком слабый пароль! Критерии:\n1) Не менее 8 симмволов;\n2) Одна прописная буква;" +
                    "\n3) Одна строчная буква;\n4) Одна цифра;\n5) Один специальный символ;\n6) Без пробелов;");
            BlockControls(false);
            UserViewModel model = await _requestService.LoginUserAsync(viewModel);
            if (model is null)
                throw new Exception("Произошла ошибка сервера!");
            Preferences.Set("userId", model.UserId.ToString());
            Preferences.Set("userName", model.UserName);
            Preferences.Set("emailAddress", model.EmailAddress);
            Preferences.Set("phone", model.Phone);
            if (model.DateOfBirth.HasValue)
                Preferences.Set("dateOfBirth", model.DateOfBirth.Value.ToDateTime(new(0,0,0)));
            else
                Preferences.Set("dateOfBirth", null);
            Preferences.Set("password", viewModel.Password);
            Preferences.Set("roleId", model.RoleId);
            Application.Current.MainPage = new AppShell();
            await DismissAsync();
        }
        catch (FormatException)
        {
            EmailSend.TextColor = Color.Parse("#ED1C24");
        }
        catch (ArgumentException)
        {
            await Application.Current.MainPage.DisplayAlert("Ошибка!", "Для авторизации необходим действительный адрес электронной почты.", "OK");
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Ошибка!", ex.Message, "OK");
        }
        BlockControls(true);
    }

    private async void RegisterClicked_Tapped(object sender, TappedEventArgs e)
    {
        await DismissAsync();
        await Navigation.PushModalAsync(new RegisterPage(_requestService));
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

    private void EmailShow_TextChanged(object sender, TextChangedEventArgs e) => 
        EmailSend.TextColor = Application.Current.PlatformAppTheme == AppTheme.Dark ? Color.Parse("#ffffff") : Color.Parse("#000000");

    private void BlockControls(bool isBusy)
    {
        IsCancelable = isBusy;
        EmailSend.IsReadOnly = !isBusy;
        PasswordShow.IsReadOnly = !isBusy;
        Login.IsEnabled = isBusy;
        RegisterButton.IsEnabled = isBusy;
    }
}