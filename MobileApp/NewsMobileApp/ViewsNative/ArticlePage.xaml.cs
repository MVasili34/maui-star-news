namespace NewsMobileApp.ViewsNative;

public partial class ArticlePage : ContentPage
{

	public ArticlePage(int articleId)
	{
		InitializeComponent();
        RootComponentControl.Parameters = new Dictionary<string, object> { { "articleid", articleId } };
    }
}