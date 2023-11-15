using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NewsMobileApp.Models;

public record RegisterModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private string _userName = null!;

    private string _email = null!;

    private string _password = null!;

    private int _roleId = 1;

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
        get => _email;
        set 
        {
            _email = value;
            NotifyPropertyChanged();
        }
    }
    public string Password
    {
        get => _password;
        set
        {
            _password = value;
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
}
