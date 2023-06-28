using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Core.Students;
using CMS.Core.StudentTeachers;

namespace CMS.Core.Teachers
{
    public class TeacherViewModel
    {
        public string FullName { get; set; }
        public ICollection<StudentTeacher> StudentTeachers { get; set; }
    }
}