using NewsMobileApp.ViewsNative;

namespace NewsMobileApp;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnSigninClicked(object sender, EventArgs e)
    {
#if WINDOWS
        Application.Current.MainPage = new AppShell();
#endif
#if ANDROID
        LoginBottomPage pageSheet = new();
        pageSheet.HasHandle = true;
        await pageSheet.ShowAsync();
#endif
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
    }
}