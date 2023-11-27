namespace NewsMobileApp.ViewModels;

public class RegisterViewModel : ViewModelBase
{
    private string _userName = null!;

    private string _email = null!;

    private string _password = null!;

    private int _roleId = 1;

    public string UserName
    {
        get => _userName;
        set => SetProperty(ref _userName, value);
    }
    public string EmailAddress
    {
        get => _email;
        set => SetProperty(ref _email, value);
    }
    public string Password
    {
        get => _password;
        set => SetProperty(ref _password, value);
    }
    public int RoleId
    {
        get => _roleId;
        set => SetProperty(ref _roleId, value);
    }
}
