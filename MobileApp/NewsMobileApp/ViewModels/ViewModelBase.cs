using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NewsMobileApp.ViewModels;

/// <summary>
/// Base ViewModel class that implements the <see cref="INotifyPropertyChanged"/> interface.
/// </summary>
public class ViewModelBase : INotifyPropertyChanged
{
    /// <summary>
    /// Event to be invoked when a property changes.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// Notifies that a property has changed.
    /// </summary>
    /// <param name="propertyName">The name of the property that changed.</param>
    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    /// <summary>
    /// Sets the property to a new value and notifies that the property has changed.
    /// </summary>
    /// <typeparam name="T">The type of the property.</typeparam>
    /// <param name="storage">A reference to a field to contain the property value.</param>
    /// <param name="value">The new value of the property.</param>
    /// <param name="propertyName">The name of the property that changed.</param>
    /// <returns>Returns <see href="true"/> if the value was changed, otherwise <see href="false"/>.</returns>
    protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = "")
    {
        if (EqualityComparer<T>.Default.Equals(storage, value))
            return false;
        storage = value;
        this.NotifyPropertyChanged(propertyName);
        return true;
    }
}
