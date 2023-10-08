using NewsMobileApp.TempServices;
using NewsMobileApp.ViewModels;
using Newtonsoft.Json;

namespace NewsMobileApp.ViewsNative;

public partial class ArticleDetailPage : ContentPage
{
	private readonly INewsService _newsService;

	public ArticleDetailPage(INewsService newsService)
	{
		InitializeComponent();
		_newsService = newsService;
        ArticleViewModel viewModel = new()
        {
            ArticleId = Guid.NewGuid(),
            PublisherId = null
        };
        BindingContext = viewModel;
		FilePathLabel.Text = "Файл не выбран";
        Edit.IsVisible = false;
	}

	public ArticleDetailPage(INewsService newsService, int _articleId)
	{
		InitializeComponent();
        _newsService = newsService;

        Submit.IsVisible = false;
	}

    private async void PickImageButton_Clicked(object sender, EventArgs e)
    {
		FileResult photo = await MediaPicker.Default.PickPhotoAsync();
		if(photo is not null)
		{
			FileImage.Source = ImageSource.FromFile(photo.FullPath);
			FileImage.WidthRequest = 100;
			FileImage.HeightRequest = 70;
			FilePathLabel.Text = photo.FullPath;
		}
		else
		{
			await DisplayAlert("Ошибка", "Фото не добавлено", "ОК");
		}
    }

    private async void Submit_Clicked(object sender, EventArgs e)
    {
        ArticleViewModel result = (ArticleViewModel)BindingContext;
        string serialized = JsonConvert.SerializeObject(result);
        await DisplayAlert("Success", $"{serialized}", "OK");
    }

    private async void Edit_Clicked(object sender, EventArgs e)
    {

    }
}