using Cafe.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cafe.Models;

namespace Cafe
{
    public static class HoaDonService
    {
        public static ObservableCollection<HoaDon> DanhSachHoaDon { get; set; }
            = new ObservableCollection<HoaDon>();
    }
}
