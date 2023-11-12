using System.ComponentModel;
using System.Runtime.CompilerServices;
using NewsMobileApp.Models;

namespace NewsMobileApp.ViewModels;

public class ChangePasswordUserViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private string _emailAddress;
    private string _oldPassword;
    private string _newPassword1;
    private string _newPassword2;

    public string EmailAddress
    {
        get => _emailAddress;
        set
        {
            _emailAddress = value;
            NotifyPropertyChanged();
        }
    }

    public string OldPassword
    {
        get => _oldPassword;
        set
        {
            _oldPassword = value;
            NotifyPropertyChanged();
        }
    }

    public string NewPassword1
    {
        get => _newPassword1;
        set
        {
            _newPassword1 = value;
            NotifyPropertyChanged();
        }
    }

    public string NewPassword2
    {
        get => _newPassword2;
        set
        {
            _newPassword2 = value;
            NotifyPropertyChanged();
        }
    }

    /// <summary>
    /// Getting <see cref="AuthorizeModel"/> with old password
    /// </summary>
    /// <param name="model"></param>
    public static explicit operator AuthorizeModel(ChangePasswordUserViewModel model) => new() {
        EmailAddress = model.EmailAddress,
        Password = model.OldPassword
    };

    /// <summary>
    /// Method for getting <see cref="AuthorizeModel"/> with new password
    /// </summary>
    /// <returns><see cref="AuthorizeModel"/> instance</returns>
    public AuthorizeModel GetNewAuthorizeModel() => new() { 
        EmailAddress = EmailAddress, 
        Password = NewPassword1 
    };
}
