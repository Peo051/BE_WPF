using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CuaSo2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Info(object sender, RoutedEventArgs e)
        {
            //Lấy dữ liệu
            string hoTen = txtHoTen.Text.Trim();
            string gioiTinh = (rbNam.IsChecked == true) ? "Nam" : (rbNu.IsChecked == true) ? "Nữ" : "Không xác định";
            string dateOfBirth = dpNgaySinh.SelectedDate.HasValue ? dpNgaySinh.SelectedDate.Value.ToString("dd/MM/yyyy") : "Chưa chọn ngày sinh";
            string quocTich = (cbQuocTich.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Chưa chọn";
            string ngheNghiep = txtNgheNghiep.Text.Trim();

            var sothichList = new StringBuilder();
            if (cbTheThao.IsChecked == true) sothichList.Append("Thể thao ");
            if (cbNgheNhac.IsChecked == true) sothichList.Append("Âm nhạc ");
            if (cbDuLich.IsChecked == true) sothichList.Append("Du lịch ");
            if (cbDocSach.IsChecked == true) sothichList.Append("Đọc sách ");
            string soThich = sothichList.Length > 0 ? sothichList.ToString() : "Không có sở thích nào được chọn";

            var kyNang = lvKyNang.SelectedItems.Cast<ListBoxItem>().Select(i => i.Content.ToString());
            string kyNangText = kyNang.Any() ? string.Join(", ", kyNang):"Chưa chọn";

            //Hien thi thong tin
            txtHoTen.Text = hoTen;


        }

        private void Button_Thoat(object sender, RoutedEventArgs e)
        {

        }

    }
}