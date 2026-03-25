using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bai3.Models
{
    public class PhieuThanhToan
    {
        public int STT { get; set; }
        public KhachHang KhachHang { get; set; } = new KhachHang();
        public int SoNgayO { get; set; }
        public string LoaiPhong { get; set; } = string.Empty;
        public bool CoTiVi { get; set; }
        public bool CoInternet { get; set; }
        public bool CoMayNuocNong { get; set; }

        public bool CoKaraoke { get; set; }
        public bool CoAnSang { get; set; }

        public decimal TienPhong { get; set; }
        public decimal TienTienNghi { get; set; }
        public decimal TienDichVu { get; set; }
        public decimal TongTien { get; set; }

        public string HienThiTienNghi
        {
            get
            {
                List<string> dsTienNghi = new List<string>();
                if (CoTiVi) dsTienNghi.Add("TiVi");
                if (CoInternet) dsTienNghi.Add("Internet");
                if (CoMayNuocNong) dsTienNghi.Add("Máy nước nóng");
                return string.Join(", ", dsTienNghi);
            }
        }

        public string HienThiDichVu
        {
            get
            {
                List<string> dsDichVu = new List<string>();
                if (CoKaraoke) dsDichVu.Add("Karaoke");
                if (CoAnSang) dsDichVu.Add("Ăn sáng");
                return string.Join(", ", dsDichVu);
            }

        }
    }
}
