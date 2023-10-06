namespace NewsMobileApp.ViewsNative;

public partial class ArticlePage : ContentPage
{

	public ArticlePage(int articleId)
	{
		InitializeComponent();
        RootComponentControl.Parameters = new Dictionary<string, object> { { "articleid", articleId } };
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        string action = await DisplayActionSheet("Удалить статью", "Отмена",
            "Удалить", "Вы уверены, что хотите удалить статью?");
        if (action == "Удалить")
            await DisplayAlert("Удалено", "Статья удалена!", "OK");
    }
}