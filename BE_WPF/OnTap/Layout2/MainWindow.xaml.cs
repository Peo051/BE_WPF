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

namespace Layout2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Mảng lưu thông tin người
        private string[,] people = new string[5, 2];
        private int count = 0; //Biến đếm số lượng người đã nhập
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string fullName = txtName.Text.Trim();
            string ageText = txtAge.Text.Trim();

            if(string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(ageText))
            {
                MessageBox.Show("Vui lòng nhập tên và tuổi của bạn!");
                return;
            }
            if(!int.TryParse(ageText, out int age) || age <= 0)
            {
                MessageBox.Show("Vui lòng nhập tuổi hợp lệ!");
                return;
            }
            if(count >= people.GetLength(0))
            {
                MessageBox.Show("Đã đạt giới hạn số lượng người!");
                return;
            }
            //Lưu thông tin người vào mảng
            people[count, 0] = fullName;
            people[count, 1] = ageText;
            count++;

            txt_show_info.Text = "Danh sách người đã nhập:\n";
            for(int i = 0; i < count; i++)
            {
                txt_show_info.Text += $"{i + 1}. {people[i, 0]} - {people[i, 1]} tuổi\n";
            }

            //Xóa thông tin đã nhập sau khi lưu
            txtName.Clear();
            txtAge.Clear();
            txtName.Focus();
        }
    }
}