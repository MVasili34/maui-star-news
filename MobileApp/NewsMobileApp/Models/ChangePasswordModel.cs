using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NewsMobileApp.Models;

public class ChangePasswordModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private string _emailAddress = null!;

    private string _oldPswd = null!;

    private string _newPswd = null!;

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
        get => _oldPswd; 
        set
        {
            _oldPswd = value;
            NotifyPropertyChanged();
        }
    }

    public string NewPassword
    {
        get => _newPswd;
        set
        {
            _newPswd = value;
            NotifyPropertyChanged();
        }
    }
}