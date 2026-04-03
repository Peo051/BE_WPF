using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Models
{
    public class HoaDon
    {
        public KhachHang KhachHang { get; set; }
        public string Ban { get; set; }
        public List<Mon> NuocUong { get; set; }
        public List<Mon> ThucAn { get; set; }
        public int STT { get; set; }

        public decimal TongTien //(logic nghiệp vụ) — gắn trực tiếp với dữ liệu.
        {
            get
            {
                decimal tong = 0;

                if (NuocUong != null)
                    tong += NuocUong.Sum(x => x.Gia); //.Sum() dùng để cộng tất cả giá trị trong danh sách → thay cho vòng for

                if (ThucAn != null)
                    tong += ThucAn.Sum(x => x.Gia);

                if (KhachHang.LaSinhVien)
                    tong *= 0.8m;

                return tong;
            }
        }

        //public string NuocText => string.Join(", ", NuocUong.Select(x => x.TenMon)); ////=> thuộc tính viết gọn, trả về giá trị của biểu thức
        //public string ThucAnText => string.Join(", ", ThucAn.Select(x => x.TenMon)); //x =>: Lambda Expression = một hàm ngắn gọn: x => gì đó "Với mỗi x, làm gì đó"

    //    public string NuocText => NuocUong != null
    //? string.Join(", ", NuocUong.Select(x => x.TenMon))
    //: "";

    //    public string ThucAnText => ThucAn != null
    //        ? string.Join(", ", ThucAn.Select(x => x.TenMon))
    //        : "";
        //public string SinhVienText => KhachHang.LaSinhVien ? "Có" : "Không";
    }
}
