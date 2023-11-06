using NewsMobileApp.Models;
using NewsMobileApp.ViewModels;

namespace NewsMobileApp.ViewsNative;

public partial class SectionsPage : ContentPage
{
    private SectionViewModel viewModel => BindingContext as SectionViewModel;

	public SectionsPage(SectionViewModel viewModel)
	{
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        if (this.AnimationIsRunning("TransitionAnimation"))
            return;

        var parentAnimation = new Animation
        {
            { 0, 0.5, new Animation(v => LoadBlock1.Opacity = v, 1, 0.3, Easing.CubicIn) },
            { 0.5, 1, new Animation(v => LoadBlock1.Opacity = v, 0.3, 1, Easing.CubicIn) },
            { 0, 0.5, new Animation(v => LoadBlock2.Opacity = v, 1, 0.3, Easing.CubicIn) },
            { 0.5, 1, new Animation(v => LoadBlock2.Opacity = v, 0.3, 1, Easing.CubicIn) },
            { 0, 0.5, new Animation(v => LoadBlock3.Opacity = v, 1, 0.3, Easing.CubicIn) },
            { 0.5, 1, new Animation(v => LoadBlock3.Opacity = v, 0.3, 1, Easing.CubicIn) },
            { 0, 0.5, new Animation(v => LoadBlock4.Opacity = v, 1, 0.3, Easing.CubicIn) },
            { 0.5, 1, new Animation(v => LoadBlock4.Opacity = v, 0.3, 1, Easing.CubicIn) },
            { 0, 0.5, new Animation(v => LoadBlock5.Opacity = v, 1, 0.3, Easing.CubicIn) },
            { 0.5, 1, new Animation(v => LoadBlock5.Opacity = v, 0.3, 1, Easing.CubicIn) },
        };

        parentAnimation.Commit(this, "TransitionAnimation", length: 2000, repeat: () => true);
        await viewModel.SetSections();
    }


    private async void Section_Tapped(object sender, TappedEventArgs e)
    {
        if (e.Parameter is not Section section) {
            await DisplayAlert("Ошибка!", "Произошла неизвестная ошибка.", "OK");
            return;
        }
        await Navigation.PushAsync(new ArticlesBySectionPage(
            section));
    }
}