using System.Windows;
using System.Windows.Controls;

namespace Bai04
{
    public partial class ucSinhVien : UserControl
    {
        public ucSinhVien()
        {
            InitializeComponent();
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaSV.Text) || string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã SV và Họ tên!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            MessageBox.Show("Lưu sinh viên thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaSV.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã SV cần xóa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            MessageBox.Show("Xóa sinh viên thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnLamMoi_Click(object sender, RoutedEventArgs e)
        {
            txtMaSV.Clear();
            txtHoTen.Clear();
            dtpNgaySinh.SelectedDate = null;
            radNam.IsChecked = true;
            chkTheThao.IsChecked = false;
            chkAmNhac.IsChecked = false;
            chkDuLich.IsChecked = false;
            cboLop.SelectedIndex = -1;
            lstMonHoc.SelectedItems.Clear();
        }

        public void XoaForm()
        {
            btnLamMoi_Click(null!, null!);
        }
    }
}
