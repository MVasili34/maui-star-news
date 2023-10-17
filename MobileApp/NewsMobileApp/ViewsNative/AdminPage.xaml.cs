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
        ListView.BindingContext = _viewModel.Users;
        _viewModel.AddUsers(false, 20, 0);
    }

    private async void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.First() is not UserViewModel user) return;

        await DisplayAlert("OK", $"{user.UserName}, {user.UserId}", "OK");
    }

    private async void OnScrollViewScrolled(object sender, ScrolledEventArgs e)
    {
        if (sender is not ScrollView scrollView) return;
        
        double scrollViewHeight = scrollView.Height;
        double contentHeight = scrollView.ContentSize.Height;
        double currentScrollPosition = scrollView.ScrollY;

        if (currentScrollPosition + scrollViewHeight >= contentHeight - 20)
        {
            _viewModel.AddUsers();
        }
    }

    private async void SearchText_Completed(object sender, EventArgs e)
    {
        _viewModel.SearchUser(SearchText.Text);
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