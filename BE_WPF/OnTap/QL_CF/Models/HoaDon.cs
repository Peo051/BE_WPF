using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_CF.Models
{
    public class HoaDon
    {
        public int STT { get; set; }
        public string TenKhachHang { get; set; }
        public string SoDienThoai { get; set; }
        public bool LaSinhVien { get; set; }
        public string TenBan { get; set; }
        public string NuocUong { get; set; }
        public string ThucAn { get; set; }
        public decimal TongTien { get; set; }
    }
}
