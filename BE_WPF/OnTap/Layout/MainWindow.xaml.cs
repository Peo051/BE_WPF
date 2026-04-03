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

namespace Layout
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
            string fullName = txtName.Text.Trim();
            int age = txtAge.Text.Trim() != "" ? int.Parse(txtAge.Text.Trim()) : 0;
            if (string.IsNullOrEmpty(fullName))
            {
                MessageBox.Show("Vui lòng nhập tên của bạn!");
            }
            else
            {
                TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
                string formattedName = textInfo.ToTitleCase(fullName.ToLower());
                txt_show_name.Text = $"{formattedName}";
            }
            if (age <= 0)
            {
                MessageBox.Show("Vui lòng nhập tuổi hợp lệ!");
            }
            else
            {
                txt_show_age.Text = $"{age} tuổi";
            }
        }
    }
}