using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bai1_5.Models;



namespace Bai1_5.ViewModels
{
    public class StudentViewModel
    {
        // Danh sách sinh viên, khởi tạo ds mẫu
        //    public List<Student> Students { get; set; } = new List<Student>
        //     {
        //        new Student { Name = "An", Age = 20 },
        //        new Student { Name = "Bình", Age = 18 },
        //        new Student { Name = "Chi", Age = 19 }
        //     };  
        //}

        // Danh sách sinh viên
        private List<Student> _students; //biến nội bộ dùng để lưu dữ liệu của sinh viên
        public List<Student> Students //property công khai để các thành phần khác (UI, Class) truy cập dữ liệu
        {
            get { return _students; }  //khi UI cần đọc dữ liệu sẽ trả về danh sách _students
            set
            {
                _students = value; // cập nhật dữ liệu mới
            }
        }

        public StudentViewModel() //constructor (hàm khởi tạo của ViewModel, tên giống class
        {
            // Khởi tạo danh sách mẫu
            Students = new List<Student>
            {
                new Student { Name = "An", Age = 20 },
                new Student { Name = "Bình", Age = 18 },
                new Student { Name = "Chi", Age = 19 }
            };
        }

    }
}
