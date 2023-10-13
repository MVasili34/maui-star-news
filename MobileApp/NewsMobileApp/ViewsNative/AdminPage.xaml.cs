using LiveChartsCore.SkiaSharpView;

namespace NewsMobileApp.ViewsNative;

public partial class AdminPage : ContentPage
{
	public AdminPage()
	{
		InitializeComponent();

        Finance.XAxes = new Axis[] {
            new DateTimeAxis(TimeSpan.FromDays(1), date => date.ToString("MMMM dd"))
        };
    }
}