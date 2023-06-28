using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Core.StudentTeachers;
using CMS.Core.Teachers;

namespace Library.CMS.Core.Students
{
    public class Student
    {
        public int StudentId { get; set; }
        public string FullName { get; set; }
        public DateTime DOB { get; set; }
        public int Age { get; set; }
        public ICollection<StudentTeacher> StudentTeachers { get; set; }
 
    }
}