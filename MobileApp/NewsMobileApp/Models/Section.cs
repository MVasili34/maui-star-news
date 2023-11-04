using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NewsMobileApp.Models;

public class Section : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private int _sectionId;
    private string _name;
    private string _materialIcon;
    private string _description = string.Empty;

    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    public Section(int sectionId, string name, string materialIcon)
    {
        SectionId = sectionId;
        Name = name;
        MaterialIcon = materialIcon;
    }

    public int SectionId
    {
        get => _sectionId;
        set
        {
            _sectionId = value;
            NotifyPropertyChanged();
        }
    }

    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            NotifyPropertyChanged();
        }
    }

    public string MaterialIcon
    {
        get => _materialIcon;
        set
        {
            _materialIcon = value;
            NotifyPropertyChanged();
        }
    }

    public string Description
    {
        get => _description;
        set
        {
            _description = value;
            NotifyPropertyChanged();
        }
    }
}
