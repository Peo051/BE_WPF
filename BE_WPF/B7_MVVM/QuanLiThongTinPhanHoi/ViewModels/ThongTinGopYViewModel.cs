using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using QuanLiThongTinPhanHoi.Models;

namespace QuanLiThongTinPhanHoi.ViewModels;

public class ThongTinGopYViewModel : BaseViewModel
{
    private readonly KhachHang _khachHang;
    private readonly Action<string> _themGopY;

    private string _noiDungGopY = string.Empty;

    public ThongTinGopYViewModel(
        KhachHang khachHang,
        ObservableCollection<DSGopY> danhSachGopY,
        Action<string> themGopY)
    {
        _khachHang = khachHang;
        DanhSachGopY = danhSachGopY;
        _themGopY = themGopY;
        GuiGopYCommand = new RelayCommand(_ => GuiGopY());
    }

    public string TenKhachHang => _khachHang.TenKhachHang;

    public string NoiDungGopY
    {
        get => _noiDungGopY;
        set
        {
            _noiDungGopY = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<DSGopY> DanhSachGopY { get; }

    public ICommand GuiGopYCommand { get; }

    public void RefreshTenKhachHang()
    {
        OnPropertyChanged(nameof(TenKhachHang));
    }

    private void GuiGopY()
    {
        if (string.IsNullOrWhiteSpace(NoiDungGopY))
        {
            MessageBox.Show("Vui lòng nhập nội dung góp ý.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        _themGopY(NoiDungGopY.Trim());
        MessageBox.Show("Gửi góp ý thành công.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

        NoiDungGopY = string.Empty;
    }
}
