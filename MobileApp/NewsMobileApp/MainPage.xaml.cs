using NewsMobileApp.ViewsNative;
using NewsMobileApp.Models;
using NewsMobileApp.TempServices;

namespace NewsMobileApp;

public partial class MainPage : ContentPage
{
    private bool _status = false;
    private IRequestsService _newsService;

    public MainPage()
    {
        InitializeComponent();
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        _newsService = Handler
          .MauiContext.Services.GetService<IRequestsService>();
    }

    private async void OnSigninClicked(object sender, EventArgs e)
    {
#if WINDOWS
        Application.Current.MainPage = new AppShell();
#endif
#if ANDROID
        LoginBottomPage pageSheet = new(_newsService);
        pageSheet.HasHandle = true;
        await pageSheet.ShowAsync();
#endif
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
            BlockUIButtons();
            
            await Task.Delay(3000);
            Application.Current.MainPage = new AppShell();
        }
        catch (Exception ex) 
        {
            await DisplayAlert("Ошибка!", ex.Message, "OK");
        }
        _status = true;
        BlockUIButtons();
    }

    private async void Register_Tapped(object sender, TappedEventArgs e) =>
         await Navigation.PushModalAsync(new RegisterPage(_newsService));

    private void BlockUIButtons()
    {
        LoginBtn.IsEnabled = _status;
        RegisterButton.IsEnabled = _status;
    }
}