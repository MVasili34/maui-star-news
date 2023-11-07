using NewsMobileApp.Models;
using NewsMobileApp.TempServices;
using NewsMobileApp.ViewModels;
using Newtonsoft.Json;

namespace NewsMobileApp.ViewsNative;

public partial class ArticleDetailPage : ContentPage
{
	private readonly INewsService _newsService;
	private readonly ArticleViewModel _articleViewModel;
	private bool _loaded = false;
	private bool _pictureChanged = false;

	public ArticleDetailPage()
	{
		InitializeComponent();
		_newsService = _newsService = Application.Current.Handler
           .MauiContext.Services.GetService<INewsService>();
        ComboBox1.ItemsSource = _newsService.GetCategories().ToList();
		ComboBox1.SelectedIndex = 0;
        _articleViewModel = new()
        {
            ArticleId = Guid.NewGuid(),
            PublisherId = null
        };
        BindingContext = _articleViewModel;
        Edit.IsVisible = false;
		LoadingState();
	}

	public ArticleDetailPage(Guid _articleId)
	{
		InitializeComponent();
        _newsService = _newsService = Application.Current.Handler
           .MauiContext.Services.GetService<INewsService>();
        ComboBox1.ItemsSource = _newsService.GetCategories().ToList();
        _articleViewModel = _newsService.GetThrendArticlesFull().FirstOrDefault(a => a.ArticleId == _articleId);
        BindingContext = _articleViewModel;
		ComboBox1.SelectedIndex = _articleViewModel.SectionId - 1;
        Submit.IsVisible = false;
        LoadingState();
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        if (this.AnimationIsRunning("TransitionAnimation"))
            return;

        var parentAnimation = AnimationCreator.SetAnimations(LoadBlock1, LoadBlock2, 
			LoadBlock3, LoadBlock4, LoadBlock5);

        parentAnimation.Commit(this, "TransitionAnimation", length: 2000, repeat: () => true);
		await Task.Delay(3000);
		_loaded = true;
		LoadingState();
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
			_pictureChanged = true;
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
		if (FilePathLabel.Text is null)
		{
			await DisplayAlert("Bad", $"Не прикреплено изображение", "OK");
			return;
        }
		result.Image = "https://future";
		result.PublishTime = DateTime.Now;
		string serialized = JsonConvert.SerializeObject(result);
        await DisplayAlert("Success", $"{serialized}", "OK");
    }

    private async void Edit_Clicked(object sender, EventArgs e)
    {
        ArticleViewModel result = (ArticleViewModel)BindingContext;
        result.SectionId = ((Section)ComboBox1.SelectedItem).SectionId;
        if (_pictureChanged)
		{
			result.Image = "https://imgur.link/";
		}
        string serialized = JsonConvert.SerializeObject(result);
        await DisplayAlert("Success", $"{serialized}", "OK");
    }

	private void LoadingState()
	{
		LoadBlocks.IsVisible = !_loaded;
		FullyContent.IsVisible = _loaded;
	}
}