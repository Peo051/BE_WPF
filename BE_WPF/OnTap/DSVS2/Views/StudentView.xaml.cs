using DSVS2.ViewModels;
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

namespace DSVS2.Views
{
    /// <summary>
    /// Interaction logic for StudentView.xaml
    /// </summary>
    public partial class StudentView : Window
    {
        StudentViewModels vm;
        public StudentView()
        {
            InitializeComponent();
            vm = new StudentViewModels();
            this.DataContext = vm;
        }

        private void Btn_Them(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = txtName.Text.Trim();
                int age = int.Parse(txtAge.Text);
                vm.AddStudent(name, age);
                txtName.Clear();
                txtAge.Clear();
            }
            catch
            {
                MessageBox.Show("Dữ liệu không hợp lệ!");
            }
        }

        private void Btn_Xoa(object sender, RoutedEventArgs e)
        {
            try
            {
                vm.DeleteStudent();
            }
            catch
            {
                MessageBox.Show("Không thể xóa!");
            }
        }
    }
}
