using System.Windows;
using System.Windows.Input;
using QuanLiThongTinPhanHoi.Models;

namespace QuanLiThongTinPhanHoi.ViewModels;

public class ThongTinKhachHangViewModel : BaseViewModel
{
    private readonly KhachHang _khachHang;
    private readonly Action _moGopY;
    private readonly Action _moPhanHoi;

    public ThongTinKhachHangViewModel(KhachHang khachHang, Action moGopY, Action moPhanHoi)
    {
        _khachHang = khachHang;
        _moGopY = moGopY;
        _moPhanHoi = moPhanHoi;

        ThamGiaGopYCommand = new RelayCommand(_ => _moGopY());
        PhanHoiCommand = new RelayCommand(_ => _moPhanHoi());
        ThoatCommand = new RelayCommand(_ => Application.Current.Shutdown());
    }

    public string TenKhachHang
    {
        get => _khachHang.TenKhachHang;
        set
        {
            _khachHang.TenKhachHang = value;
            OnPropertyChanged();
        }
    }

    public string SoDienThoai
    {
        get => _khachHang.SoDienThoai;
        set
        {
            _khachHang.SoDienThoai = value;
            OnPropertyChanged();
        }
    }

    public ICommand ThamGiaGopYCommand { get; }

    public ICommand PhanHoiCommand { get; }

    public ICommand ThoatCommand { get; }
}
