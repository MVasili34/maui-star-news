namespace NewsMobileApp.ViewsNative;

public partial class AddingArticlePage : ContentPage
{
	public AddingArticlePage()
	{
		InitializeComponent();
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
}