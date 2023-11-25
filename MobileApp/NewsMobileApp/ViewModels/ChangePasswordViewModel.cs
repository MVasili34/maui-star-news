using System.Text.Json.Serialization;

namespace NewsMobileApp.ViewModels;

public class ChangePasswordViewModel : ViewModelBase
{
    private string _emailAddress = null!;

    private string _oldPswd = null!;

    private string _newPswd = null!;

    public string EmailAddress
    {
        get => _emailAddress;
        set => SetProperty(ref _emailAddress, value);
    }
    public string OldPassword
    {
        get => _oldPswd;
        set => SetProperty(ref _oldPswd, value);
    }

    public string NewPassword
    {
        get => _newPswd;
        set => SetProperty(ref _newPswd, value);
    }

    [JsonIgnore]
    public string NewPasswordVerify
    {
        get => _newPswd;
        set => SetProperty(ref _newPswd, value);
    }
}