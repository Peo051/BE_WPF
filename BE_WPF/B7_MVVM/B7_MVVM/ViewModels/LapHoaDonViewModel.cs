using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using B7_MVVM.Models;
using B7_MVVM.Services;

namespace B7_MVVM.ViewModels;

/// <summary>
/// ViewModel chính phụ trách toàn bộ Xử Lý Logic Nghiệp Vụ (Nhập dữ liệu, Lập hóa đơn)
/// Của màn hình "Lập hóa đơn" (LapHoaDonView).
/// </summary>
public class LapHoaDonViewModel : BaseViewModel
{
    // Dịch vụ dùng chung để cất giữ hoá đơn đã thanh toán.
    private readonly QuanLyHoaDonService _quanLyHoaDonService;

    // --- Các biến private chứa dữ liệu Binding với Form ---
    private string _tenKhachHang = string.Empty;
    private string _soDienThoai = string.Empty;
    private bool _laSinhVien;
    private bool _ban01;
    private bool _ban02;
    private bool _ban03;
    private bool _ban04;
    private string _viTriBan = string.Empty;
    
    // Hóa đơn tạm để theo dõi trước khi được bấm Thanh toán
    private HoaDon? _hoaDonTam;

    public LapHoaDonViewModel(QuanLyHoaDonService quanLyHoaDonService)
    {
        _quanLyHoaDonService = quanLyHoaDonService;

        // Khởi tạo danh sách các món đồ uống cố định
        DanhSachNuocUong = new ObservableCollection<LuaChonMonViewModel>
        {
            new() { TenMon = "Cafe đen", DonGia = 20000 },
            new() { TenMon = "Cafe sữa", DonGia = 25000 },
            new() { TenMon = "Cafe đá", DonGia = 25000 },
            new() { TenMon = "Cafe kem", DonGia = 35000 },
            new() { TenMon = "Cafe sữa đá", DonGia = 30000 }
        };

        // Khởi tạo danh sách các món đồ ăn cố định
        DanhSachThucAn = new ObservableCollection<LuaChonMonViewModel>
        {
            new() { TenMon = "Bánh mỳ trứng", DonGia = 15000 },
            new() { TenMon = "Bánh mỳ cá", DonGia = 15000 },
            new() { TenMon = "Mỳ tôm trứng", DonGia = 20000 },
            new() { TenMon = "Mỳ xào bò", DonGia = 30000 },
            new() { TenMon = "Mỳ cay", DonGia = 50000 }
        };

        DanhSachHoaDonTam = new ObservableCollection<HoaDon>();

        // Thiết lập các Command tương ứng với các thao tác của nút bấm.
        ChonCommand = new RelayCommand(_ => ChonMon());
        NhapLaiCommand = new RelayCommand(_ => NhapLai());
        ThanhToanCommand = new RelayCommand(_ => ThanhToan());
        ThoatCommand = new RelayCommand(_ => Application.Current.Shutdown());
    }

    // ===================================
    // KHOẢNG KHÔNG GIAN KHAI BÁO PROPERTY
    // Những Property này được View bind tới.
    // ===================================
    
    public string TenKhachHang
    {
        get => _tenKhachHang;
        set
        {
            _tenKhachHang = value;
            OnPropertyChanged();
        }
    }

    public string SoDienThoai
    {
        get => _soDienThoai;
        set
        {
            _soDienThoai = value;
            OnPropertyChanged();
        }
    }

    public bool LaSinhVien
    {
        get => _laSinhVien;
        set
        {
            _laSinhVien = value;
            OnPropertyChanged();
        }
    }

    // Xử lý Check của RadioButton cho Bàn 01 đến 04.
    // Khi một bàn được check thành True, mình cho biến _viTriBan nhận theo luôn.
    public bool Ban01
    {
        get => _ban01;
        set { _ban01 = value; if (value) ViTriBan = "Bàn 01"; OnPropertyChanged(); }
    }

    public bool Ban02
    {
        get => _ban02;
        set { _ban02 = value; if (value) ViTriBan = "Bàn 02"; OnPropertyChanged(); }
    }

    public bool Ban03
    {
        get => _ban03;
        set { _ban03 = value; if (value) ViTriBan = "Bàn 03"; OnPropertyChanged(); }
    }

    public bool Ban04
    {
        get => _ban04;
        set { _ban04 = value; if (value) ViTriBan = "Bàn 04"; OnPropertyChanged(); }
    }

