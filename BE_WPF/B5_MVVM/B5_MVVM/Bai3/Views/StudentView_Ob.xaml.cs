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
using Bai3.ViewModels;

namespace MVVM.Views
{
    /// <summary>
    /// Interaction logic for StudentView_Ob.xaml
    /// </summary>
    public partial class StudentView_Ob : Window
    {
        private StudentViewModel_Ob VM
        {
            get { return (StudentViewModel_Ob)DataContext; }
        }

        public StudentView_Ob()
        {
            InitializeComponent();
            // DataContext đã gán trong XAML
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            VM.AddStudent();
            //try
            //{
            //    string name = txtName.Text;
            //    int age = int.Parse(txtAge.Text);

            //    VM.AddStudent(name, age);

            //    txtName.Clear();
            //    txtAge.Clear();
            //}
            //catch
            //{
            //    MessageBox.Show("Dữ liệu không hợp lệ!");
            //}
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            VM.DeleteStudent();
        }

        private void BtnSort_Click(object sender, RoutedEventArgs e)
        {
            VM.ToggleSortByAge();
        }
    }
}
