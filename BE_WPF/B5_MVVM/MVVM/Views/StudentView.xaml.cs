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
using System.Xml.Linq;
using MVVM.ViewModels;

namespace MVVM.Views
{
    public partial class StudentView : Window
    {
        StudentViewModel vm;

        public StudentView()
        {
            InitializeComponent();

            // Gán ViewModel trực tiếp vào DataContext
            vm = new StudentViewModel();
            this.DataContext = vm;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = txtName.Text;
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

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
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

