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
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {

    }
}