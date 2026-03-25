using System.Windows;
using System.Windows.Input;
using B7_MVVM.Services;

namespace B7_MVVM.ViewModels;

/// <summary>
/// ViewModel "T?ng t? l?nh", ch?a và n?m gi? các ViewModels nh? h?n.
/// Nó quy?t ??nh vi?c ?i?u h??ng, t?c là thay ??i n?i dung (Content Control) thay qua thay l?i màn hình.
/// ???c g?n th?ng vào MainWindow.DataContext
/// </summary>
public class MainViewModel : BaseViewModel
{
    private readonly LapHoaDonViewModel _lapHoaDonViewModel;
    private readonly DanhSachHoaDonViewModel _danhSachHoaDonViewModel;

    // Bi?n ??i di?n "Bây gi? ?ang chi?u màn hình nào?"
    private object _currentViewModel;

    public MainViewModel()
    {
        // 1. T?o Database Share chung (n?i l?u danh sách mua hàng).
        QuanLyHoaDonService quanLyHoaDonService = new();

        // 2. Chích chung DB ?ó phân phát cho c? hai màn hình.
        // B?ng cách này thêm hóa ??n ? LapHoaDon, nh?y qua DanhSach là th?y ngay.
        _lapHoaDonViewModel = new LapHoaDonViewModel(quanLyHoaDonService);
        _danhSachHoaDonViewModel = new DanhSachHoaDonViewModel(quanLyHoaDonService);

        // M?c ??nh kh?i ??ng app lên thì m? màn hình l?p phi?u ?? ?n.
        _currentViewModel = _lapHoaDonViewModel;

        // Command cho menu
        MoLapHoaDonCommand = new RelayCommand(_ => CurrentViewModel = _lapHoaDonViewModel);
        MoDanhSachHoaDonCommand = new RelayCommand(_ =>
        {
            // B?t bu?c Load/Làm m?i d? li?u tr??c khi chuy?n qua tab hi?n th?
            _danhSachHoaDonViewModel.LoadCommand.Execute(null);
            CurrentViewModel = _danhSachHoaDonViewModel;
        });
        ThoatUngDungCommand = new RelayCommand(_ => Application.Current.Shutdown());
    }

    /// <summary>
    /// View (MainWindow) k?t n?i (Bind) m?t `ContentControl` vào property này.
    /// N?u thay _currentViewModel thành LapHoaDon thì s? t? ??ng dùng `<DataTemplate DataType="{x:Type LapHoaDonViewModel}">` bên MainWindow ?? v? Form L?p hóa ??n lên và ng??c l?i.
    /// </summary>
    public object CurrentViewModel
    {
        get => _currentViewModel;
        set
        {
            _currentViewModel = value;
            OnPropertyChanged(); // C?n thi?t ?? ??i c?nh
        }
    }

    // Các k?t n?i c?a Menu H? th?ng, Bán hàng
    public ICommand MoLapHoaDonCommand { get; }

    public ICommand MoDanhSachHoaDonCommand { get; }

    public ICommand ThoatUngDungCommand { get; }
}
