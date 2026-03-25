using Bai3.Models;
using Bai3.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Bai3.ViewModels
{
    public class ThongKeViewModel : BaseViewModel
    {
        private readonly QuanLyThanhToanService _quanLyThanhToanService;

        private int _tongSoLuotNguoi;
        private decimal _tongSoTien;

        public ObservableCollection<PhieuThanhToan> DanhSachPhieu => _quanLyThanhToanService.DanhSachPhieu;

        public int TongSoLuotNguoi
        {
            get => _tongSoLuotNguoi;
            set { _tongSoLuotNguoi = value; OnPropertyChanged(); }
        }

        public decimal TongSoTien
        {
            get => _tongSoTien;
            set { _tongSoTien = value; OnPropertyChanged(); }
        }

        public ICommand ThongKeCommand { get; }

        public ThongKeViewModel(QuanLyThanhToanService quanLyThanhToanService)
        {
            _quanLyThanhToanService = quanLyThanhToanService;

            ThongKeCommand = new RelayCommand(_ => CapNhatThongKe());

            _quanLyThanhToanService.DanhSachPhieu.CollectionChanged += (s, e) => CapNhatThongKe();
        }

        private void CapNhatThongKe()
        {
            TongSoLuotNguoi = DanhSachPhieu.Count;
            TongSoTien = DanhSachPhieu.Sum(x => x.TongTien);
        }
    }
}
