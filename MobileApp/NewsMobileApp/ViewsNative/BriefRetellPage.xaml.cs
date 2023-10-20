namespace NewsMobileApp.ViewsNative;

public partial class BriefRetellPage : ContentPage
{
	public BriefRetellPage(string title, string text)
	{
		InitializeComponent();
        RootComponentControl.Parameters = new Dictionary<string, object> { { "title", title },
			{ "text", text } };
    }
}