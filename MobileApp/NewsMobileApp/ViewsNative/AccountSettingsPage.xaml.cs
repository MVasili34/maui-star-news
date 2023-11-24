using NewsMobileApp.TempServices;
using NewsMobileApp.ViewModels;
using System.Text.RegularExpressions;

namespace NewsMobileApp.ViewsNative;

public partial class AccountSettingsPage : ContentPage
{
	private UserViewModel viewModel => BindingContext as UserViewModel;
    private readonly IRequestsService _requestsService;

	public AccountSettingsPage(UserViewModel _viewModel)
	{
		InitializeComponent();
        _requestsService = Application.Current.Handler
           .MauiContext.Services.GetService<IRequestsService>();
        BindingContext = _viewModel;

    }

    protected override void OnAppearing()
    {
        viewModel.UserId = new Guid(Preferences.Get("userId", Guid.NewGuid().ToString()));
        viewModel.UserName = Preferences.Get("userName", "Error!");
        viewModel.EmailAddress = Preferences.Get("emailAddress", null);
        DatePicker.Date = Preferences.Get("dateOfBirth", DateTime.Now.AddYears(-6));
        viewModel.Phone = Preferences.Get("phone", null);
        viewModel.PasswordSalt = string.Empty;
        viewModel.PasswordHash = string.Empty;
        viewModel.RoleId = Preferences.Get("roleId", 1);
        viewModel.Registered = Preferences.Get("registered", DateTime.Now);
        base.OnAppearing();
        DatePicker.MaximumDate = DateTime.Now.AddYears(-5);
    }

    private async void Submit_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(viewModel.UserName) || viewModel.UserName.Length < 3)
                throw new InvalidDataException("Недопустимый никнейм.");
            if (viewModel.EmailAddress is null)
                throw new Exception("Произошла ошибка!\nПожалуйста, выполните повторный вход.");
            if (!string.IsNullOrEmpty(viewModel.Phone) && 
                !Regex.IsMatch(viewModel.Phone, @"^(373)?0?[6-9]{3}[0-9]{5}$"))
                throw new InvalidDataException("Неверный номер телефона.");
            string password = await Shell.Current.DisplayPromptAsync("Пароль", "Для изменения данных введите пароль:",
                accept: "OK", cancel: "Отмена");
            if (string.IsNullOrWhiteSpace(password))
            {
                return;
            }
            viewModel.PasswordHash = password;
            viewModel.DateOfBirth = DateOnly.FromDateTime(DatePicker.Date);
            Submit.IsEnabled = false;

            if(!await _requestsService.UpdateUserAsync(viewModel))
            {
                await DisplayAlert("Ошибка!", "Данные не были изменены.", "OK");
                return;
            }

            Preferences.Set("userName", viewModel.UserName);
            Preferences.Set("phone", viewModel.Phone);
            if (viewModel.DateOfBirth.HasValue)
                Preferences.Set("dateOfBirth", viewModel.DateOfBirth.Value.ToDateTime(new(0, 0, 0)));
            else
                Preferences.Set("dateOfBirth", null);

            await DisplayAlert("Успех!", "Данные успешно изменены.", "OK");
            await Navigation.PopAsync(animated: true);
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

    private async void ChangePswButton_Clicked(object sender, EventArgs e) =>
        await Navigation.PushAsync(new ChangePasswordPage());
}