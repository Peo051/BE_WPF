using System.Windows.Input;

namespace B7_MVVM.ViewModels;

/// <summary>
/// L?p RelayCommand là m?t ph?n quan tr?ng trong mô hình MVVM.
/// Nó giúp tách bi?t x? lý s? ki?n (Event) ra kh?i giao di?n (View), 
/// thay vào ?ó s? liên k?t nút b?m (Button, Menu) v?i các ph??ng th?c n?m trong ViewModel.
/// </summary>
public class RelayCommand : ICommand
{
    // C?t gi? hàm th?c thi chính (hành ??ng khi click)
    private readonly Action<object?> _execute;
    
    // C?t gi? hàm ki?m tra ?i?u ki?n (có ???c phép click hay không)
    private readonly Predicate<object?>? _canExecute;

    /// <summary>
    /// Kh?i t?o m?t Command m?i.
    /// </summary>
    /// <param name="execute">Hành ??ng c?n th?c thi (B?t bu?c).</param>
    /// <param name="canExecute">Ki?m tra xem có ???c phép th?c thi không (Tùy ch?n).</param>
    public RelayCommand(Action<object?> execute, Predicate<object?>? canExecute = null)
    {
        _execute = execute;
        _canExecute = canExecute;
    }

    /// <summary>
    /// Xác ??nh xem command có th? th?c thi ? tr?ng thái hi?n t?i hay không.
    /// WPF s? t? ??ng g?i hàm này ?? ?n/hi?n (Enable/Disable) ví d? nh? m?t nút b?m Button.
    /// </summary>
    public bool CanExecute(object? parameter)
    {
        return _canExecute == null || _canExecute(parameter);
    }

    /// <summary>
    /// Hàm này ???c g?i khi Command ???c kích ho?t (Ng??i dùng click Button).
    /// </summary>
    public void Execute(object? parameter)
    {
        _execute(parameter);
    }

    /// <summary>
    /// Báo cho giao di?n (View) bi?t r?ng ?i?u ki?n CanExecute ?ã thay ??i, 
    /// ?? nó ki?m tra l?i vi?c b?t/t?t (Enable/Disable) các control.
    /// </summary>
    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }
}
