using System.Collections.ObjectModel;
using QuanLiThongTinPhanHoi.Models;

namespace QuanLiThongTinPhanHoi.ViewModels;

public class ThongTinPhanHoiViewModel : BaseViewModel
{
    private readonly KhachHang _khachHang;
    private readonly Action<string> _capNhatDapAnCauHoi1;

    private bool _ratHaiLong;
    private bool _haiLong;
    private bool _binhThuong;
    private bool _khongHaiLong;

    public ThongTinPhanHoiViewModel(
        KhachHang khachHang,
        ObservableCollection<DSPhanHoi> danhSachPhanHoi,
        Action<string> capNhatDapAnCauHoi1)
    {
        _khachHang = khachHang;
        DanhSachPhanHoi = danhSachPhanHoi;
        _capNhatDapAnCauHoi1 = capNhatDapAnCauHoi1;
    }

    public string TenKhachHang => _khachHang.TenKhachHang;

    public string CauHoi => "Bạn đánh giá chất lượng dịch vụ như thế nào?";

    public ObservableCollection<DSPhanHoi> DanhSachPhanHoi { get; }

    public bool RatHaiLong
    {
        get => _ratHaiLong;
        set
        {
            _ratHaiLong = value;
            OnPropertyChanged();
            if (value) CapNhatLuaChon("Rất hài lòng");
        }
    }

    public bool HaiLong
    {
        get => _haiLong;
        set
        {
            _haiLong = value;
            OnPropertyChanged();
            if (value) CapNhatLuaChon("Hài lòng");
        }
    }

    public bool BinhThuong
    {
        get => _binhThuong;
        set
        {
            _binhThuong = value;
            OnPropertyChanged();
            if (value) CapNhatLuaChon("Bình thường");
        }
    }

    public bool KhongHaiLong
    {
        get => _khongHaiLong;
        set
        {
            _khongHaiLong = value;
            OnPropertyChanged();
            if (value) CapNhatLuaChon("Không hài lòng");
        }
    }

    public void RefreshTenKhachHang()
    {
        OnPropertyChanged(nameof(TenKhachHang));
    }

    private void CapNhatLuaChon(string dapAn)
    {
        _capNhatDapAnCauHoi1(dapAn);
        OnPropertyChanged(nameof(DanhSachPhanHoi));
    }
}
