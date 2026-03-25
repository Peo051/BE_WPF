namespace B7_MVVM.Models;

/// <summary>
/// L?p ??i di?n cho m?t Món ?n ho?c N??c U?ng trong menu c?a quán. (Model)
/// </summary>
public class MonAnNuocUong
{
    // Tên c?a món ?? (VD: Cafe ?en, M? cay)
    public string TenMon { get; set; } = string.Empty;

    // Giá ti?n c?a món ??
    public decimal DonGia { get; set; }

    // Phân lo?i: "N??c u?ng" ho?c "Th?c ?n" (dùng ?? chia nhóm)
    public string Loai { get; set; } = string.Empty; 
}
