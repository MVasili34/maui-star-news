using NewsMobileApp.TempServices;

namespace NewsMobileApp.ViewsNative;

public partial class ArticlePage : ContentPage
{
    private readonly Guid _articleId;

	public ArticlePage(Guid articleId)
	{
		InitializeComponent();
        _articleId = articleId;
        RootComponentControl.Parameters = new Dictionary<string, object> { { "articleid", _articleId } };
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        string action = await DisplayActionSheet("Удалить статью", "Отмена",
            "Удалить", "Вы уверены, что хотите удалить статью?");
        if (action == "Удалить")
            await DisplayAlert("Удалено", "Статья удалена!", "OK");
    }

    private async void EditButton_Clicked(object sender, EventArgs e) =>
        await Navigation.PushAsync(new ArticleDetailPage(new NewsService(), _articleId));
}