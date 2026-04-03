using Cafe.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cafe.Models;
using System.Windows.Documents;

namespace Cafe.ViewModels
{
    //→ nhập liệu  → tính tiền  → thêm hóa đơn
    public class LapHoaDonViewModel : BaseViewModel
    {
        private string _tenKhach;
        public string TenKhach
        {
            get => _tenKhach;
            set
            {
                _tenKhach = value;
                OnPropertyChanged();
            }
        }

        private string _soDienThoai;
        public string SoDienThoai
        {
            get => _soDienThoai;
            set
            {
                _soDienThoai = value;
                OnPropertyChanged();
            }
        }

        private bool _laSinhVien;
        public bool LaSinhVien
        {
            get => _laSinhVien;
            set
            {
                _laSinhVien = value;
                OnPropertyChanged();
            }
        }
        public string BanDuocChon { get; set; }

        public ObservableCollection<Mon> NuocUong { get; set; }
        public ObservableCollection<Mon> ThucAn { get; set; }

        public HoaDonItemViewModel HoaDonHienThi { get; set; }

        public RelayCommand ChonBanCommand { get; set; }

        public RelayCommand ChonCommand { get; set; }
        public RelayCommand ThanhToanCommand { get; set; }
        public RelayCommand NhapLaiCommand { get; set; }
        public RelayCommand ThoatCommand { get; set; }

        private HoaDon hoaDonTam;

        private ObservableCollection<HoaDon> _ds;
        public LapHoaDonViewModel(ObservableCollection<HoaDon> ds)
        {
            _ds = ds; //KHÔNG tạo list mới → dùng CHUNG list với MainViewModel

            NuocUong = new ObservableCollection<Mon>
            {
                new Mon{ TenMon="Cafe đen", Gia=20000 },
                new Mon{ TenMon="Cafe đá", Gia=25000 },
                new Mon{ TenMon="Cafe sữa", Gia=25000 },
                new Mon{ TenMon="Cafe sữa đá", Gia=30000 },
                new Mon{ TenMon="Cafe kem", Gia=35000 }
            };

            ThucAn = new ObservableCollection<Mon>
            {
                new Mon{ TenMon="Bánh mỳ trứng", Gia=15000 },
                new Mon{ TenMon="Bánh mỳ cá", Gia=15000 },
                new Mon{ TenMon="Mỳ tôm trứng", Gia=20000 },
                new Mon{ TenMon="Mỳ xào bò", Gia=30000 },
                new Mon{ TenMon="Mỳ cay", Gia=50000 }
            };

            // ✅ KHÔNG dùng lambda nữa
            ChonCommand = new RelayCommand(Chon);
            ChonBanCommand = new RelayCommand(ChonBan);
            ThanhToanCommand = new RelayCommand(ThanhToan);
            NhapLaiCommand = new RelayCommand(NhapLai);
            ThoatCommand = new RelayCommand(Thoat);
        }

        // ================= COMMAND METHODS =================

        void ChonBan(object obj)
        {
            BanDuocChon = obj.ToString();
            OnPropertyChanged(nameof(BanDuocChon)); // đảm bảo UI update nếu cần
        }
        void Chon(object o)
        {
            TaoHoaDon();
        }

        void ThanhToan(object o)
        {
            if (hoaDonTam == null) return;

            // 👉 STT tự tăng
            hoaDonTam.STT = _ds.Count + 1;

            _ds.Add(hoaDonTam);
            Reset();
        }

        //bool CanThanhToan(object o)
        //{
        //    return hoaDonTam != null;
        //}

        void NhapLai(object o)
        {
            Reset();
        }

        void Thoat(object o)
        {
            System.Windows.Application.Current.Shutdown();
        }

        // ================= LOGIC =================

        void TaoHoaDon()
        {
            hoaDonTam = new HoaDon
            {
                KhachHang = new KhachHang
                {
                    TenKhach = TenKhach,
                    SoDienThoai = SoDienThoai,
                    LaSinhVien = LaSinhVien
                },
                Ban = BanDuocChon,
                NuocUong = NuocUong.Where(x => x.IsSelected).ToList(), //LINQ
                ThucAn = ThucAn.Where(x => x.IsSelected).ToList()
            };

            HoaDonHienThi = new HoaDonItemViewModel(hoaDonTam);
            OnPropertyChanged(nameof(HoaDonHienThi));
        }

        void Reset()
        {
            TenKhach = "";
            SoDienThoai = "";
            LaSinhVien = false;
            BanDuocChon = null;

            // 👉 BẮT BUỘC phải notify
            OnPropertyChanged(nameof(TenKhach));
            OnPropertyChanged(nameof(SoDienThoai));
            OnPropertyChanged(nameof(LaSinhVien));
            OnPropertyChanged(nameof(BanDuocChon));

            foreach (var item in NuocUong)
                item.IsSelected = false;

            foreach (var item in ThucAn)
                item.IsSelected = false;

            HoaDonHienThi = null;
            OnPropertyChanged(nameof(HoaDonHienThi));

            // (nếu có disable nút thanh toán sau này)
            ThanhToanCommand?.RaiseCanExecuteChanged();
        }
    }
}
