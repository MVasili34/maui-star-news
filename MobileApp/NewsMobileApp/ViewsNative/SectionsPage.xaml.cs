using NewsMobileApp.Models;
using NewsMobileApp.TempServices;
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

        var parentAnimation = AnimationCreator.SetAnimations(LoadBlock1, LoadBlock2, 
            LoadBlock3, LoadBlock4, LoadBlock5);

        parentAnimation.Commit(this, "TransitionAnimation", length: 2000, repeat: () => true);
        try
        {
            await viewModel.SetSections();
        }
        catch (Exception ex) 
        {
            await DisplayAlert("Ошибка!", ex.Message, "OK");
        }
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