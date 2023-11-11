using LiveChartsCore.SkiaSharpView;
using NewsMobileApp.ViewModels;
using NewsMobileApp.TempServices;

namespace NewsMobileApp.ViewsNative;

public partial class AdminPage : ContentPage
{
    private AdminViewModel viewModel => BindingContext as AdminViewModel;
    private bool _loaded = false;
    private bool _scrolled = true;
    private string _currentSearch = string.Empty;
    private const int _limit = 20;
    private int _offset = 0;

    public AdminPage(AdminViewModel viewmodel)
	{
		InitializeComponent();
        Finance.XAxes = new Axis[] {
            new DateTimeAxis(TimeSpan.FromDays(1), date => date.ToString("MMMM dd"))
        };
        BindingContext = viewmodel;
        LoadProcessSimulation();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (this.AnimationIsRunning("TransitionAnimation"))
            return;

        var parentAnimation = AnimationCreator.SetAnimations(LoadBlock1, LoadBlock2, LoadBlock3);

        parentAnimation.Commit(this, "TransitionAnimation", length: 2000, repeat: () => true);

        try
        {
            await Task.WhenAll(viewModel.GetDiagtamData(), viewModel.AddUsers(false, _limit, _offset));
            _offset++;
            _loaded = true;
            LoadProcessSimulation();
        }
        catch(Exception ex) 
        {
            await DisplayAlert("Ошибка!", ex.Message, "OK");
        }
    }

    private async void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection[0] is not UserViewModel user) return;

        await Navigation.PushAsync(new AdminUserSettingPage(user));
        //await DisplayAlert("OK", $"{user.UserName}, {user.UserId}", "OK");
    }

    private async void OnScrollViewScrolled(object sender, ScrolledEventArgs e)
    {
        if (sender is not ScrollView scrollView) return;
        
        double scrollViewHeight = scrollView.Height;
        double contentHeight = scrollView.ContentSize.Height;
        double currentScrollPosition = scrollView.ScrollY;

        if (currentScrollPosition + scrollViewHeight >= contentHeight - 20 && _scrolled)
        {
            _scrolled = false;
            try
            {
                await viewModel.AddUsers(limit: _limit, offset: _offset);
                _offset++;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка!", ex.Message, "OK");
            }
            _scrolled = true;
        }
    }

    private async void SearchText_Completed(object sender, EventArgs e)
    {
        _loaded = false;
        LoadProcessSimulation();
        try
        {
            if (string.IsNullOrEmpty(SearchText.Text) || string.IsNullOrWhiteSpace(SearchText.Text))
            {
                await viewModel.AddUsers(true);
                _offset = 1;
                _loaded = true;
                LoadProcessSimulation();
                return;
            }
            _currentSearch = SearchText.Text;
            viewModel.Users.Clear();
            _offset = 0;
            await viewModel.SearchUser(_currentSearch);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ошибка!", ex.Message, "OK");
        }
        _loaded = true;
        LoadProcessSimulation();
    }
    private void LoadProcessSimulation()
    {
        LoadingText1.IsVisible = _loaded;
        SearchText.IsEnabled = _loaded;
        ListView.IsVisible = _loaded;
        LoadBlocks.IsVisible = !_loaded;
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