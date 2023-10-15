using LiveChartsCore.SkiaSharpView;
using NewsMobileApp.ViewModels;

namespace NewsMobileApp.ViewsNative;

public partial class AdminPage : ContentPage
{
    private readonly AdminViewModel _viewModel;

	public AdminPage(AdminViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
        Finance.XAxes = new Axis[] {
            new DateTimeAxis(TimeSpan.FromDays(1), date => date.ToString("MMMM dd"))
        };
    }

    private async void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.First() is not UserViewModel user) return;

        await DisplayAlert("OK", $"{user.UserName}, {user.UserId}", "OK");
    }
    /*
private async void ListView_Refreshing(object sender, EventArgs e)
{
   listView.IsRefreshing = true;

   await Task.Delay(1500);

   listView.IsRefreshing = false;
}
*/
}