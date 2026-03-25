using Bai3.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bai3.Service
{
    public class QuanLyThanhToanService
    {
        public ObservableCollection<PhieuThanhToan> DanhSachPhieu { get; } = new();

        public void ThemPhieu(PhieuThanhToan phieu)
        {
            phieu.STT = DanhSachPhieu.Count + 1;
            DanhSachPhieu.Add(phieu);
        }
    }
}
