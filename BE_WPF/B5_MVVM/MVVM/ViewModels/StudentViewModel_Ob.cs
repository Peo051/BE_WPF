using System;
using System.Collections.Generic; //kiểu Collection
using System.Collections.ObjectModel; //ObservableCollection<T>
using System.ComponentModel; //INotifyPropertyChanged
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data; //ICollectionView
using MVVM.Models;

namespace MVVM.ViewModels
{
    public class StudentViewModel_Ob : BaseViewModel
    {
        // Danh sách sinh viên
        private ObservableCollection<Student> _students;
        public ObservableCollection<Student> Students
        {
            get { return _students; }
            set { _students = value; OnPropertyChanged("Students"); }
        }

        // ICollectionView để sắp xếp/lọc
        private ICollectionView _studentsView;
        public ICollectionView StudentsView
        {
            get { return _studentsView; }
            set { _studentsView = value; OnPropertyChanged("StudentsView"); }
        }

        private Student _selectedStudent;
        public Student SelectedStudent
        {
            get { return _selectedStudent; }
            set { _selectedStudent = value; OnPropertyChanged("SelectedStudent");
                if (_selectedStudent != null)
                {
                    NewName = _selectedStudent.Name;
                    NewAge = _selectedStudent.Age;
                }
            }
        }

        private string _newName;
        public string NewName
        {
            get { return _newName; }
            set { _newName = value; OnPropertyChanged("NewName"); }
        }

        private int _newAge;
        public int NewAge
        {
            get { return _newAge; }
            set { _newAge = value; OnPropertyChanged("NewAge"); }
        }

        //lưu text tìm kiếm do người dùng nhập và cập nhật lại danh sách sinh viên theo filter
        private string _filterText;
        public string FilterText
        {
            get { return _filterText; }
            set
            {
                _filterText = value;
                OnPropertyChanged("FilterText");
                StudentsView.Refresh(); // cập nhật filter
            }
        }

        public StudentViewModel_Ob()
        {
            // Khởi tạo danh sách sinh viên
            Students = new ObservableCollection<Student>
            {
                new Student { Name = "An", Age = 20 },
                new Student { Name = "Bình", Age = 18 },
                new Student { Name = "Chi", Age = 19 }
            };

            // Tạo ICollectionView từ ObservableCollection
            StudentsView = CollectionViewSource.GetDefaultView(Students);

            //Thêm một quy tắc sắp xếp cho StudentsView: → sắp xếp theo thuộc tính Age → theo thứ tự tăng dần: SortDescription là một ds các điều kiện sắp xếp
            StudentsView.SortDescriptions.Add(new SortDescription("Age", ListSortDirection.Ascending));
            

            // Hàm lọc: lọc danh sách sinh viên trước khi hiển thị ra UI (DataGrid, ListView, ListBox). chỉ hiển thị các sinh viên thỏa điều kiện FilterStudents
            StudentsView.Filter = FilterStudents; //Thiết lập điều kiện lọc cho StudentView

            //Filter trong ICollectionView được định nghĩa là Public Predicate<object> Filter {get; set;}: hàm nhận object trả về bool,true thì hiển thị và ngược lại
        }
        // Biến private để lưu trạng thái sắp xếp
        private bool _isAscending = true; // mặc định tăng dần

        // Hàm đổi chiều sắp xếp theo tuổi
        public void ToggleSortByAge()
        {
            if (StudentsView == null)
                return;

            // Xóa các SortDescriptions hiện tại
            StudentsView.SortDescriptions.Clear();

            // Thêm SortDescription mới theo chiều hiện tại
            if (_isAscending)
                StudentsView.SortDescriptions.Add(new SortDescription("Age", ListSortDirection.Descending));
            else
                StudentsView.SortDescriptions.Add(new SortDescription("Age", ListSortDirection.Ascending));

            // Đổi chiều cho lần sau
            _isAscending = !_isAscending;
        }

        // Hàm lọc
        private bool FilterStudents(object obj)
        {
            if (string.IsNullOrWhiteSpace(FilterText))
                return true;

            Student student = obj as Student; 
            if (student == null)
                return false;

            return student.Name.IndexOf(FilterText, StringComparison.OrdinalIgnoreCase) >= 0;
            //IndexOf: tìm vị trí xuất hiện của chuỗi con trong chuỗi : StringComparison.OrdinalIgnoreCase:so sánh chuỗi nhưng KHÔNG phân biệt chữ hoa chữ thường

            /// Nguyễn Văn A -->y --> 3
            /// 01234567
        }

        //  Thêm sinh viên
        public void AddStudent()
        {

            if (string.IsNullOrWhiteSpace(NewName) || NewAge <= 0)
            {
                MessageBox.Show("Dữ liệu không hợp lệ!");
                return;
            }

            Students.Add(new Student { Name = NewName, Age = NewAge });
            NewName = "";
            NewAge = 0;
            SelectedStudent = null;

        }

        //public void AddStudent(string name, int age)
        //{
        //    try
        //    {
        //        if (string.IsNullOrWhiteSpace(name))
        //        {
        //            throw new Exception("Tên sinh viên không được để trống!");
        //        }
        //        if (age <= 0)
        //        {
        //            throw new Exception("Tuổi sinh viên phải lớn hơn 0!");
        //        }

        //        Students.Add(new Student { Name = name, Age = age });
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Windows.MessageBox.Show("Lỗi khi thêm sinh viên: " + ex.Message);
        //    }
        //}

        // Xóa sinh viên
        public void DeleteStudent()
        {
            if (SelectedStudent == null)
            {
                MessageBox.Show("Vui lòng chọn sinh viên để xóa!");
                return;
            }

            Students.Remove(SelectedStudent);
            // reset dữ liệu textbox
            SelectedStudent = null;
            NewName = "";
            NewAge = 0;
        }

    }
}
