using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Bai2.ViewModel;

namespace Bai2.Views
{
    /// <summary>
    /// Interaction logic for StudentView.xaml
    /// </summary>
    public partial class StudentView : Window
    {
        // Property lấy ViewModel từ DataContext
        private StudentViewModel VM
        {
            get { return (StudentViewModel)DataContext; }

        }

        public StudentView()
        {
            InitializeComponent();
            // Không cần gán DataContext vì đã gán trong XAML
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = txtName.Text;
                int age = int.Parse(txtAge.Text);

                //AddStudent(name, age);
                ((StudentViewModel)DataContext).AddStudent(name, age);
                //VM.AddStudent(name, age);

                txtName.Clear();
                txtAge.Clear();
            }
            catch
            {
                MessageBox.Show("Dữ liệu không hợp lệ!");
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //DeleteStudent();
                ((StudentViewModel)DataContext).DeleteStudent();
            }
            catch
            {
                MessageBox.Show("Không thể xóa!");
            }
        }
    }
}
