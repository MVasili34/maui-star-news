using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NewsMobileApp.Models;

public class AuthorizeModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private string _emailAddress = null!;

    private string _password = null!;

    public string EmailAddress
    {
        get => _emailAddress;
        set
        {
            _emailAddress = value;
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
}