using NewsMobileApp.Models;
using NewsMobileApp.TempServices;
using NewsMobileApp.ViewModels;

namespace NewsMobileApp.ViewsNative;

public partial class ArticleDetailPage : ContentPage
{
	private ArticleDetailViewModel viewModel => BindingContext as ArticleDetailViewModel;
    private readonly Guid? _articleId;
	private bool _loaded = false;
	private bool _pictureChanged = false;

	public ArticleDetailPage()
	{
		InitializeComponent();
        BindingContext = new ArticleDetailViewModel();
        Edit.IsVisible = false;
		LoadingState();
	}

	public ArticleDetailPage(Guid articleId)
	{
		InitializeComponent();
		_articleId = articleId;
        BindingContext = new ArticleDetailViewModel();
        //ComboBox1.SelectedIndex = _articleViewModel.SectionId - 1;
        Submit.IsVisible = false;
        LoadingState();
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        if (this.AnimationIsRunning("TransitionAnimation"))
            return;
#if WINDOWS
		PickerHeight.HeightRequest = 110;
#endif
        var parentAnimation = AnimationCreator.SetAnimations(LoadBlock1, LoadBlock2, 
			LoadBlock3, LoadBlock4, LoadBlock5);

        parentAnimation.Commit(this, "TransitionAnimation", length: 2000, repeat: () => true);
		try
		{
			if (_articleId is null)
			{
				await viewModel.InitializeArticle();
				ComboBox1.SelectedIndex = 0;
			}
			else
			{
				await viewModel.InitializeArticle(_articleId);
				ComboBox1.SelectedIndex = viewModel.Article.SectionId - 1;
			}
			_loaded = true;
			LoadingState();
		}
		catch (Exception ex) 
		{
			await DisplayAlert("Ошибка!", $"Произошла ошибка при попытке загрузки статьи: {ex.Message}", "OK");
		}
    }

    private async void PickImageButton_Clicked(object sender, EventArgs e)
    {
		try
		{
			FileResult photo = await MediaPicker.Default.PickPhotoAsync();
			if (photo is not null)
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
		catch (Exception ex) 
		{
			await DisplayAlert("Ошибка!", ex.Message, "ОК");
        }
    }

    private async void Submit_Clicked(object sender, EventArgs e)
    {
		try
		{
			if (FilePathLabel.Text is null)
			{
				await DisplayAlert("Bad", $"Не прикреплено изображение", "OK");
				return;
			}

			if (!(new FileInfo(FilePathLabel.Text)).Exists)
			{
				throw new FileNotFoundException();
			}
			viewModel.Article.Image = FilePathLabel.Text;
			viewModel.Article.SectionId = ((Section)ComboBox1.SelectedItem).SectionId;
			BlockUI(true);
			await viewModel.PublishArticle();
            await DisplayAlert("Успех!", "Статья опубликована.", "OK");
            await Navigation.PopAsync(true);
		}
		catch (FileNotFoundException)
		{
			await DisplayAlert("Ошибка!", "Изображения не существует", "ОК");
		}
		catch (Exception ex)
		{
			await DisplayAlert("Ошибка!", ex.Message, "ОК");
			ComboBox1.SelectedIndex--;
		}
        BlockUI(false);
    }

    private async void Edit_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (FilePathLabel.Text is null)
            {
                await DisplayAlert("Bad", $"Не прикреплено изображение", "OK");
                return;
            }

            if (_pictureChanged && !(new FileInfo(FilePathLabel.Text)).Exists)
            {
                throw new FileNotFoundException();
            }
            viewModel.Article.Image = FilePathLabel.Text;
            viewModel.Article.SectionId = ((Section)ComboBox1.SelectedItem).SectionId;
            BlockUI(true);
            await viewModel.ChangeArticle(_pictureChanged);
			await DisplayAlert("Успех!", "Статья изменена.", "OK");
            await Navigation.PopAsync(true);
        }
        catch (FileNotFoundException)
        {
            await DisplayAlert("Ошибка!", "Изображения не существует", "ОК");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ошибка!", ex.Message, "ОК");
            ComboBox1.SelectedIndex--;
        }
        BlockUI(false);
    }

	private void LoadingState()
	{
		LoadBlocks.IsVisible = !_loaded;
		FullyContent.IsVisible = _loaded;
	}
	
	private void BlockUI(bool block)
	{
		TitleField.IsReadOnly = block;
		SubTitleField.IsReadOnly = block;
		ComboBox1.IsEnabled = !block;
		PickImageButton.IsEnabled = !block;
		ArticleText.IsReadOnly = block;
		Edit.IsEnabled = !block;
		Submit.IsEnabled = !block;
	}
}