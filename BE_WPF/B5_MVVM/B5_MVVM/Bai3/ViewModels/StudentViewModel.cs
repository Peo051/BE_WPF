using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Bai3.Models;// Import namespace chứa Student

namespace Bai3.ViewModels
{
    //  public class StudentViewModel
    //{
    //    // Danh sách sinh viên
    //    public List<Student> Students { get; set; }

    //    public StudentViewModel()
    //    {
    //        // Khởi tạo danh sách mẫu
    //        Students = new List<Student>
    //        {
    //            new Student { Name = "An", Age = 20 },
    //            new Student { Name = "Bình", Age = 18 },
    //            new Student { Name = "Chi", Age = 19 }
    //        };
    //    }
    //}

    public class StudentViewModel : INotifyPropertyChanged
    {
        // Danh sách sinh viên
        private List<Student> _students;
        public List<Student> Students
        {
            get { return _students; }
            set
            {
                _students = value;
                OnPropertyChanged("Students");
            }
        }

        // Sinh viên được chọn trong ListBox
        private Student _selectedStudent;
        public Student SelectedStudent
        {
            get { return _selectedStudent; }
            set
            {
                _selectedStudent = value;
                OnPropertyChanged("SelectedStudent");
            }
        }

        public StudentViewModel()
        {
            // Khởi tạo danh sách mẫu
            Students = new List<Student>
            {
                new Student { Name = "An", Age = 20 },
                new Student { Name = "Bình", Age = 18 }
            };
        }

        // 👉 Hàm thêm sinh viên với try/catch
        public void AddStudent(string name, int age)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    throw new Exception("Tên sinh viên không được để trống!");
                }
                if (age <= 0)
                {
                    throw new Exception("Tuổi sinh viên phải lớn hơn 0!");
                }

                // Clone list để gán lại
                List<Student> newList = new List<Student>(Students);
                newList.Add(new Student { Name = name, Age = age });
                Students = newList;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Lỗi khi thêm sinh viên: " + ex.Message);
            }
        }

        // 👉 Hàm xóa sinh viên với try/catch
        public void DeleteStudent()
        {
            try
            {
                if (SelectedStudent == null)
                {
                    throw new Exception("Vui lòng chọn sinh viên để xóa!");
                }

                List<Student> newList = new List<Student>(Students);
                newList.Remove(SelectedStudent);
                Students = newList;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Lỗi khi xóa sinh viên: " + ex.Message);
            }
        }

        // Sự kiện PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        // OnPropertyChanged viết đầy đủ, không dùng cú pháp ngắn gọn
        protected void OnPropertyChanged(string propertyName)
        {
            // Kiểm tra xem có handler nào đăng ký chưa
            if (PropertyChanged != null)
            {
                // Tạo đối tượng EventArgs với tên property
                PropertyChangedEventArgs args = new PropertyChangedEventArgs(propertyName);

                // Gọi tất cả các handler đã đăng ký
                PropertyChanged(this, args);
            }
        }
    }

}
