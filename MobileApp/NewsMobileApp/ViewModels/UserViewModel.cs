using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NewsMobileApp.ViewModels;

public class UserViewModel : ViewModelBase
{

    private Guid _userId;
    private string _userName;
    private string _emailAddress;
    private string _phone;
    private DateOnly? _dateOfBirth;
    private string _passwordSalt = string.Empty;
    private string _passwordHash;
    private int _roleId;
    private DateTime _registered;
    private DateTime? _lastLogin;

    public Guid UserId
    {
        get => _userId;
        set => SetProperty(ref _userId, value);
    }

    public string UserName
    {
        get => _userName;
        set => SetProperty(ref _userName, value);
    }

    public string EmailAddress
    {
        get => _emailAddress;
        set => SetProperty(ref _emailAddress, value);
    }

    public string Phone
    {
        get => _phone;
        set => SetProperty(ref _phone, value);
    }

    public DateOnly? DateOfBirth
    {
        get => _dateOfBirth;
        set => SetProperty(ref _dateOfBirth, value);
    }

    public string PasswordSalt
    {
        get => _passwordSalt;
        set => _passwordSalt = value;
    }

    public string PasswordHash
    {
        get => _passwordHash;
        set => SetProperty(ref _passwordHash, value);
    }

    public int RoleId
    {
        get => _roleId;
        set => SetProperty(ref _roleId, value);
    }

    public DateTime Registered
    {
        get => _registered;
        set => SetProperty(ref _registered, value);
    }

    public DateTime? LastLogin
    {
        get => _lastLogin;
        init => _lastLogin = value;
    }
}
