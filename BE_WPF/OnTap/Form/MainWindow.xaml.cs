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

namespace Form
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

        //Thêm sự kiện LostFocus
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if(string.IsNullOrEmpty(tb.Text))
            {
                MessageBox.Show("Vui lòng nhập thông tin vào ô này!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                tb.Focus(); //Đặt lại focus vào TextBox nếu người dùng để trống
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string info = $"Họ và tên:{txtName.Text}\nTuổi:{txtAge.Text}\nGhi chu:{txtNote.Text}";
            MessageBox.Show(info, "Thông tin cá nhân", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}