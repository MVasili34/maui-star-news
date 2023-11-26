using NewsMobileApp.ViewsNative;
using NewsMobileApp.ViewModels;
using NewsMobileApp.TempServices;

namespace NewsMobileApp;

public partial class MainPage : ContentPage
{
    private IRequestsService _requestService;

    public MainPage(IServiceProvider service)
    {
        InitializeComponent();
        _requestService = service.GetService<IRequestsService>();
    }



    private async void OnSigninClicked(object sender, EventArgs e)
    {
        LoginBottomPage pageSheet = new(_requestService) {
            HasHandle = true
        };
        await pageSheet.ShowAsync();
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        string userId = Preferences.Get("userId", null);
        if (userId is null)
        {
            return;
        }
        try
        {
            AuthorizeViewModel authmodel = new()
            {
                EmailAddress = Preferences.Get("emailAddress", string.Empty),
                Password = Preferences.Get("password", string.Empty)
            };
            
            UserViewModel model = await _requestService.LoginUserAsync(authmodel) ?? 
                throw new Exception("Произошла ошибка сервера!");

            Preferences.Set("userId", model.UserId.ToString());
            Preferences.Set("userName", model.UserName);
            Preferences.Set("emailAddress", model.EmailAddress);
            Preferences.Set("phone", model.Phone);
            if (model.DateOfBirth.HasValue)
                Preferences.Set("dateOfBirth", model.DateOfBirth.Value.ToDateTime(new(0, 0, 0)));
            else
                Preferences.Set("dateOfBirth", null);
            Preferences.Set("password", authmodel.Password);
            Preferences.Set("roleId", model.RoleId);
            Application.Current.MainPage = new AppShell();
        }
        catch (Exception ex) 
        {
            await DisplayAlert("Ошибка!", ex.Message, "OK");
        }
    }

    private async void Register_Tapped(object sender, TappedEventArgs e) =>
         await Navigation.PushModalAsync(new RegisterPage(_requestService));
}