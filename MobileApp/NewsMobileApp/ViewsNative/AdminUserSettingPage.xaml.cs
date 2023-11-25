using NewsMobileApp.TempServices;
using NewsMobileApp.ViewModels;
using CryptographyTool;

namespace NewsMobileApp.ViewsNative;

public partial class AdminUserSettingPage : ContentPage
{
	private readonly IRequestsService _requestsService;
    private UserViewModel viewModel => BindingContext as UserViewModel;
    private readonly int _initRole;

	public AdminUserSettingPage(UserViewModel _viewModel)
	{
		InitializeComponent();
        _requestsService = Application.Current.Handler
            .MauiContext.Services.GetService<IRequestsService>();
        BindingContext = _viewModel;
        _initRole = _viewModel.RoleId;
    }

    protected override void OnAppearing()
    {
#if WINDOWS
		PickerHeight.HeightRequest = 110;
#endif
        DatePicker.MaximumDate = DateTime.Now.AddYears(-5);
        base.OnAppearing();
        if (viewModel.DateOfBirth is not null)
        {
            DateOnly current = viewModel.DateOfBirth.Value;
            DatePicker.Date = new(current.Year, current.Month, current.Day);
        }
        ComboBox1.Items.Add("Читатель");
        ComboBox1.Items.Add("Редактор");
        ComboBox1.Items.Add("Администратор");
        ComboBox1.SelectedIndex = viewModel.RoleId - 1;
    }

    private async void Delete_Clicked(object sender, EventArgs e)
    {
        try
        {
            string action = await DisplayActionSheet("Удалить пользователя", "Отмена",
                    "Удалить", "Вы уверены, что хотите удалить пользователя и связанные с ним данные?");
            if (action == "Удалить")
            {
                if (!await _requestsService.DeleteUserAsync(viewModel.UserId))
                    throw new Exception("Пользователь не удалён!");
                await DisplayAlert("Удалено", "Пользователь удалён!", "OK");
                await Navigation.PopAsync();
            }
        }
        catch (Exception ex) 
        {
            await DisplayAlert("Ошибка!", ex.Message, "OK");
        }
    }

    private async void Submit_Clicked(object sender, EventArgs e)
    {
        try
        {
            Guid current = new(Preferences.Get("userId", Guid.NewGuid().ToString()));
            viewModel.RoleId = ComboBox1.SelectedIndex + 1;
            if (viewModel.UserId.Equals(current) && viewModel.RoleId != _initRole)
            {
                throw new Exception("Нельзя изменять свои права доступа!");
            }
            if (!viewModel.UserId.Equals(current) && viewModel.RoleId == 3)
            {
                throw new Exception("Администратором может быть только один пользователь!");
            }    
            viewModel.DateOfBirth = DateOnly.FromDateTime(DatePicker.Date);

            if (!await _requestsService.UpdateUserAdminAsync(viewModel))
                throw new Exception("Данные пользователя не были обнавлёны.");

            await DisplayAlert("Успех!", "Данные пользователя обновлены.", "OK");
        }
        catch (HttpRequestException ex)
        {
            await DisplayAlert("Ошибка!", ex.Message, "OK");
        }
        catch (Exception ex) 
        {
            await DisplayAlert("Ошибка!", ex.Message, "OK");
        }
    }

    private async void Update_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (!StrongPasswordChecker.PasswordCheck(PasswordField.Text))
                throw new Exception("Слабый пароль.");
            AuthorizeViewModel model = new() { 
                EmailAddress = viewModel.EmailAddress,
                Password = PasswordField.Text
            };

            if (!await _requestsService.UpdateUserPswByAdminAsync(model))
                throw new Exception("Пароль не был обнавлён.");
            
            await DisplayAlert("Успех!", "Пароль пользователя обновлен.", "OK");
        }
        catch (HttpRequestException ex)
        {
            await DisplayAlert("Ошибка!", ex.Message, "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ошибка!", ex.Message, "OK");
        }
    }
}