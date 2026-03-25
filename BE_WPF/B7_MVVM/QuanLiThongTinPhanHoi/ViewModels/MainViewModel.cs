using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using QuanLiThongTinPhanHoi.Models;

namespace QuanLiThongTinPhanHoi.ViewModels;

public class MainViewModel : BaseViewModel
{
    private object _currentViewModel;

    public MainViewModel()
    {
        KhoiTaoDuLieuMau();

        ThongTinPhanHoiVM = new ThongTinPhanHoiViewModel(
            KhachHangHienTai,
            DanhSachPhanHoi,
            CapNhatDapAnCauHoi1);

        ThongTinGopYVM = new ThongTinGopYViewModel(
            KhachHangHienTai,
            DanhSachGopY,
            ThemGopY);

        ThongTinKhachHangVM = new ThongTinKhachHangViewModel(
            KhachHangHienTai,
            () =>
            {
                ThongTinGopYVM.RefreshTenKhachHang();
                CurrentViewModel = ThongTinGopYVM;
            },
            () =>
            {
                ThongTinPhanHoiVM.RefreshTenKhachHang();
                CurrentViewModel = ThongTinPhanHoiVM;
            });

        _currentViewModel = ThongTinKhachHangVM;

        MoThongTinKhachHangCommand = new RelayCommand(_ => CurrentViewModel = ThongTinKhachHangVM);
        MoPhanHoiCommand = new RelayCommand(_ =>
        {
            ThongTinPhanHoiVM.RefreshTenKhachHang();
            CurrentViewModel = ThongTinPhanHoiVM;
        });
        MoGopYCommand = new RelayCommand(_ =>
        {
            ThongTinGopYVM.RefreshTenKhachHang();
            CurrentViewModel = ThongTinGopYVM;
        });
        ThoatCommand = new RelayCommand(_ => Application.Current.Shutdown());
    }

    public ThongTinKhachHangViewModel ThongTinKhachHangVM { get; }

    public ThongTinPhanHoiViewModel ThongTinPhanHoiVM { get; }

    public ThongTinGopYViewModel ThongTinGopYVM { get; }

    public object CurrentViewModel
    {
        get => _currentViewModel;
        set
        {
            _currentViewModel = value;
            OnPropertyChanged();
        }
    }

    public ICommand MoThongTinKhachHangCommand { get; }

    public ICommand MoPhanHoiCommand { get; }

    public ICommand MoGopYCommand { get; }

    public ICommand ThoatCommand { get; }

    public KhachHang KhachHangHienTai { get; } = new();

    public ObservableCollection<DSCauHoi> DanhSachCauHoi { get; } = new();

    public ObservableCollection<DSPhanHoi> DanhSachPhanHoi { get; } = new();

    public ObservableCollection<DSGopY> DanhSachGopY { get; } = new();

    private void KhoiTaoDuLieuMau()
    {
        DanhSachCauHoi.Add(new DSCauHoi { SoThuTu = 1, NoiDung = "Chất lượng dịch vụ", DapAn = "" });
        DanhSachCauHoi.Add(new DSCauHoi { SoThuTu = 2, NoiDung = "Chất lượng sản phẩm", DapAn = "" });
        DanhSachCauHoi.Add(new DSCauHoi { SoThuTu = 3, NoiDung = "Chất lượng phục vụ", DapAn = "" });
        DanhSachCauHoi.Add(new DSCauHoi { SoThuTu = 4, NoiDung = "Chất lượng bảo hành", DapAn = "" });
        DanhSachCauHoi.Add(new DSCauHoi { SoThuTu = 5, NoiDung = "Chất lượng dịch vụ", DapAn = "" });

        DongBoPhanHoiTuCauHoi();
    }

    private void CapNhatDapAnCauHoi1(string dapAn)
    {
        DSCauHoi? cauHoi = DanhSachCauHoi.FirstOrDefault(x => x.SoThuTu == 1);
        if (cauHoi == null)
        {
            return;
        }

        cauHoi.DapAn = dapAn;
        DongBoPhanHoiTuCauHoi();
    }

    private void ThemGopY(string noiDung)
    {
        DanhSachGopY.Add(new DSGopY
        {
            NoiDung = noiDung,
            ThoiGian = DateTime.Now
        });
    }

    private void DongBoPhanHoiTuCauHoi()
    {
        DanhSachPhanHoi.Clear();
        foreach (DSCauHoi cauHoi in DanhSachCauHoi)
        {
            DanhSachPhanHoi.Add(new DSPhanHoi
            {
                NoiDung = $"Câu {cauHoi.SoThuTu}: {cauHoi.NoiDung} – Đáp án: {cauHoi.DapAn}"
            });
        }
    }
}
