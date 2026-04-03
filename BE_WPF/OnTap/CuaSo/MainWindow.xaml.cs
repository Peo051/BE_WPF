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

namespace CuaSo
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
            string strMessage, strTitle, strHoTen, strNgoaiNgu = "";
            strHoTen = txtHoDem.Text + " " + txtTen.Text;
            if(rbNam.IsChecked == true)
            {
                strTitle = "Mr.";
            }
            else
            {
                strTitle = "Miss/Mrs";
            }
            strMessage = $"Xin chào {strTitle} {strHoTen}";

            if(cbTiengAnh.IsChecked == true)
            {
                strNgoaiNgu += "Tiếng Anh ";
            }
            if(cbTiengTrung.IsChecked == true)
            {
                strNgoaiNgu = (strNgoaiNgu.Length == 0) ? "Tiếng Trung" : (strNgoaiNgu + " và Tiếng Trung");
            }
            strMessage += "\n Ngoại ngữ: " + strNgoaiNgu;
            if (cbQueQuan.SelectedIndex >= 0) //Nếu đã có một mục trong danh sách được chọn
{
                strMessage += "\n Quê quán: " + cbQueQuan.Text;
            }
            MessageBox.Show(strMessage);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            txtHoDem.Text = "";
            txtTen.Text = "";
            rbNam.IsChecked = true;
            rbNu.IsChecked = false;
            cbTiengAnh.IsChecked = false;
            cbTiengTrung.IsChecked = false;
            cbQueQuan.SelectedIndex = 0;
        }

    }
}