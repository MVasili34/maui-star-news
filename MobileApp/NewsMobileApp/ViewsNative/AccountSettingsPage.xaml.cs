using NewsMobileApp.ViewModels;
using Newtonsoft.Json;

namespace NewsMobileApp.ViewsNative;

public partial class AccountSettingsPage : ContentPage
{
	public AccountSettingsPage()
	{
		InitializeComponent();
		UserViewModel viewModel = new()
		{
			UserId = Guid.NewGuid(),
			UserName = "Test",
			EmailAddress = "Test@gmail.com",
			DateOfBirth = new(2000, 12, 13),
			Phone = null,
            PasswordSalt = "salt",
            PasswordHash = "somejey",
		};
        BindingContext = viewModel;
        PasswordField.Text = string.Empty;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        DatePicker.MaximumDate = DateTime.Now.AddYears(-5);
		DatePicker.MinimumDate = new(1900, 1, 1);
    }

    private async void Submit_Clicked(object sender, EventArgs e)
    {
		UserViewModel result = (UserViewModel)BindingContext;
		string serialized = JsonConvert.SerializeObject(result);
		await DisplayAlert("Success", $"{serialized}", "OK");
		await Navigation.PopAsync(animated: true);
    }
}