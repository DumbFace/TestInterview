using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using CMS.Core.StudentTeachers;
using Library.CMS.Core.Students;

namespace CMS.Core.Teachers
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string FullName { get; set; }
        public ICollection<StudentTeacher> StudentTeachers { get; set; }
    }
}