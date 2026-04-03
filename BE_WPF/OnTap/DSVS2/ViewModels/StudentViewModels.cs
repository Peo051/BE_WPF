using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSVS2.Models;

namespace DSVS2.ViewModels
{
    public class StudentViewModels : BaseViewModel
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
        private Student _selectedStudents;
        public Student SelectedStudents
        {
            get { return _selectedStudents; }
            set
            {
                _selectedStudents = value;
                OnPropertyChanged("SelectedStudents");
            }
        }

        public StudentViewModels()
        {
            Students = new List<Student>
            {
                new Student {Name = "Bảo", Age = 20},
                new Student {Name = "Kim", Age = 19}
            };
        }

        public void AddStudent(string name, int age)
        {
            try
            {
                if(string.IsNullOrEmpty(name))
                {
                    throw new Exception("Tên không được để trống");
                }
                if (age <= 0)
                {
                    throw new Exception("Tuổi sinh viên phải lớn hơn 0");
                }
                List<Student> newList = new List<Student>(Students);
                newList.Add(new Student { Name = name, Age = age });
                Students = newList;
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
                if (SelectedStudents == null)
                {
                    throw new Exception("Vui lòng chọn sinh viên để xóa!");
                }
                List<Student> newList = new List<Student>(Students);
                newList.Remove(SelectedStudents);
                Students = newList;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Lỗi khi xóa sinh viên: " + ex.Message);
            }
        }
    }
}
