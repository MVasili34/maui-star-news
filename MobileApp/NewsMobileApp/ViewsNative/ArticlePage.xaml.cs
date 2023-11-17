using NewsMobileApp.TempServices;

namespace NewsMobileApp.ViewsNative;

public partial class ArticlePage : ContentPage
{
    private readonly Guid _articleId;
    private readonly IRequestsService _requestsService;

	public ArticlePage(Guid articleId)
	{
		InitializeComponent();
        _articleId = articleId;
        RootComponentControl.Parameters = new Dictionary<string, object> { { "articleid", _articleId } };
        _requestsService = Application.Current.Handler.MauiContext
            .Services.GetService<IRequestsService>();
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            string action = await DisplayActionSheet("Удалить статью", "Отмена",
                "Удалить", "Вы уверены, что хотите удалить статью?");
            if (action == "Удалить")
            {
                if (!await _requestsService.DeleteArticleAsync(_articleId))
                    throw new Exception("Произошла неизвестная ошибка сервера!");
                await DisplayAlert("Удалено", "Статья удалена!", "OK");
                await Navigation.PopAsync();
            }
        }
        catch(Exception ex) 
        {
            await DisplayAlert("Ошибка!", $"При удалении статьи произошла ошибка: {ex.Message}", "OK");
        }
    }

    private async void EditButton_Clicked(object sender, EventArgs e) =>
        await Navigation.PushAsync(new ArticleDetailPage(_articleId));
}