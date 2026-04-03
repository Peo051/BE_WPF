using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cafe.Models;

namespace Cafe.ViewModels
{
    //format dữ liệu 1 item (Text, tiền, ...)
    public class HoaDonItemViewModel
    {
        private HoaDon _hd;

        public HoaDonItemViewModel(HoaDon hd)
        {
            _hd = hd;
        }

        public int STT => _hd.STT;
        public string Ban => _hd.Ban;

        public string TenKhach => _hd.KhachHang.TenKhach;
        public string SoDienThoai => _hd.KhachHang.SoDienThoai;

        public string SinhVienText => _hd.KhachHang.LaSinhVien ? "Có" : "Không";

        public string NuocText => _hd.NuocUong != null
            ? string.Join(", ", _hd.NuocUong.Select(x => x.TenMon))
            : "";

        public string ThucAnText => _hd.ThucAn != null
            ? string.Join(", ", _hd.ThucAn.Select(x => x.TenMon))
            : "";

        public decimal TongTien => _hd.TongTien;
    }
}
