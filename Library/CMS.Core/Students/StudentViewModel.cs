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
        // public int IdTeacher { get; set; }
        public string IdTeacher { get; set; }
    }
}