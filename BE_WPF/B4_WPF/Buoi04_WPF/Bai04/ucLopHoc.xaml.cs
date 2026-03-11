using System.Windows;
using System.Windows.Controls;

namespace Bai04
{
    public partial class ucLopHoc : UserControl
    {
        public ucLopHoc()
        {
            InitializeComponent();
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaLop.Text) || string.IsNullOrWhiteSpace(txtTenLop.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã lớp và Tên lớp!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            MessageBox.Show("Lưu lớp học thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaLop.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã lớp cần xóa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            MessageBox.Show("Xóa lớp học thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnLamMoi_Click(object sender, RoutedEventArgs e)
        {
            txtMaLop.Clear();
            txtTenLop.Clear();
        }

        public void XoaForm()
        {
            btnLamMoi_Click(null!, null!);
        }
    }
}
