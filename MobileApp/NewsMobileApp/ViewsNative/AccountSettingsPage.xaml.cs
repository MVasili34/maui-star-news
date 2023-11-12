using NewsMobileApp.TempServices;
using NewsMobileApp.ViewModels;
using Newtonsoft.Json;

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
            //if (viewModel.EmailAddress is null)
                //throw new Exception("Произошла ошибка!\nПожалуйста, выполните повторный вход.");
            viewModel.DateOfBirth = DateOnly.FromDateTime(DatePicker.Date);
            Submit.IsEnabled = false;
            await Task.Delay(5000);
            string serialized = JsonConvert.SerializeObject(viewModel);
            await DisplayAlert("Success", $"{serialized}", "OK");
            await Navigation.PopAsync(animated: true);
        }
        catch(Exception ex) 
        {
            await DisplayAlert("Ошибка!", ex.Message, "OK");
        }
        Submit.IsEnabled = true;
    }

    private async void ChangePswd_Clicked(object sender, EventArgs e) =>
        await Navigation.PushAsync(new ChangePasswordPage());
}