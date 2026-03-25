namespace B7_MVVM.Models;

/// <summary>
/// L?p ??i di?n cho thông tin Khách Hàng. (Thành ph?n Model trong MVVM).
/// </summary>
public class KhachHang
{
    // Tên c?a khách hàng (TextBox s? binding v?i thu?c tính này)
    public string TenKhachHang { get; set; } = string.Empty;

    // S? ?i?n tho?i c?a khách (TextBox s? binding v?i thu?c tính này)
    public string SoDienThoai { get; set; } = string.Empty;

    // Xác ??nh xem khách có ph?i là sinh viên không (CheckBox s? binding v?i thu?c tính này)
    public bool LaSinhVien { get; set; }
}
