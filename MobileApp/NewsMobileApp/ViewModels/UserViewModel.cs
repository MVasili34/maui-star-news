using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NewsMobileApp.ViewModels;

public class UserViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private Guid _userId;
    private string _userName;
    private string _emailAddress;
    private string _phone;
    private DateOnly? _dateOfBirth;
    private string _passwordSalt;
    private string _passwordHash;
    private int _roleId;
    private DateTime _registered;
    private DateTime? _lastLogin;


    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    public Guid UserId
    {
        get => _userId;
        init => _userId = value;
    }

    public string UserName
    {
        get => _userName;
        set
        {
            _userName = value;
            NotifyPropertyChanged();
        }
    }

    public string EmailAddress
    {
        get => _emailAddress;
        init => _emailAddress = value;
    }

    public string Phone
    {
        get => _phone;
        set 
        {
            _phone = value;
            NotifyPropertyChanged();
        }
    }

    public DateOnly? DateOfBirth
    {
        get => _dateOfBirth;
        set
        {
            _dateOfBirth = value;
            NotifyPropertyChanged();
        }
    }

    public string PasswordSalt
    {
        get => _passwordSalt;
        set => _passwordSalt = value;
    }

    public string PasswordHash
    {
        get => _passwordHash;
        set
        {
            _passwordHash = value;
            NotifyPropertyChanged();
        }
    }

    public int RoleId
    {
        get => _roleId;
        set
        {
            _roleId = value;
            NotifyPropertyChanged();
        }
    }

    public DateTime Registered
    {
        get => _registered;
        init => _registered = value;
    }

    public DateTime? LastLogin
    {
        get => _lastLogin;
        init => _lastLogin = value;
    }
}
