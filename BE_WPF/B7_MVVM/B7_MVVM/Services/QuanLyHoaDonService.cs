using System.Collections.ObjectModel;
using B7_MVVM.Models;

namespace B7_MVVM.Services;

/// <summary>
/// Service dùng chung (Singleton or injected) dùng ?? l?u l?i và chia s? b? nh?
/// gi?a các màn hình (LapHoaDonViewModel và DanhSachHoaDonViewModel).
/// </summary>
public class QuanLyHoaDonService
{
    // S? d?ng ObservableCollection ?? giao di?n t? c?p nh?t khi thêm m?i vào danh sách.
    // L?u các hóa ??n ?ã ???c "Thanh toán".
    public ObservableCollection<HoaDon> DanhSachHoaDon { get; } = new();

    /// <summary>
    /// Hàm thêm m?t hóa ??n vào kho l?u tr? (t?c là ?ã thanh toán).
    /// Gán t? ??ng s? th? t? t?ng d?n.
    /// </summary>
    /// <param name="hoaDon">??i t??ng hóa ??n c?n thanh toán/l?u</param>
    public void ThemHoaDon(HoaDon hoaDon)
    {
        // Gán STT d?a theo s? l??ng ph?n t? ?ang có trong danh sách
        hoaDon.STT = DanhSachHoaDon.Count + 1;
        
        // Thêm vào danh sách (S? kích ho?t cho List View t? ??ng hi?n th? m?i)
        DanhSachHoaDon.Add(hoaDon);
    }
}
