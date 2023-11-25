using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NewsMobileApp.ViewModels;

public class AuthorizeViewModel : ViewModelBase
{
    private string _emailAddress = null!;

    private string _password = null!;

    public string EmailAddress
    {
        get => _emailAddress;
        set => SetProperty(ref _emailAddress, value);
    }
    public string Password
    {
        get => _password;
        set => SetProperty(ref _password, value);
    }
}