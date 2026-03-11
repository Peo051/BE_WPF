using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bai2_5.Models;

namespace Bai2_5.ViewModels
{
    public class StudentViewModel : BaseViewModel
    {
        // Danh sách sinh viên
        private ObservableCollection<Student> _students;
        public ObservableCollection<Student> Students
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
            Students = new ObservableCollection<Student>
            {
                new Student { Name = "An", Age = 20 },
                new Student { Name = "Bình", Age = 18 }
            };
        }

        // Hàm thêm sinh viên với try/catch
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

                // Clone list để gán lại  --tạo một bản sao (copy) của danh sách hiện tại.
                //List<Student> newList = new List<Student>(Students);
                //newList.Add(new Student { Name = name, Age = age });
                //Students = newList;
                Students.Add(new Student { Name = name, Age = age });
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Lỗi khi thêm sinh viên: " + ex.Message);
            }
        }

        // Hàm xóa sinh viên với try/catch
        public void DeleteStudent()
        {
            try
            {
                if (SelectedStudent == null)
                {
                    throw new Exception("Vui lòng chọn sinh viên để xóa!");
                }

                //List<Student> newList = new List<Student>(Students);
                //newList.Remove(SelectedStudent);
                //Students = newList;
                Students.Remove(SelectedStudent);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Lỗi khi xóa sinh viên: " + ex.Message);
            }
        }

    }

}
