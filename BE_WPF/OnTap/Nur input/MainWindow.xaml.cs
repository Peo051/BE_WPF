using System.Globalization;
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

namespace Nut_input
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string fullName = txtName.Text.Trim(); //Lay ki tu tu TextBox
            if(string.IsNullOrEmpty(fullName))
            {
                MessageBox.Show("Vui lòng nhập tên của bạn!");
            }
            else
            {
                //Viết hoa chữ đầu mỗi từ
                TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo; //Lấy thông tin văn hóa hiện tại
                string formattedName = textInfo.ToTitleCase(fullName.ToLower()); //Viết hoa chữ đầu mỗi từ

                txt_show.Text = $"Xin chào, {formattedName}!"; //Hiển thị lời chào với tên đã được định dạng
            }
        }
    }
}