using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NewsMobileApp.Models;

public record AuthorizeModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private string _emailAddress;

    private string _password;

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