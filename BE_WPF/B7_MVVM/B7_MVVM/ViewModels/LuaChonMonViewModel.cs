namespace B7_MVVM.ViewModels;

/// <summary>
/// Môt ViewModel nh? dành riêng cho các món ?n/n??c u?ng khi hi?n th? trên giao di?n theo danh sách CheckBox.
/// Lý do ph?i dùng l?p này thay vì dùng th?ng Model `MonAnNuocUong`:
/// Vì mình c?n thu?c tính `IsSelected` (bi?t món ?ó có ?ang ???c CheckBox tick ch?n hay không) và c?n NotifyPropertyChanged.
/// </summary>
public class LuaChonMonViewModel : BaseViewModel
{
    private bool _isSelected;

    // Tên món (?? render ra ch? k? bên cái Checkbox)
    public string TenMon { get; set; } = string.Empty;

    // Giá ti?n c?a món
    public decimal DonGia { get; set; }

    /// <summary>
    /// Thu?c tính này binding th?ng vào IsChecked c?a CheckBox.
    /// Có OnPropertyChanged ?? khi check vào / b? check, giao di?n c?p nh?t tr?ng thái.
    /// </summary>
    public bool IsSelected
    {
        get => _isSelected;
        set
        {
            _isSelected = value;
            OnPropertyChanged();
        }
    }
}
