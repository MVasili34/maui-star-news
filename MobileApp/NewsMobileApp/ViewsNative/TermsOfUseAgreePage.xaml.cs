namespace NewsMobileApp.ViewsNative;

public partial class TermsOfUseAgreePage : ContentPage
{
	public TermsOfUseAgreePage(bool addBack = false)
	{
        InitializeComponent();
        Exit.IsVisible = addBack;
    }

    private async void Exit_Tapped(object sender, TappedEventArgs e) => 
		await Navigation.PopModalAsync();
}