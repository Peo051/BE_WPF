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

namespace Bai04
{
    public partial class MainWindow : Window
    {
        private ucSinhVien ucSinhVien = new ucSinhVien();
        private ucLopHoc ucLopHoc = new ucLopHoc();

        public MainWindow()
        {
            InitializeComponent();
            contentArea.Content = ucSinhVien;
        }

        private void MenuDangNhap_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Chức năng Đăng nhập", "Thông báo");
        }

        private void MenuDoiMatKhau_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Chức năng Đổi mật khẩu", "Thông báo");
        }

        private void MenuThoat_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MenuSinhVien_Click(object sender, RoutedEventArgs e)
        {
            contentArea.Content = ucSinhVien;
        }

        private void MenuLopHoc_Click(object sender, RoutedEventArgs e)
        {
            contentArea.Content = ucLopHoc;
        }

        private void BtnThem_Click(object sender, RoutedEventArgs e)
        {
            if (contentArea.Content is ucSinhVien sv)
                sv.XoaForm();
            else if (contentArea.Content is ucLopHoc lh)
                lh.XoaForm();
        }

        private void BtnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (contentArea.Content is ucSinhVien sv)
                sv.XoaForm();
            else if (contentArea.Content is ucLopHoc lh)
                lh.XoaForm();
        }
    }
}