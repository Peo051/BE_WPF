using System.Linq;

namespace B7_MVVM.Models;

/// <summary>
/// L?p ??i di?n cho m?t Hóa ??n bán hàng. (Thành ph?n Model).
/// L?u tr? toàn b? thông tin v? vi?c mua hàng, t? khách hàng cho ??n món ?n n??c u?ng và tính ti?n.
/// </summary>
public class HoaDon
{
    // S? th? t? hóa ??n (dùng ?? hi?n th? trong Danh sách hóa ??n)
    public int STT { get; set; }

    // Thông tin Khách hàng c?a hóa ??n này
    public KhachHang KhachHang { get; set; } = new();

    // V? trí bàn mà khách ?ang ng?i (VD: Bàn 01)
    public string ViTriBan { get; set; } = string.Empty;

    // Danh sách các lo?i n??c u?ng khách ?ã ch?n
    public List<MonAnNuocUong> DanhSachNuocUong { get; set; } = new();

    // Danh sách các lo?i th?c ?n khách ?ã ch?n
    public List<MonAnNuocUong> DanhSachThucAn { get; set; } = new();

    // T?ng s? ti?n tr??c khi ???c gi?m giá
    public decimal TongTienTamTinh { get; set; }

    // T?ng s? ti?n th?c t? khách ph?i tr? sau khi gi?m giá (n?u là sinh viên)
    public decimal TongTienThanhToan { get; set; }

    // ========================================================
    // Các thu?c tính ti?n ích (Helper) ?? hi?n th? lên ListView
    // Các thu?c tính này ch? ph?c v? m?c ?ích ??a d? li?u lên View ??p h?n.
    // ========================================================

    // Tr? v? chu?i "Có" n?u là sinh viên, "Không" n?u ng??c l?i.
    public string SinhVienHienThi => KhachHang.LaSinhVien ? "Có" : "Không";

    // Ghép các món n??c u?ng thành 1 chu?i liên t?c (VD: "Cafe ?en, Cafe s?a")
    public string NuocUongHienThi => string.Join(", ", DanhSachNuocUong.Select(x => x.TenMon));

    // Ghép các món th?c ?n thành 1 chu?i liên t?c (VD: "Bánh m? tr?ng, M? xào bò")
    public string ThucAnHienThi => string.Join(", ", DanhSachThucAn.Select(x => x.TenMon));
}
