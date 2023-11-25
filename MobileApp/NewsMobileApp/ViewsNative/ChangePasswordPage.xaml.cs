using NewsMobileApp.TempServices;
using NewsMobileApp.ViewModels;
using CryptographyTool;

namespace NewsMobileApp.ViewsNative;

public partial class ChangePasswordPage : ContentPage
{
    private ChangePasswordViewModel viewModel => BindingContext as ChangePasswordViewModel;
    private readonly IRequestsService _requestService;

	public ChangePasswordPage()
	{
		InitializeComponent();
        BindingContext = new ChangePasswordViewModel {
            EmailAddress = Preferences.Get("emailAddress", "Error!")
        };
        _requestService = Application.Current.Handler
           .MauiContext.Services.GetService<IRequestsService>();
    }



    private async void Submit_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (viewModel.NewPassword != viewModel.NewPasswordVerify)
                throw new Exception("Новые пароли разные!");
            if (!StrongPasswordChecker.PasswordCheck(viewModel.NewPassword))
                throw new Exception("Слишком слабый пароль! Критерии:\n1) Не менее 8 симмволов;\n2) Одна прописная буква;" +
                    "\n3) Одна строчная буква;\n4) Одна цифра;\n5) Один специальный символ;\n6) Без пробелов;");
            Submit.IsEnabled = false;
            if (!await _requestService.ChangeUserPasswordAsync(viewModel))
                throw new Exception("Произошла ошибка сервера!");
            await DisplayAlert("Успех!", "Пароль изменён.", "OK");
            Preferences.Set("password", viewModel.NewPassword);
            await Navigation.PopAsync();
        }
        catch (HttpRequestException ex)
        {
            await DisplayAlert("Ошибка!", ex.Message, "OK");
        }
        catch (Exception ex) 
        {
            await DisplayAlert("Ошибка!", ex.Message, "OK");
        }
        Submit.IsEnabled = true;
    }

    private void ShowPassword_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (ShowPassword.IsChecked)
        {
            NewPassword1.IsPassword = false;
            NewPassword2.IsPassword = false;
            OldPassword.IsPassword = false;
            return;
        }
        NewPassword1.IsPassword = true;
        NewPassword2.IsPassword = true;
        OldPassword.IsPassword = true;
    }
}