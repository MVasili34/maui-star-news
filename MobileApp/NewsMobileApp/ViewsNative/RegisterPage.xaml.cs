using NewsMobileApp.TempServices;
using System.Net.Mail;
using CryptographyTool;
using NewsMobileApp.ViewModels;

namespace NewsMobileApp.ViewsNative;

public partial class RegisterPage : ContentPage
{
    private RegisterViewModel viewModel => BindingContext as RegisterViewModel;
    private readonly IRequestsService _requestsService;

	public RegisterPage(IRequestsService requestsService)
	{
		InitializeComponent();
        _requestsService = requestsService;
        BindingContext = new RegisterViewModel();
    }

    private async void Register_Clicked(object sender, EventArgs e)
    {
        try
        {
            MailAddress mailAddress = new(viewModel.EmailAddress);
            if (string.IsNullOrWhiteSpace(viewModel.UserName) || viewModel.UserName.Length < 3)
                throw new ArgumentException("Недопустимый никнейм.");
            if (viewModel.Password != PasswordShow2.Text)
                throw new InvalidDataException("Пароли не совпадают.");
            if (!StrongPasswordChecker.PasswordCheck(viewModel.Password))
                throw new InvalidDataException("Слишком слабый пароль! Критерии:\n1) Не менее 8 симмволов;\n2) Одна прописная буква;" +
                    "\n3) Одна строчная буква;\n4) Одна цифра;\n5) Один специальный символ;\n6) Без пробелов;");
            BlockControls(false);
            UserViewModel result = await _requestsService.RegisterUserAsync(viewModel);

            if (result is null)
                throw new Exception("Ошибка сервера!");
            Preferences.Set("userId", result.UserId.ToString());
            Preferences.Set("userName", result.UserName);
            Preferences.Set("emailAddress", result.EmailAddress);
            Preferences.Set("phone", result.Phone);
            if (result.DateOfBirth is not null)
            {
                DateOnly current = result.DateOfBirth.Value;
                Preferences.Set("dateOfBirth", new DateTime(current.Year, current.Month, current.Day));
            }
            Preferences.Set("roleId", result.RoleId);
            Preferences.Set("password", PasswordShow1.Text);
            await Navigation.PopModalAsync();
        }
        catch (FormatException)
        {
            EmailSend.TextColor = Color.Parse("#ED1C24");
        }
        catch (ArgumentNullException)
        {
            await DisplayAlert("Ошибка!", "Для регистрации необходим действительный адрес электронной почты.", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ошибка!", ex.Message, "OK");
        }
        BlockControls(true);
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
        EmailSend.TextColor = Application.Current.PlatformAppTheme == AppTheme.Dark ? Color.Parse("#ffffff") : Color.Parse("#000000");

    private async void BackButton_Clicked(object sender, EventArgs e) => await Navigation.PopModalAsync();

    private void BlockControls(bool isBusy)
    {
        BackButton.IsEnabled = isBusy;
        UserName.IsReadOnly = !isBusy;
        EmailSend.IsReadOnly = !isBusy;
        PasswordShow1.IsReadOnly = !isBusy;
        PasswordShow2.IsReadOnly = !isBusy;
        Register.IsEnabled = isBusy;
        UserAgrrement.IsEnabled = isBusy;
    }
}