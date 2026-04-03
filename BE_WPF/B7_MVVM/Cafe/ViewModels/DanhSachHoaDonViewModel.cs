using Cafe.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace Cafe.ViewModels
{
    //→ hiển thị danh sách → filter / tìm kiếm
    public class DanhSachHoaDonViewModel : BaseViewModel
    {
        public ObservableCollection<HoaDonItemViewModel> DanhSachHoaDon { get; set; }

        public int TongKhach => DanhSachHoaDon.Count;

        public decimal TongTien => DanhSachHoaDon.Sum(x => x.TongTien); //LINQ

        public DanhSachHoaDonViewModel(ObservableCollection<HoaDon> ds)
        {
            //Có tham số dùng khi: nhận dữ liệu từ bên ngoài
            DanhSachHoaDon = new ObservableCollection<HoaDonItemViewModel>(ds.Select(x => new HoaDonItemViewModel(x))); //Chuyển danh sách HoaDon → danh sách hiển thị (HoaDonItemViewModel)
        }
    }
}
