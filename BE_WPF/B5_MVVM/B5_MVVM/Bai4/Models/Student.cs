using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Bai4.Models;

public class Student : INotifyPropertyChanged
{
    private string _name = string.Empty;
    private int _age;

    public string Name
    {
        get => _name;
        set
        {
            if (_name == value)
            {
                return;
            }

            _name = value;
            OnPropertyChanged();
        }
    }

    public int Age
    {
        get => _age;
        set
        {
            if (_age == value)
            {
                return;
            }

            _age = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
