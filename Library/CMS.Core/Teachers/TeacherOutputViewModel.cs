using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Core.Teachers
{
    public class TeacherOutputViewModel
    {
        public string FullName { get; set; }
        public ICollection<StudentOutputViewModel> Students { get; set; }
    }
    public class StudentOutputViewModel
    {
        public string FullName { get; set; }
        public int Age { get; set; }
    }
}