    public string ViTriBan
    {
        get => _viTriBan;
        set
        {
            _viTriBan = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<LuaChonMonViewModel> DanhSachNuocUong { get; }

    public ObservableCollection<LuaChonMonViewModel> DanhSachThucAn { get; }

    public ObservableCollection<HoaDon> DanhSachHoaDonTam { get; }

    // Hóa đơn vừa được "Chọn", dùng để hiển thị phần thông tin "Khách đã chọn" ở bên phải giao diện
    public HoaDon? HoaDonTam
    {
        get => _hoaDonTam;
        set
        {
            _hoaDonTam = value;
            OnPropertyChanged();
        }
    }

    // =============================
    // KHOẢNG KHÔNG GIAN CÁC COMMAND
    // =============================
    public ICommand ChonCommand { get; }
    public ICommand NhapLaiCommand { get; }
    public ICommand ThanhToanCommand { get; }
    public ICommand ThoatCommand { get; }

    // ========================================
    // KHOẢNG KHÔNG GIAN CÁC HÀM XỬ LÝ (LOGIC)
    // ========================================

    /// <summary>
    /// Xử lý khi nhấn nút "Chọn".
    /// Kiểm tra hợp lệ -> Lọc ra các món có tick IsSelected = true.
    /// Tính tiền -> Chuyển vào đối tượng Hoá Đơn Tạm.
    /// </summary>
    private void ChonMon()
    {
        // 1. Kiểm tra tính hợp lệ trước khi tạo (đề bài yêu cầu)
        if (!KiemTraDuLieuHopLe())
        {
            return; // Dừng nếu ko hợp lệ
        }

        // 2. Lấy ra những đồ uống đã được check
        List<MonAnNuocUong> nuocUongDaChon = DanhSachNuocUong
            .Where(x => x.IsSelected)
            .Select(x => new MonAnNuocUong
            {
                TenMon = x.TenMon,
                DonGia = x.DonGia,
                Loai = "Nước uống"
            })
            .ToList();

        // Lấy ra những thức ăn đã được check
        List<MonAnNuocUong> thucAnDaChon = DanhSachThucAn
            .Where(x => x.IsSelected)
            .Select(x => new MonAnNuocUong
            {
                TenMon = x.TenMon,
                DonGia = x.DonGia,
                Loai = "Thức ăn"
            })
            .ToList();

        // 3. Tính tiền gốc
        decimal tongTienTamTinh = nuocUongDaChon.Sum(x => x.DonGia) + thucAnDaChon.Sum(x => x.DonGia);
        
        // Theo yêu cầu, nếu là Sinh viên -> giảm giá 20% tổng tiền.
        decimal tongTienThanhToan = LaSinhVien ? tongTienTamTinh * 0.8m : tongTienTamTinh;

        // 4. Khởi tạo một hóa đơn tạm lưu lại các thông tin của Form
        HoaDonTam = new HoaDon
        {
            KhachHang = new KhachHang
            {
                TenKhachHang = TenKhachHang.Trim(),
                SoDienThoai = SoDienThoai.Trim(),
                LaSinhVien = LaSinhVien
            },
            ViTriBan = ViTriBan,
            DanhSachNuocUong = nuocUongDaChon,
            DanhSachThucAn = thucAnDaChon,
            TongTienTamTinh = tongTienTamTinh,
            TongTienThanhToan = tongTienThanhToan
        };

        // Đẩy vào danh sách hiển thị phần hóa đơn tạm (ListView nhỏ bên trái)
        DanhSachHoaDonTam.Clear();
        DanhSachHoaDonTam.Add(HoaDonTam);

        MessageBox.Show("Đã tạo hóa đơn tạm thành công.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    /// <summary>
    /// Kiểm tra Form nhập theo tiêu chí của đề bài.
    /// - Tên KH, SĐT phải khác rỗng. SĐT toàn số.
    /// - Bắt buộc chọn 1 bàn.
    /// - Chọn tối thiểu 1 đồ ăn hoặc đồ uống.
    /// </summary>
    private bool KiemTraDuLieuHopLe()
    {
        if (string.IsNullOrWhiteSpace(TenKhachHang))
        {
            MessageBox.Show("Tên khách hàng không được rỗng.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }

        if (string.IsNullOrWhiteSpace(SoDienThoai))
        {
            MessageBox.Show("Số điện thoại không được rỗng.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }

        // Bắt buộc nhập vào là số
        if (!SoDienThoai.All(char.IsDigit))
        {
            MessageBox.Show("Số điện thoại chỉ được nhập số.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }

        if (string.IsNullOrWhiteSpace(ViTriBan))
        {
            MessageBox.Show("Vui lòng chọn bàn.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }

        bool coNuoc = DanhSachNuocUong.Any(x => x.IsSelected);
        bool coThucAn = DanhSachThucAn.Any(x => x.IsSelected);

        if (!coNuoc && !coThucAn)
        {
            MessageBox.Show("Vui lòng chọn ít nhất một món ăn hoặc nước uống.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }

        return true;
    }

    /// <summary>
    /// Xử lý nhấn nút Thanh Toán.
    /// Đưa Hóa đơn tạm vào danh sách Hóa đơn chính thức của ứng dụng, sau đó reset Form.
    /// </summary>
    private void ThanhToan()
    {
        if (HoaDonTam == null)
        {
            MessageBox.Show("Bạn cần nhấn nút Chọn để tạo hóa đơn trước khi thanh toán.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        // Cất vào kho
        _quanLyHoaDonService.ThemHoaDon(HoaDonTam);
        MessageBox.Show("Thanh toán thành công.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

        // Đề bài: reset lại form nhập hóa đơn
        NhapLai();
    }

    /// <summary>
    /// Xóa toàn bộ dữ liệu trên màn hình để đưa về form rỗng.
    /// </summary>
    private void NhapLai()
    {
        TenKhachHang = string.Empty;
        SoDienThoai = string.Empty;
        LaSinhVien = false;

        Ban01 = false;
        Ban02 = false;
        Ban03 = false;
        Ban04 = false;
        ViTriBan = string.Empty;

        // Bỏ check tất cả thuộc tính
        foreach (LuaChonMonViewModel nuoc in DanhSachNuocUong)
        {
            nuoc.IsSelected = false;
        }

        foreach (LuaChonMonViewModel mon in DanhSachThucAn)
        {
            mon.IsSelected = false;
        }

        HoaDonTam = null;
        DanhSachHoaDonTam.Clear();
    }
}
