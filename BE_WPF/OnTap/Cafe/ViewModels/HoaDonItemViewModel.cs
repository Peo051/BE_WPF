using Cafe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.ViewModels
{
    public class HoaDonItemViewModel
    {
        private HoaDon _hd;
        public HoaDonItemViewModel(HoaDon hd)
        {
            _hd = hd;
        }

        public int STT => _hd.STT;
        public string Ban => _hd.Ban;
        public string TenKhachHang => _hd.KhachHang.TenKH;
        public string SDT => _hd.KhachHang.SDT;
        public string SinhVienText => _hd.KhachHang.LaSinhVien ? "Có" : "Không";

        public string NuocText => _hd.DoUong != null ? string.Join(", ", _hd.DoUong.Select(m => m.TenMon)) : "";
        public string ThucAnText => _hd.ThucAn != null ? string.Join(", ", _hd.ThucAn.Select(m => m.TenMon)) : "";
        public decimal TongTien => _hd.TongTien;
    }
}
