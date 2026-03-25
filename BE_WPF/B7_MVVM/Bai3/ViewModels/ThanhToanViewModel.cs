using Bai3.Service;
using Bai3.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Bai3.ViewModels
{
    public class ThanhToanViewModel : BaseViewModel
    {
        private readonly QuanLyThanhToanService _quanLyThanhToanService;

        private string _hoTen = string.Empty;
        private string _diaChi = string.Empty;
        private string _soNgayO = string.Empty;

        private bool _phongDon;
        private bool _phongDoi;
        private bool _phongBa;

        private bool _coTiVi;
        private bool _coInternet;
        private bool _coMayNuocNong;

        private bool _coKaraoke;
        private bool _coAnSang;

        private decimal _thanhTien;
        private PhieuThanhToan? _phieuTam;

        public string HoTen
        {
            get => _hoTen;
            set { _hoTen = value; OnPropertyChanged(); }
        }

        public string DiaChi
        {
            get => _diaChi;
            set { _diaChi = value; OnPropertyChanged(); }
        }

        public string SoNgayO
        {
            get => _soNgayO;
            set { _soNgayO = value; OnPropertyChanged(); }
        }

        public bool PhongDon
        {
            get => _phongDon;
            set
            {
                _phongDon = value;
                if (value)
                {
                    PhongDoi = false;
                    PhongBa = false;
                }
                OnPropertyChanged();
            }
        }

        public bool PhongDoi
        {
            get => _phongDoi;
            set
            {
                _phongDoi = value;
                if (value)
                {
                    PhongDon = false;
                    PhongBa = false;
                }
                OnPropertyChanged();
            }
        }

        public bool PhongBa
        {
            get => _phongBa;
            set
            {
                _phongBa = value;
                if (value)
                {
                    PhongDon = false;
                    PhongDoi = false;
                }
                OnPropertyChanged();
            }
        }

        public bool CoTivi
        {
            get => _coTiVi;
            set { _coTiVi = value; OnPropertyChanged(); }
        }

        public bool CoInternet
        {
            get => _coInternet;
            set { _coInternet = value; OnPropertyChanged(); }
        }

        public bool CoMayNuocNong
        {
            get => _coMayNuocNong;
            set { _coMayNuocNong = value; OnPropertyChanged(); }
        }

        public bool CoKaraoke
        {
            get => _coKaraoke;
            set { _coKaraoke = value; OnPropertyChanged(); }
        }

        public bool CoAnSang
        {
            get => _coAnSang;
            set { _coAnSang = value; OnPropertyChanged(); }
        }

        public decimal ThanhTien
        {
            get => _thanhTien;
            set { _thanhTien = value; OnPropertyChanged(); }
        }

        public ObservableCollection<PhieuThanhToan> DanhSachTam { get; } = new();

        public ICommand ThanhToanCommand { get; }
        public ICommand NhapMoiCommand { get; }
        public ICommand TinhTienCommand { get; }
        public ICommand ThoatCommand { get; }

        public ThanhToanViewModel(QuanLyThanhToanService quanLyThanhToanService)
        {
            _quanLyThanhToanService = quanLyThanhToanService;

            TinhTienCommand = new RelayCommand(_ => TinhTien());
            ThanhToanCommand = new RelayCommand(_ => ThanhToan());
            NhapMoiCommand = new RelayCommand(_ => NhapMoi());
            ThoatCommand = new RelayCommand(_ => Application.Current.Shutdown());

            // Giá trị mặc định khi mở màn hình.
            PhongDon = true;
        }

        private bool KiemTraHopLe()
        {
            if (string.IsNullOrWhiteSpace(HoTen))
            {
                MessageBox.Show("Vui lòng nhập họ tên.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(DiaChi))
            {
                MessageBox.Show("Vui lòng nhập địa chỉ.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(SoNgayO))
            {
                MessageBox.Show("Vui lòng nhập số ngày ở.");
                return false;
            }

            if (!int.TryParse(SoNgayO, out int soNgay) || soNgay <= 0)
            {
                MessageBox.Show("Số ngày ở phải là số nguyên dương.");
                return false;
            }

            if (!PhongDon && !PhongDoi && !PhongBa)
            {
                MessageBox.Show("Vui lòng chọn loại phòng.");
                return false;
            }

            return true;
        }

        private string LayLoaiPhong()
        {
            if (PhongDon) return "Phòng đơn";
            if (PhongDoi) return "Phòng đôi";
            return "Phòng ba";
        }

        private decimal LayDonGiaPhong()
        {
            if (PhongDon) return 300000;
            if (PhongDoi) return 350000;
            return 400000;
        }

        private void TinhTien()
        {
            if (!KiemTraHopLe())
                return;

            int soNgay = int.Parse(SoNgayO);
            string loaiPhong = LayLoaiPhong();
            decimal donGiaPhong = LayDonGiaPhong();

            decimal tienPhong = donGiaPhong * soNgay;

            int soTienNghi = 0;
            if (CoTivi) soTienNghi++;
            if (CoInternet) soTienNghi++;
            if (CoMayNuocNong) soTienNghi++;

            decimal tienTienNghi = soTienNghi * 10000;

            decimal tienDichVu = 0;
            if (CoKaraoke) tienDichVu += 50000;
            if (CoAnSang) tienDichVu += 15000 * soNgay;

            decimal tongTien = tienPhong + tienTienNghi + tienDichVu;

            ThanhTien = tongTien;

            _phieuTam = new PhieuThanhToan
            {
                KhachHang = new KhachHang
                {
                    HoTen = HoTen.Trim(),
                    DiaChi = DiaChi.Trim()
                },
                SoNgayO = soNgay,
                LoaiPhong = loaiPhong,
                CoTiVi = CoTivi,
                CoInternet = CoInternet,
                CoMayNuocNong = CoMayNuocNong,
                CoKaraoke = CoKaraoke,
                CoAnSang = CoAnSang,
                TienPhong = tienPhong,
                TienTienNghi = tienTienNghi,
                TienDichVu = tienDichVu,
                TongTien = tongTien
            };

            DanhSachTam.Clear();
            DanhSachTam.Add(_phieuTam);
        }

        private void ThanhToan()
        {
            if (_phieuTam == null)
            {
                MessageBox.Show("Vui lòng bấm Tính tiền trước khi thanh toán.");
                return;
            }

            _quanLyThanhToanService.ThemPhieu(_phieuTam);
            MessageBox.Show("Thanh toán thành công.");
            NhapMoi();
        }

        private void NhapMoi()
        {
            HoTen = string.Empty;
            DiaChi = string.Empty;
            SoNgayO = string.Empty;

            PhongDon = true;
            PhongDoi = false;
            PhongBa = false;

            CoTivi = false;
            CoInternet = false;
            CoMayNuocNong = false;

            CoKaraoke = false;
            CoAnSang = false;

            ThanhTien = 0;
            _phieuTam = null;
            DanhSachTam.Clear();
        }
    }
}

