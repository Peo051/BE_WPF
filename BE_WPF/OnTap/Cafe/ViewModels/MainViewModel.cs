using Cafe.Models;
using Cafe.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cafe.Models;
using System.Windows.Input;

namespace Cafe.ViewModels
{
    //→ Có chức năng Lưu dữ liệu chung và điều hướng màn hình → KHÔNG xử lý nghiệp vụ
    public class MainViewModel : BaseViewModel
    {
        public ObservableCollection<HoaDon> DanhSachHoaDon { get; set; }


        //Cho phép UI biết khi nào màn hình thay đổi để tự update
        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand ShowLapHoaDonCommand { get; set; }
        public RelayCommand ShowDanhSachCommand { get; set; }

        public MainViewModel()
        {
            DanhSachHoaDon = new ObservableCollection<HoaDon>();
            ShowLapHoaDonCommand = new RelayCommand(ShowLapHoaDon);
            ShowDanhSachCommand = new RelayCommand(ShowDanhSach);

            // Mặc định mở màn hình lập hóa đơn
            ShowLapHoaDon(null);
        }

        // ================= METHOD RIÊNG =================

        private void ShowLapHoaDon(object obj)
        {
            CurrentView = new LapHoaDonView // tạo view mới gán là view hiện tại
            {
                DataContext = new LapHoaDonViewModel(DanhSachHoaDon) //gắn dữ liệu cho UI 
            };
        }

        private void ShowDanhSach(object obj)
        {
            CurrentView = new DanhSachHoaDonView
            {
                DataContext = new DanhSachHoaDonViewModel(DanhSachHoaDon)
            };
        }
    }
}
