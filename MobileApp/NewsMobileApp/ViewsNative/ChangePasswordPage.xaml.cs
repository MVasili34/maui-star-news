using NewsMobileApp.TempServices;
using NewsMobileApp.ViewModels;
using CryptographyTool;
using NewsMobileApp.Models;

namespace NewsMobileApp.ViewsNative;

public partial class ChangePasswordPage : ContentPage
{
    private ChangePasswordUserViewModel viewModel => BindingContext as ChangePasswordUserViewModel;
    private readonly IRequestsService _requestService;

	public ChangePasswordPage()
	{
		InitializeComponent();
        BindingContext = new ChangePasswordUserViewModel {
            EmailAddress = Preferences.Get("emailAddress", "Error!")
        };
        _requestService = Application.Current.Handler
           .MauiContext.Services.GetService<IRequestsService>();
    }



    private async void Submit_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (viewModel.NewPassword1 != viewModel.NewPassword2)
                throw new Exception("Новые пароли разные!");
            if (!StrongPasswordChecker.PasswordCheck(viewModel.NewPassword1))
                throw new Exception("Слишком слабый пароль! Критерии:\n1) Не менее 8 симмволов;\n2) Одна прописная буква;" +
                    "\n3) Одна строчная буква;\n4) Одна цифра;\n5) Один специальный символ;\n6) Без пробелов;");
            AuthorizeModel signin = (AuthorizeModel)viewModel;
            AuthorizeModel changing = viewModel.GetNewAuthorizeModel();
            Submit.IsEnabled = false;

            if (!await _requestService.ChangeUserPasswordAsync(signin, changing))
                throw new Exception("Произошла ошибка сервера!");
            await DisplayAlert("Успех!", "Пароль изменён.", "OK");
            Preferences.Set("password", changing.Password);
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