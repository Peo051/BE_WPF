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

namespace CuaSo3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            cbBan.ItemsSource = new List<string> { "Bàn 1", "Bàn 2", "Bàn 3" };
            cbMon.ItemsSource = new List<string> { "Phở", "Bún chả", "Cơm tấm", "Gỏi cuốn" };
        }

        private void Button_ThemMon(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenKhachHang.Text) ||
                string.IsNullOrWhiteSpace(txtSDT.Text) ||
                cbBan.SelectedItem == null ||
                cbMon.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin và chọn món ăn trước khi thêm.");
                return;
            }

            // Thêm món vào danh sách
            lstThongTinMonAn.Items.Add(cbMon.SelectedItem.ToString());

            // Hiển thị thông tin khách hàng bên phải
            lstThongTinKhachHang.Items.Clear();
            lstThongTinKhachHang.Items.Add($"Khách hàng: {txtTenKhachHang.Text}");
            lstThongTinKhachHang.Items.Add($"SDT: {txtSDT.Text}");
            lstThongTinKhachHang.Items.Add($"Bàn: {cbBan.SelectedItem}");
        }

        private void Button_XoaMon(object sender, RoutedEventArgs e)
        {
            if(lstThongTinMonAn.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn món ăn cần xóa.");
            }
            lstThongTinMonAn.Items.Remove(lstThongTinMonAn.SelectedItem);
        }

        private void Button_DatMon(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenKhachHang.Text) || string.IsNullOrWhiteSpace(txtSDT.Text) || cbBan.SelectedItem == null || lstThongTinMonAn.Items.Count == 0)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin và chọn món ăn trước khi đặt.");
            }
            MessageBox.Show($"Đặt món thành công", "Thông báo");
        }
    }
}