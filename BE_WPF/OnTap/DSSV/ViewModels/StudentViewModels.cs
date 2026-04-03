using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSSV.Models;

namespace DSSV.ViewModels
{
    public class StudentViewModels
    {
        public List<Student> Students { get; set; }
        public StudentViewModels()
        {
            Students = new List<Student>
            {
                new Student { Name = "An", Age = 20 },
                new Student { Name = "Bình", Age = 22 },
                new Student { Name = "Chi", Age = 21 }
            };
        }
    }
}
