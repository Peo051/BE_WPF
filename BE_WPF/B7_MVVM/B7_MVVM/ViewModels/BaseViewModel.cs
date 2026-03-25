using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace B7_MVVM.ViewModels;

/// <summary>
/// L?p c? s? (Base class) cho mô hình MVVM, th?c thi giao di?n INotifyPropertyChanged.
/// Khi m?t thu?c tính (Property) trong ViewModel thay ??i giá tr?, 
/// nó c?n báo cho View (XAML) bi?t ?? c?p nh?t l?i giao di?n t? ??ng.
/// </summary>
public class BaseViewModel : INotifyPropertyChanged
{
    // S? ki?n s? ???c kích ho?t m?i khi có thu?c tính thay ??i
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Ph??ng th?c h? tr? g?i s? ki?n PropertyChanged.
    /// CallerMemberName giúp t? ??ng l?y tên c?a thu?c tính g?i hàm này.
    /// </summary>
    /// <param name="propertyName">Tên c?a thu?c tính v?a ???c thay ??i giá tr?.</param>
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
