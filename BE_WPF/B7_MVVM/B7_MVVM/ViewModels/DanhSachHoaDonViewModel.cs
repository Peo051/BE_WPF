using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using B7_MVVM.Models;
using B7_MVVM.Services;

namespace B7_MVVM.ViewModels;

/// <summary>
/// ViewModel chuyên bi?t cho màn hình hi?n th? và th?ng kê toàn b? "Danh sách hóa ??n" ?ã thanh toán.
/// </summary>
public class DanhSachHoaDonViewModel : BaseViewModel
{
    private readonly QuanLyHoaDonService _quanLyHoaDonService;

    // Bi?n cho các ô TextBox dùng th?ng kê t?ng
    private int _tongKhachHang;
    private decimal _tongTienThanhToan;

    public DanhSachHoaDonViewModel(QuanLyHoaDonService quanLyHoaDonService)
    {
        _quanLyHoaDonService = quanLyHoaDonService;
        
        // ??ng ký s? ki?n: C? h? d?ch v? này b? thêm cái gì, thì ta g?i hàm tính l?i Th?ng kê.
        _quanLyHoaDonService.DanhSachHoaDon.CollectionChanged += DanhSachHoaDon_CollectionChanged;

        LoadCommand = new RelayCommand(_ => Load());
        ThongKeCommand = new RelayCommand(_ => ThongKe());

        // L?n ??u m? thì t? load danh sách ra
        Load();
    }

    // G?n List hóa ??n ?ã l?u t? Service ra bên ngoài Property ch? ListView nó Binding
    // Do dùng ObservableCollection nên không c?n vi?t Set r??m rà.
    public ObservableCollection<HoaDon> DanhSachHoaDon => _quanLyHoaDonService.DanhSachHoaDon;

    /// <summary>
    /// S? l??ng khách hàng mua hàng (Dùng hi?n th? text box bên d??i cùng)
    /// </summary>
    public int TongKhachHang
    {
        get => _tongKhachHang;
        set
        {
            _tongKhachHang = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// C?ng d?n toàn b? s? ti?n thanh toán (Dùng hi?n th? text box)
    /// </summary>
    public decimal TongTienThanhToan
    {
        get => _tongTienThanhToan;
        set
        {
            _tongTienThanhToan = value;
            OnPropertyChanged();
        }
    }

    public ICommand LoadCommand { get; }
    public ICommand ThongKeCommand { get; }

    /// <summary>
    /// Event c?a ObservableCollection, kích ho?t m?i lúc khi có hóa ??n thêm/b?t/xóa
    /// </summary>
    private void DanhSachHoaDon_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        // M?i khi có thay ??i danh sách hóa ??n thì t? ??ng c?p nh?t th?ng kê luôn
        CapNhatThongKe();
    }

    /// <summary>
    /// T?i, làm m?i danh sách d? li?u ?? hi?n th? ra View
    /// </summary>
    private void Load()
    {
        // Thông báo UI c?p nh?t l?i List View hóa ??n
        OnPropertyChanged(nameof(DanhSachHoaDon));
        
        // Và tính l?i các giá tr? t?ng
        CapNhatThongKe();
    }

    /// <summary>
    /// Hàm dành cho nút "Th?ng kê"
    /// </summary>
    private void ThongKe()
    {
        CapNhatThongKe();
    }

    /// <summary>
    /// Toán h?c: T?ng s? khách hàng = S? s? list. T?ng ti?n = C?ng sum các ??i t??ng.
    /// </summary>
    private void CapNhatThongKe()
    {
        TongKhachHang = DanhSachHoaDon.Count;
        TongTienThanhToan = DanhSachHoaDon.Sum(x => x.TongTienThanhToan);
    }
}
