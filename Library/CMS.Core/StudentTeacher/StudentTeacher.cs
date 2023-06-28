using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Core.Teachers;
using Library.CMS.Core.Students;

namespace CMS.Core.StudentTeachers
{
    public class StudentTeacher
    {
        public int StudentId { get; set; }
        public int TeacherId { get; set; }
        public Student Student { get; set; }
        public Teacher Teacher { get; set; }
    }
}