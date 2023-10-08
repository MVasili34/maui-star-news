using NewsMobileApp.Models;
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
		ComboBox1.ItemsSource = _newsService.GetCategories().ToList();
		ComboBox1.SelectedIndex = 0;
        ArticleViewModel viewModel = new()
        {
            ArticleId = Guid.NewGuid(),
            PublisherId = null
        };
        BindingContext = viewModel;
		FilePathLabel.Text = "Файл не выбран";
        Edit.IsVisible = false;
	}

	public ArticleDetailPage(INewsService newsService, Guid _articleId)
	{
		InitializeComponent();
        _newsService = newsService;
        ComboBox1.ItemsSource = _newsService.GetCategories().ToList();
        ArticleViewModel article = _newsService.GetThrendArticlesFull().FirstOrDefault(a => a.ArticleId == _articleId);
        BindingContext = article;
		ComboBox1.SelectedIndex = article.SectionId - 1;
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
		result.SectionId = ((Section)ComboBox1.SelectedItem).SectionId;
        string serialized = JsonConvert.SerializeObject(result);
        await DisplayAlert("Success", $"{serialized}", "OK");
    }

    private async void Edit_Clicked(object sender, EventArgs e)
    {

    }
}