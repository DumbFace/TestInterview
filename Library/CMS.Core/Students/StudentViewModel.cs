using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Core.Students
{
    public class StudentViewModel
    {
        public string FullName { get; set; }
        public DateTime DoB { get; set; }
        public List<int> idTeachers { get; set; } = new List<int>();
        public List<string> lstTeacherFullName { get; set; }
    }
}