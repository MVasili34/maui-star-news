using NewsMobileApp.ViewsNative;

namespace NewsMobileApp;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void OnSigninClicked(object sender, EventArgs e)
    {
#if WINDOWS
        Application.Current.MainPage = new AppShell();
#endif
#if ANDROID
        LoginBottomPage pageSheet = new();
        pageSheet.HasHandle = true;
        pageSheet.ShowAsync();
#endif
    }

    private async void Register_Tapped(object sender, TappedEventArgs e) =>
        await Navigation.PushModalAsync(new RegisterPage());
}