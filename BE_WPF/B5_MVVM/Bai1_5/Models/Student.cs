using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bai1_5.Models
{
    public class Student // mô tả cấu trúc dữ liệu, chứa dữ liệu và cấu trúc, chỉ chứa dữ liệu của 1 sinh viên, ko quản lý danh sách và cung cấp dữ liệu cho UI
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
    // Viết dạng đầy đủ
    //public class Student
    //{
    //    private string _name; // biến để lưu dữ liệu thật của sinh viên
    //    private int _age; // biến dữ liệu để lưu dữ liệu của tuổi

    //    public string Name //property public để UI và các lớp khác có thể truy cập để lấy dữ liệu
    //    {
    //        get { return _name; } // khi UI cần đọc dữ liệu thì sẽ trả về tên của SV
    //        set { _name = value; } //cập nhật dữ liệu tên
    //    }

    //    public int Age
    //    {
    //        get { return _age; }
    //        set { _age = value; }
    //    }
    //}
}
